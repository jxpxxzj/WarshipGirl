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
    /// <summary>
    /// 动画循环模式
    /// </summary>
    public enum LoopMode
    {
        /// <summary>
        /// 播放一次
        /// </summary>
        Once,
        /// <summary>
        /// 无限循环
        /// </summary>
        Forever,
        /// <summary>
        /// 播放有限次
        /// </summary>
        Limited
    }
    /// <summary>
    /// 动画
    /// </summary>
    public class Animation 
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        private GameTime present { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public GameTime BeginTime { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// 开始值
        /// </summary>
        public int BeginValue { get; set; }
        /// <summary>
        /// 结束值
        /// </summary>
        public int EndValue { get; set; }
        /// <summary>
        /// 缓动函数
        /// </summary>
        public BaseEasing EasingFunction { get; set; }
        /// <summary>
        /// 要绑定到的Sprite
        /// </summary>
        public Sprite TargetSprite { get; set; }
        /// <summary>
        /// 要绑定到的属性
        /// </summary>
        public PropertyInfo TargetProperty { get; set; }

        /// <summary>
        /// 循环次数
        /// </summary>
        public int LoopCount { get; set; }
        /// <summary>
        /// 循环间隔
        /// </summary>
        public TimeSpan LoopDelay { get; set; }
        /// <summary>
        /// 循环模式
        /// </summary>
        public LoopMode LoopMode { get; set; }
        /// <summary>
        /// 播完是否返回开头
        /// </summary>
        public bool ReturnToBeginning { get; set; }
        /// <summary>
        /// 播完后是否倒放
        /// </summary>
        public bool PlayBack { get; set; }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool isRunning { get; set; }
        /// <summary>
        /// 是否播放结束
        /// </summary>
        public bool Finished { get; set; }

        /// <summary>
        /// 播放时间
        /// </summary>
        public TimeSpan RunningTime
        {
            get
            {
                return present.TotalGameTime - BeginTime.TotalGameTime;
            }
        }
        /// <summary>
        /// 播放进度
        /// </summary>
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
