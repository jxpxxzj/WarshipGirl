using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework;
using jxGameFramework.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace jxGameFramework.Media
{
    //TODO: buggy
    //TODO: remake with libVLC
    public class VideoPlayerBase : Control
    {
        public readonly int BufferSize = 60;
        protected Control Surface { get; set; }
        Stopwatch timer = new Stopwatch();
        public virtual double Position
        {
            get
            {
                return timer.ElapsedMilliseconds / 1000;
            }
            set
            {
                //TODO: algorithm
                throw new NotImplementedException();
            }
        }
        public virtual double Length
        {
            get
            {
                return Decoder.Length;
            }
        }
        public string FileName { get; set; }
        private bool _autosize = true;
        public VideoDecoder Decoder { get; protected set; }
        public VideoPlayerBase (string file,bool asize=true)
        {
            FileName = file;
            _autosize = asize;
            Decoder = new VideoDecoder(BufferSize);
            Decoder.Open(FileName);
            if (_autosize)
            {
                Width = Decoder.width;
                Height = Decoder.height;
            }
        }
        public override void Initialize()
        {

            Surface = new Control()
            {
                Width = this.Width,
                Height = this.Height,
            };
            Surface.Texture = new Texture2D(GraphicsDevice, Decoder.width, Decoder.height, false, SurfaceFormat.Bgr32);
            Controls.Add(Surface);
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            SetProgress();
            base.Update(gameTime);
        }
        protected virtual void SetProgress()
        {
            var data = Decoder.GetFrame((int)timer.ElapsedMilliseconds);
            if (data != null)
                Surface.Texture.SetData(data);
        }
        public virtual void Play(bool restart = true)
        {
            //TODO: buggy
            if (restart)
                timer.Restart();
            else
                timer.Start();
        }
        public virtual void Pause()
        {
            timer.Stop();
        }
        public virtual void Stop()
        {
            timer.Reset();
        }
    }
    public class VideoPlayer : VideoPlayerBase
    {
        public event EventHandler PlayEnd;
        public VideoPlayer(string file,bool autosize = true) :base(file,autosize)
        {
            AudioStream = new AudioStream(file);
            if (AudioStream.Length == -1)
                AudioStream = null;
            else
                _player = new AudioPlayer(AudioStream);
        }
        //TODO: audio & video offset
        public VideoPlayer(string audio,string file,bool autosize = true) : base(file,autosize)
        {
            AudioStream = new AudioStream(audio);
            _player = new AudioPlayer(AudioStream);
        }
        public AudioStream AudioStream { get; set; }
        AudioPlayer _player;
        public override double Position
        {
            get
            {
                if (AudioStream != null)
                    return AudioStream.Position;
                else
                    return base.Position;
            }
            set
            {
                if (AudioStream != null)
                    AudioStream.Position = value;
                else
                    base.Position = value;
            }
        }
        public override double Length
        {
            get
            {
                if (AudioStream != null)
                    return Math.Min(AudioStream.Length, Decoder.Length);
                else
                    return Decoder.Length;
            }
        }
        protected override void SetProgress()
        {
            if (AudioStream != null)
            {
                var data = Decoder.GetFrame((int)(AudioStream.Position * 1000));
                if (data != null)
                    Surface.Texture.SetData(data);
            }
            else
                base.SetProgress();
        }
        protected void OnPlayEnd(object sender,EventArgs e)
        {
            if (PlayEnd != null)
                PlayEnd(sender, e);
        }
        public override void Play(bool restart = true)
        {
            if(_player!=null)
                _player.Play(true);
            base.Play(restart);
        }
        public override void Pause()
        {
            if (_player != null)
                _player.Pause();
            base.Pause();
        }
        public override void Stop()
        {
            if (_player != null)
                _player.Stop();
            base.Stop();
            OnPlayEnd(this,EventArgs.Empty);
        }
        public override void Update(GameTime gameTime)
        {
            if (Position >= Length)
                Stop();
            base.Update(gameTime);
        }
    }

}
