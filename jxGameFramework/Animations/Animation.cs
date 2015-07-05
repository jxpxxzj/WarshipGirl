using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Xna.Framework;
using jxGameFramework.Components;
using jxGameFramework.Animations.Curve;

namespace jxGameFramework.Animations
{
    public enum LoopMode
    {
        Once,Forever,Limited
    }
    public class Animation 
    {
        private GameTime present { get; set; }
        public GameTime BeginTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int BeginValue { get; set; }
        public int EndValue { get; set; }
        public BaseEasing EasingFunction { get; set; }
        public Sprite TargetSprite { get; set; }
        public PropertyInfo TargetProperty { get; set; }

        public int LoopCount { get; set; }
        public TimeSpan LoopDelay { get; set; }
        public LoopMode LoopMode { get; set; }
        public bool ReturnToBeginning { get; set; }
        public bool PlayBack { get; set; }


        public bool isRunning { get; set; }
        public bool Finished { get; set; }

        public TimeSpan RunningTime
        {
            get
            {
                return present.TotalGameTime - BeginTime.TotalGameTime;
            }
        }
        public double Progress
        {
            get
            {
                return RunningTime.TotalMilliseconds / Duration.TotalMilliseconds;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            present = gameTime;
            if (gameTime.TotalGameTime > BeginTime.TotalGameTime)
            {
                if ((gameTime.TotalGameTime < (BeginTime.TotalGameTime + Duration)) && !Finished)
                {
                    isRunning = true;
                    var presentvalue = (int)(BeginValue + (EasingFunction.EasingCurve(Progress,PlayBack) * (EndValue - BeginValue)));
                    TargetProperty.SetValue(TargetSprite, presentvalue,null);
                }
                else
                {
                    isRunning = false;
                    Finished = true;
                }
            }
            if ((LoopCount > 1 || LoopMode==LoopMode.Forever) && Finished && (LoopMode!=LoopMode.Once))
            {
                LoopCount--;
                BeginTime=new GameTime(gameTime.TotalGameTime,gameTime.ElapsedGameTime);
                BeginTime.TotalGameTime += LoopDelay;
                if(ReturnToBeginning)
                    TargetProperty.SetValue(TargetSprite, (int)BeginValue,null);
                isRunning = true;
                Finished = false;
                return;
            }         
        }
    }
}
