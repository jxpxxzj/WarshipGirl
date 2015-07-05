using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;

namespace WarshipGirl.Utilities
{
    public static class AnimationHelper
    {
        /// <summary>  
        /// 透明度播放—循环 （提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="myFrameworkElement">透明动画对象</param>  
        /// <param name="aTime">动画时间</param>  
        /// <param name="wind">动画移动所在窗口（推荐this）</param>  
        /// <returns>返回动画对象</returns>  
        public static Storyboard Opacity_Animation(FrameworkElement myFrameworkElement, TimeSpan aTime, Window wind)
        {
            Storyboard myStoryboard = new Storyboard();
            DoubleAnimation OpacityDoubleAnimation = new DoubleAnimation();
            OpacityDoubleAnimation.From = 0;
            OpacityDoubleAnimation.To = 1;
            OpacityDoubleAnimation.Duration = aTime;
            Storyboard.SetTargetName(OpacityDoubleAnimation, myFrameworkElement.Name);
            Storyboard.SetTargetProperty(OpacityDoubleAnimation, new PropertyPath(DataGrid.OpacityProperty));
            myStoryboard.Children.Add(OpacityDoubleAnimation);
            myStoryboard.RepeatBehavior = RepeatBehavior.Forever;
            myStoryboard.Begin(wind);
            return myStoryboard;

        }
        /// <summary>  
        /// 透明度播放-可传值（起始透明度，结束透明度）（提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="myFrameworkElement">透明动画对象</param>  
        /// <param name="aTime">动画时间</param>  
        /// <param name="dFrom">起始透明度</param>  
        /// <param name="dTo">结束透明度</param>  
        /// <param name="wind">动画移动所在窗口（推荐this）</param>  
        /// <returns>返回动画对象</returns>  
        public static Storyboard Opacity_Animation(FrameworkElement myFrameworkElement, TimeSpan aTime, double dFrom, double dTo, Window wind)
        {
            Storyboard myStoryboard = new Storyboard();
            DoubleAnimation OpacityDoubleAnimation = new DoubleAnimation();
            OpacityDoubleAnimation.From = dFrom;
            OpacityDoubleAnimation.To = dTo;
            OpacityDoubleAnimation.Duration = aTime;
            Storyboard.SetTargetName(OpacityDoubleAnimation, myFrameworkElement.Name);
            Storyboard.SetTargetProperty(OpacityDoubleAnimation, new PropertyPath(DataGrid.OpacityProperty));
            myStoryboard.Children.Add(OpacityDoubleAnimation);
            //myStoryboard.RepeatBehavior = RepeatBehavior.Forever;  
            myStoryboard.Begin(wind);
            return myStoryboard;

        }
        /// <summary>  
        /// 透明度播放-可传值（起始透明度，结束透明度,次数或是否循环）（提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="myFrameworkElement">透明动画对象</param>  
        /// <param name="aTime">动画时间</param>  
        /// <param name="dFrom">起始透明度</param>  
        /// <param name="dTo">结束透明度</param>  
        /// <param name="RBehavior">动画是否循环</param>  
        /// <param name="wind">动画移动所在窗口（推荐this）</param>  
        /// <returns>返回动画对象</returns>  
        public static Storyboard Opacity_Animation(FrameworkElement myFrameworkElement, TimeSpan aTime, double dFrom, double dTo, RepeatBehavior RBehavior, Window wind)
        {
            Storyboard myStoryboard = new Storyboard();
            DoubleAnimation OpacityDoubleAnimation = new DoubleAnimation();
            OpacityDoubleAnimation.From = dFrom;
            OpacityDoubleAnimation.To = dTo;
            OpacityDoubleAnimation.Duration = aTime;
            Storyboard.SetTargetName(OpacityDoubleAnimation, myFrameworkElement.Name);
            Storyboard.SetTargetProperty(OpacityDoubleAnimation, new PropertyPath(DataGrid.OpacityProperty));
            myStoryboard.Children.Add(OpacityDoubleAnimation);
            myStoryboard.RepeatBehavior = RBehavior;
            myStoryboard.Begin(wind);
            return myStoryboard;

        }
        /// <summary>  
        /// X方向移动动画（需要移动的对象、移动时间、相对控件原始位置的起始位置、相对控件原始位置的结束位置）（提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="myFrameworkElement">需要移动的对象</param>  
        /// <param name="aTime">移动时间</param>  
        /// <param name="dFrom">相对控件原始位置的-起始位置</param>  
        /// <param name="dTo">相对控件原始位置的-结束位置</param>  
        /// <param name="wind">动画移动所在窗口（推荐this）</param>  
        /// <returns>返回动画对象</returns>  
        public static Storyboard InDouble_X_Animation(FrameworkElement myFrameworkElement, TimeSpan aTime, double dFrom, double dTo, RepeatBehavior RForever, Window wind)
        {
            myFrameworkElement.RenderTransform = new TranslateTransform();
            DependencyProperty[] propertyChain = new DependencyProperty[]  
            {  
                DataGrid.RenderTransformProperty,  
                TranslateTransform.XProperty  
            };
            Storyboard myStoryboard = new Storyboard();
            DoubleAnimation InDoubleAnimation = new DoubleAnimation();
            InDoubleAnimation.From = dFrom;
            InDoubleAnimation.To = dTo;
            InDoubleAnimation.Duration = aTime;
            Storyboard.SetTargetName(InDoubleAnimation, myFrameworkElement.Name);
            Storyboard.SetTargetProperty(InDoubleAnimation, new PropertyPath("(0).(1)", propertyChain));
            myStoryboard.Children.Add(InDoubleAnimation);
            myStoryboard.RepeatBehavior = RForever;
            myStoryboard.Begin(wind);
            return myStoryboard;
        }
        /// <summary>  
        /// Y方向移动动画（需要移动的对象、移动时间、相对控件原始位置的起始位置、相对控件原始位置的结束位置）（提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="myFrameworkElement">需要移动的对象</param>  
        /// <param name="aTime">移动时间</param>  
        /// <param name="dFrom">相对控件原始位置的-起始位置</param>  
        /// <param name="dTo">相对控件原始位置的-结束位置</param>  
        /// <param name="RBehavior">动画是否循环</param>  
        /// <param name="wind">动画移动所在窗口（推荐this）</param>  
        /// <returns>返回动画对象</returns>  
        public static Storyboard InDouble_Y_Animation(FrameworkElement myFrameworkElement, TimeSpan aTime, double dFrom, double dTo, RepeatBehavior RForever, Window wind)
        {
            myFrameworkElement.RenderTransform = new TranslateTransform();
            DependencyProperty[] propertyChain = new DependencyProperty[]  
            {  
                DataGrid.RenderTransformProperty,  
                TranslateTransform.YProperty  
            };
            Storyboard myStoryboard = new Storyboard();
            DoubleAnimation InDoubleAnimation = new DoubleAnimation();
            InDoubleAnimation.From = dFrom;
            InDoubleAnimation.To = dTo;
            InDoubleAnimation.Duration = aTime;
            Storyboard.SetTargetName(InDoubleAnimation, myFrameworkElement.Name);
            Storyboard.SetTargetProperty(InDoubleAnimation, new PropertyPath("(0).(1)", propertyChain));
            myStoryboard.Children.Add(InDoubleAnimation);
            myStoryboard.RepeatBehavior = RForever;
            myStoryboard.Begin(wind);
            return myStoryboard;
        }
        /// <summary>  
        /// 缓动缩放动画 （提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="element">缩放控件</param>  
        /// <param name="aTime">缩放时间</param>  
        /// <param name="dFrom">缩放起始值(推荐1)</param>  
        /// <param name="dTo">缩放结束值(推荐1.5)</param>  
        /// <param name="aOscillations">滑过动画目标的次数（推荐5）</param>  
        /// <param name="aSpringiness">弹簧刚度（推荐10）</param>  
        /// <returns>返回动画对象</returns>  
        public static AnimationClock ScaleEasingAnimation(FrameworkElement element, TimeSpan aTime, double dFrom, double dTo, int aOscillations, int aSpringiness)
        {
            ScaleTransform scale = new ScaleTransform();
            element.RenderTransform = scale;
            element.RenderTransformOrigin = new Point(0.5, 0.5);//定义圆心位置  
            EasingFunctionBase easing = new ElasticEase()
            {
                EasingMode = EasingMode.EaseOut,            //公式  
                Oscillations = aOscillations,                           //滑过动画目标的次数  
                Springiness = aSpringiness                             //弹簧刚度  
            };
            DoubleAnimation scaleAnimation = new DoubleAnimation()
            {
                From = dFrom,                                 //起始值  
                To = dTo,                                     //结束值  
                EasingFunction = easing,                    //缓动函数  
                Duration = aTime    //动画播放时间  
            };
            AnimationClock clock = scaleAnimation.CreateClock();
            scale.ApplyAnimationClock(ScaleTransform.ScaleXProperty, clock);
            scale.ApplyAnimationClock(ScaleTransform.ScaleYProperty, clock);
            return clock;
        }
        /// <summary>  
        /// 缩放匀速动画 （提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="sender">缩放控件</param>  
        /// <param name="scaleXF">缩放X开始值</param>  
        /// <param name="scaleXT">缩放X结束值</param>  
        /// <param name="scaleYF">缩放Y开始值</param>  
        /// <param name="scaleYT">缩放Y结束值</param>  
        /// <param name="aTime">动画持续时间</param>  
        /// <param name="RBehavior">动画是否循环</param>  
        /// <param name="wind">动画移动所在窗口（推荐this）</param>  
        /// <returns>返回动画对象</returns>  
        public static Storyboard ScaleTransformAnimation(UIElement sender, double scaleXF, double scaleXT, double scaleYF, double scaleYT, TimeSpan aTime, RepeatBehavior RForever, Window wind)
        {
            DoubleAnimationUsingKeyFrames dba_SacleX = new DoubleAnimationUsingKeyFrames();
            dba_SacleX.KeyFrames.Add(new EasingDoubleKeyFrame(scaleXF, TimeSpan.FromSeconds(0)));
            dba_SacleX.KeyFrames.Add(new EasingDoubleKeyFrame(scaleXT, aTime));

            DoubleAnimationUsingKeyFrames dba_SacleY = new DoubleAnimationUsingKeyFrames();
            dba_SacleY.KeyFrames.Add(new EasingDoubleKeyFrame(scaleYF, TimeSpan.FromSeconds(0)));
            dba_SacleY.KeyFrames.Add(new EasingDoubleKeyFrame(scaleYT, aTime));

            Storyboard mystoryboard = new Storyboard();
            Storyboard.SetTarget(dba_SacleX, sender);
            sender.RenderTransform = new ScaleTransform();
            Storyboard.SetTargetProperty(dba_SacleX, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            mystoryboard.Children.Add(dba_SacleX);
            Storyboard.SetTarget(dba_SacleY, sender);
            Storyboard.SetTargetProperty(dba_SacleY, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            mystoryboard.Children.Add(dba_SacleY);
            mystoryboard.RepeatBehavior = RForever;
            mystoryboard.Begin(wind);
            return mystoryboard;
        }
        /// <summary>  
        /// 旋转动画（初始点X和Y是按元素的左上角）（提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="sender">动画对象</param>  
        /// <param name="centerX">旋转X原点</param>  
        /// <param name="centerY">旋转Y原点</param>  
        /// <param name="angle">旋转角度（推荐0-360）</param>  
        /// <param name="aTime">动画时间</param>  
        /// <param name="RBehavior">动画是否循环</param>  
        /// <param name="wind">动画移动所在窗口（推荐this）</param>  
        /// <returns>返回动画对象</returns>  
        public static Storyboard RotateTransformAnimation(UIElement sender, TimeSpan aTime, double centerX, double centerY, double angle, RepeatBehavior RForever, Window wind)
        {

            RotateTransform rt = new RotateTransform();
            rt.CenterX = centerX;
            rt.CenterY = centerY;
            sender.RenderTransform = rt;
            DoubleAnimationUsingKeyFrames dba = new DoubleAnimationUsingKeyFrames();
            dba.KeyFrames.Add(new LinearDoubleKeyFrame(angle, aTime));
            Storyboard mysb = new Storyboard();
            Storyboard.SetTarget(dba, sender);
            Storyboard.SetTargetProperty(dba, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            mysb.Children.Add(dba);
            mysb.RepeatBehavior = RForever;
            mysb.Begin(wind);
            return mysb;
        }
        /// <summary>  
        /// 旋转动画（初始点按元素的中心）（提升效率）Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 20 });  
        /// </summary>  
        /// <param name="element">动画对象</param>  
        /// <param name="angle">旋转角度（推荐0-360）</param>  
        /// <param name="aTime">动画时间</param>  
        /// <param name="RBehavior">动画是否循环</param>  
        /// <param name="wind">动画移动所在窗口（推荐this）</param>  
        /// <returns>返回动画对象</returns>  
        public static Storyboard RotateTransformAnimation(UIElement element, TimeSpan aTime, double angle, RepeatBehavior RForever, Window wind)
        {
            RotateTransform scale = new RotateTransform();
            element.RenderTransform = scale;
            element.RenderTransformOrigin = new Point(0.5, 0.5);//定义圆心位置  

            //RotateTransform rt = new RotateTransform();  
            //rt.CenterX = 0;  
            //rt.CenterY = 0;  
            //element.RenderTransform = rt;  
            DoubleAnimationUsingKeyFrames dba = new DoubleAnimationUsingKeyFrames();
            dba.KeyFrames.Add(new LinearDoubleKeyFrame(angle, aTime));
            Storyboard mysb = new Storyboard();
            Storyboard.SetTarget(dba, element);
            Storyboard.SetTargetProperty(dba, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            mysb.Children.Add(dba);
            mysb.RepeatBehavior = RForever;
            mysb.Begin(wind);
            return mysb;
        }
    }
}