using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace jxGameFramework.Media
{
    /// <summary>
    /// 音频播放器
    /// </summary>
    public class AudioPlayer : IDisposable
    {
        public AudioPlayer(AudioStream stream)
        {
            Stream = stream;
            Paused = false;
        }
        public AudioPlayer()
        {
            
        }
        public AudioStream Stream { get; set; }
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
        public void Play(bool restart)
        {
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
    /// <summary>
    /// 音频流
    /// </summary>
    public class AudioStream : IDisposable
    {
        public int StreamNumber { get; set; }
        public string StreamName { get; protected set; }
        public AudioStream(string FileName, bool Loop = false)
        {
            if (Loop)
                StreamNumber = Bass.BASS_StreamCreateFile(FileName, 0L, 0L, BASSFlag.BASS_SAMPLE_LOOP);
            else
                StreamNumber = Bass.BASS_StreamCreateFile(FileName, 0L, 0L, BASSFlag.BASS_SAMPLE_FX);
            StreamName = FileName;    
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
