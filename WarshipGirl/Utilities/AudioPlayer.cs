using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace WarshipGirl.Utilities
{
    class AudioPlayer : IDisposable
    {
        public AudioPlayer(AudioStream stream)
        {
            Stream = stream;
            Paused = false;
            //stopproc = new SYNCPROC(OnStop);
        }
        public AudioPlayer() { }
        public AudioStream Stream
        {
            get
            {
                return _stream;
            }
            set
            {
                //Bass.BASS_ChannelRemoveSync(value.StreamNumber, _sync);
                _stream = value;
                //_sync=Bass.BASS_ChannelSetSync(value.StreamNumber, BASSSync.BASS_SYNC_END, 0, stopproc, IntPtr.Zero);
            }
        }
        public bool Paused { get; protected set; }
        private float _volume
        {
            get
            {
                float val = 0;
                Bass.BASS_ChannelGetAttribute(Stream.StreamNumber, BASSAttribute.BASS_ATTRIB_VOL, ref val);
                return val;
            }
            set
            {
                Bass.BASS_ChannelSetAttribute(Stream.StreamNumber, BASSAttribute.BASS_ATTRIB_VOL, value);
            }
        }
        public float Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
            }
        }
        private AudioStream _stream;
        //private int _sync;

        public void Play(bool restart)
        {
            //_sync = Bass.BASS_ChannelSetSync(Stream.StreamNumber, BASSSync.BASS_SYNC_END, 0, stopproc, IntPtr.Zero);
            Bass.BASS_ChannelPlay(Stream.StreamNumber, restart);
            _volume = Volume;
            Paused = false;

        }
        public void Pause()
        {
            Bass.BASS_ChannelPause(Stream.StreamNumber);
            Paused = true;
        }
        public void Stop()
        {
            Bass.BASS_ChannelStop(Stream.StreamNumber);
            Stream.Position = 0;
            Paused = true;
        }
        void IDisposable.Dispose()
        {
            Paused = true;
            Bass.BASS_StreamFree(Stream.StreamNumber);
            Bass.BASS_Stop();
            Bass.BASS_Free();
        }
    }
    public class AudioStream : IDisposable
    {
        public int StreamNumber { get; set; }
        public string StreamName { get; protected set; }
        public AudioStream(string FileName,bool Loop = false)
        {
            if(Loop)
                StreamNumber = Bass.BASS_StreamCreateFile(FileName, 0L, 0L, BASSFlag.BASS_SAMPLE_LOOP);
            else
                StreamNumber = Bass.BASS_StreamCreateFile(FileName, 0L, 0L, BASSFlag.BASS_SAMPLE_FX);
            StreamName = FileName;
            //Length = new TimeSpan(0,0,(int)Bass.BASS_ChannelBytes2Seconds(StreamNumber,Bass.BASS_ChannelGetLength(StreamNumber)));
        }
        public AudioStream(int StreamNum)
        {
            StreamNumber = StreamNum;
        }

        public double Length
        {
            get
            {
                return Bass.BASS_ChannelBytes2Seconds(StreamNumber, Bass.BASS_ChannelGetLength(StreamNumber));
            }
        }
        public double Position
        {
            get
            {
                return Bass.BASS_ChannelBytes2Seconds(StreamNumber, Bass.BASS_ChannelGetPosition(StreamNumber));
            }
            set
            {
                Bass.BASS_ChannelSetPosition(StreamNumber, value);
            }
        }
        public float Frequence
        {
            get
            {
                float tofre = 0;
                Bass.BASS_ChannelGetAttribute(StreamNumber, BASSAttribute.BASS_ATTRIB_FREQ, ref tofre);
                return tofre;
            }
            set
            {
                Bass.BASS_ChannelSetAttribute(StreamNumber, BASSAttribute.BASS_ATTRIB_FREQ, value);
            }
        }

        public void Dispose()
        {
            Bass.BASS_StreamFree(StreamNumber);
        }
    }
}
