using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace jxGameFramework.Media
{
    public class VideoDecoder : IDisposable
    {
        public delegate void EndOfFileHandler();

        private const int PIXEL_FORMAT = 6;

        private IntPtr buffer;

        public int BufferSize;

        private FFmpeg.AVCodecContext codecCtx;

        private double currentDisplayTime;

        private Thread decodingThread;

        private FFmpeg.AVFormatContext formatContext;

        public double FrameDelay;

        private int frameFinished;

        private bool isDisposed;

        private double lastPts;

        private IntPtr packet;

        private IntPtr pCodec;

        private IntPtr pCodecCtx;

        private IntPtr pFormatCtx;

        private IntPtr pFrame;

        private IntPtr pFrameRGB;

        private FFmpeg.AVStream stream;

        private GCHandle streamHandle;

        private bool videoOpened;

        private int videoStream;

        private double[] FrameBufferTimes;

        private byte[][] FrameBuffer;

        private int writeCursor;

        private int readCursor;

        private string fullfilename;

        public double Length
        {
            get
            {
                long duration = this.stream.duration;
                if (duration < 0L)
                {
                    return 36000000.0;
                }
                return (double)duration * this.FrameDelay;
            }
        }

        public int width
        {
            get
            {
                return this.codecCtx.width;
            }
        }

        public int height
        {
            get
            {
                return this.codecCtx.height;
            }
        }

        public double CurrentTime
        {
            get
            {
                return this.currentDisplayTime;
            }
        }

        private double startTimeMs
        {
            get
            {
                return (double)(1000L * this.stream.start_time) * this.FrameDelay;
            }
        }
        private string _fname;
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="bufferSize">need a suitable buffer size?</param>
        public VideoDecoder(int bufferSize)
        {
            this.BufferSize = bufferSize;
            this.FrameBufferTimes = new double[this.BufferSize];
            this.FrameBuffer = new byte[this.BufferSize][];
            FFmpeg.av_register_all();
            this.videoOpened = false;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        ~VideoDecoder()
        {
            this.Dispose(false);
        }

        public void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }
            this.isDisposed = true;
            if (this.decodingThread != null)
            {
                this.decodingThread.Abort();
            }
            try
            {
                Marshal.FreeHGlobal(this.packet);
                Marshal.FreeHGlobal(this.buffer);
            }
            catch
            {
            }
            try
            {
                FFmpeg.av_free(this.pFrameRGB);
                FFmpeg.av_free(this.pFrame);
            }
            catch
            {
            }
            try
            {
                this.streamHandle.Free();
            }
            catch
            {
            }
            this.frameFinished = 0;
            try
            {
                FFmpeg.avcodec_close(this.pCodecCtx);
                FFmpeg.av_close_input_file(this.pFormatCtx);
            }
            catch
            {
            }
        }

        public bool Open(string path)
        {
            fullfilename = path;
            FileStream inStream = File.OpenRead(path);
            this.OpenStream(inStream);
            return true;
        }

        public bool Open(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return false;
            }
            this.streamHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr intPtr = this.streamHandle.AddrOfPinnedObject();
            if (this.videoOpened)
            {
                return false;
            }
            this.videoOpened = true;
            string text = fullfilename;
            _fname = fullfilename;
            int num = FFmpeg.av_open_input_file(out this.pFormatCtx, text, IntPtr.Zero, bytes.Length, IntPtr.Zero);
            if (num != 0)
            {
                throw new Exception("Couldn't open input file");
            }
            num = FFmpeg.av_find_stream_info(this.pFormatCtx);
            if (num < 0)
            {
                throw new Exception("Couldn't find stream info");
            }
            FFmpeg.dump_format(this.pFormatCtx, 0, text, 0);
            this.formatContext = (FFmpeg.AVFormatContext)Marshal.PtrToStructure(this.pFormatCtx, typeof(FFmpeg.AVFormatContext));
            this.videoStream = -1;
            int nb_streams = this.formatContext.nb_streams;
            for (int i = 0; i < nb_streams; i++)
            {
                FFmpeg.AVStream aVStream = (FFmpeg.AVStream)Marshal.PtrToStructure(this.formatContext.streams[i], typeof(FFmpeg.AVStream));
                FFmpeg.AVCodecContext aVCodecContext = (FFmpeg.AVCodecContext)Marshal.PtrToStructure(aVStream.codec, typeof(FFmpeg.AVCodecContext));
                if (aVCodecContext.codec_type == FFmpeg.CodecType.CODEC_TYPE_VIDEO)
                {
                    this.videoStream = i;
                    this.stream = aVStream;
                    this.codecCtx = aVCodecContext;
                    this.pCodecCtx = this.stream.codec;
                    break;
                }
            }
            if (this.videoStream == -1)
            {
                throw new Exception("couldn't find video stream");
            }
            this.FrameDelay = av_q2d(this.stream.time_base);
            this.pCodec = FFmpeg.avcodec_find_decoder(this.codecCtx.codec_id);
            if (this.pCodec == IntPtr.Zero)
            {
                throw new Exception("couldn't find decoder");
            }
            if (FFmpeg.avcodec_open(this.pCodecCtx, this.pCodec) < 0)
            {
                throw new Exception("couldn't open codec");
            }
            this.pFrame = FFmpeg.avcodec_alloc_frame();
            this.pFrameRGB = FFmpeg.avcodec_alloc_frame();
            if (this.pFrameRGB == IntPtr.Zero)
            {
                throw new Exception("couldn't allocate RGB frame");
            }
            int cb = FFmpeg.avpicture_get_size(6, this.codecCtx.width, this.codecCtx.height);
            this.buffer = Marshal.AllocHGlobal(cb);
            FFmpeg.avpicture_fill(this.pFrameRGB, this.buffer, 6, this.codecCtx.width, this.codecCtx.height);
            this.packet = Marshal.AllocHGlobal(57);
            for (int j = 0; j < this.BufferSize; j++)
            {
                this.FrameBuffer[j] = new byte[this.width * this.height * 4];
            }
            this.decodingThread = new Thread(new ThreadStart(this.Decode));
            this.decodingThread.IsBackground = true;
            this.decodingThread.Start();
            return true;
        }

        private void Decode()
        {
            try
            {
                while (true)
                {
                    bool flag = false;
                    lock (this)
                    {
                        while (this.writeCursor - this.readCursor < this.BufferSize && FFmpeg.av_read_frame(this.pFormatCtx, this.packet) >= 0)
                        {
                            if (Marshal.ReadInt32(this.packet, 24) == this.videoStream)
                            {
                                double num = (double)Marshal.ReadInt64(this.packet, 8);
                                IntPtr buf = Marshal.ReadIntPtr(this.packet, 16);
                                int buf_size = Marshal.ReadInt32(this.packet, 20);
                                FFmpeg.avcodec_decode_video(this.pCodecCtx, this.pFrame, ref this.frameFinished, buf, buf_size);
                                if (this.frameFinished != 0 && Marshal.ReadIntPtr(this.packet, 16) != IntPtr.Zero && FFmpeg.img_convert(this.pFrameRGB, 6, this.pFrame, (int)this.codecCtx.pix_fmt, this.codecCtx.width, this.codecCtx.height) == 0)
                                {
                                    Marshal.Copy(Marshal.ReadIntPtr(this.pFrameRGB), this.FrameBuffer[this.writeCursor % this.BufferSize], 0, this.FrameBuffer[this.writeCursor % this.BufferSize].Length);
                                    this.FrameBufferTimes[this.writeCursor % this.BufferSize] = (num - (double)this.stream.start_time) * this.FrameDelay * 1000.0;
                                    this.writeCursor++;
                                    this.lastPts = num;
                                    flag = true;
                                }
                            }
                        }
                    }
                    if (!flag)
                    {
                        Thread.Sleep(15);
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                using (StreamWriter streamWriter = File.CreateText("video-debug.txt"))
                {
                    streamWriter.WriteLine(ex.ToString());
                }
            }
        }
        public bool OpenStream(Stream inStream)
        {
            byte[] bytes = new byte[inStream.Length];
            inStream.Read(bytes, 0, (int)inStream.Length);
            return this.Open(bytes);
        }
        public byte[] GetFrame(int time)
        {
            while (this.readCursor < this.writeCursor - 1 && this.FrameBufferTimes[(this.readCursor + 1) % this.BufferSize] <= (double)time)
            {
                this.readCursor++;
            }
            if (this.readCursor < this.writeCursor)
            {
                this.currentDisplayTime = this.FrameBufferTimes[this.readCursor % this.BufferSize];
                return this.FrameBuffer[this.readCursor % this.BufferSize];
            }
            return null;
        }

        public void Seek(int time)
        {
            lock (this)
            {
                int flags = 0;
                double num = (double)time / 1000.0 / this.FrameDelay + (double)this.stream.start_time;
                if (num < this.lastPts)
                {
                    flags = 1;
                }
                FFmpeg.av_seek_frame(this.pFormatCtx, this.videoStream, (long)num, flags);
                this.readCursor = 0;
                this.writeCursor = 0;
            }
        }
        private static double av_q2d(FFmpeg.AVRational a)
        {
            return (double)a.num / (double)a.den;
        }
    }
}
