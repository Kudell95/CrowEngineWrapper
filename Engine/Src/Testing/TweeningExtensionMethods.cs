
using System;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;
using CosmicCrowGames.Core.Transform;
using CosmicCrowGames.Core.Components;
namespace CosmicCrowGames.Core
{
    public static class TweenExtensions{
       public static Tween TweenTo(this float input,float target, float duration)
       {    
            FloatTween tween = new FloatTween(input, target, duration, Easing.Linear, (float x) => {
                input = x;
            });
            Tweener.Instance.AddTween(tween);
            return tween;
       } 

       public static Tween TweenPosition(this Transform2D input, Vector2 target, float duration)
       {
            Vector2Tween tween = new Vector2Tween(input.Position, target, duration, Easing.Linear, (Vector2 x) => {
                // Console.WriteLine("Updating value by " + x);
                input.Position = x;
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }

       public static Tween TweenPosition(this Transform2D input, Vector2 target, float duration, EasingFunction easingFunction)
       {
            Vector2Tween tween = new Vector2Tween(input.Position, target, duration, easingFunction, (Vector2 x) => {
                // Console.WriteLine("Updating value by " + x);
                input.Position = x;
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }
 
       public static Tween TweenPosition(this Transform2D input, Vector2 target, float duration, EasingFunction easingFunction, int loops, RepeatType repeatType)
       {
            Vector2Tween tween = new Vector2Tween(input.Position, target, duration, easingFunction, (Vector2 x) => {
                input.Position = x;
            },loops, repeatType);
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }

        public static Tween TweenScale(this Transform2D input, Vector2 target, float duration)
       {
            Vector2Tween tween = new Vector2Tween(input.Scale, target, duration, Easing.Linear, (Vector2 x) => {
                // Console.WriteLine("Updating value by " + x);
                input.Scale = x;
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }

       public static Tween TweenScale(this Transform2D input, Vector2 target, float duration, EasingFunction easingFunction)
       {
            Vector2Tween tween = new Vector2Tween(input.Scale, target, duration, easingFunction, (Vector2 x) => {
                // Console.WriteLine("Updating value by " + x);
                input.Scale = x;
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }
 
       public static Tween TweenScale(this Transform2D input, Vector2 target, float duration, EasingFunction easingFunction, int loops, RepeatType repeatType)
       {
            Vector2Tween tween = new Vector2Tween(input.Scale, target, duration, easingFunction, (Vector2 x) => {
                input.Scale = x;
            },loops, repeatType);
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }

       public static Tween TweenRotation(this Transform2D input, float target, float duration)
       {
            FloatTween tween = new FloatTween(input.Rotation, target, duration, Easing.Linear, (float x) => {
                // Console.WriteLine("Updating value by " + x);
                input.Rotation = x;
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }

       public static Tween TweenRotation(this Transform2D input, float target, float duration, EasingFunction easingFunction)
       {
            FloatTween tween = new FloatTween(input.Rotation, target, duration, easingFunction, (float x) => {
                // Console.WriteLine("Updating value by " + x);
                input.Rotation = x;
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }
 
       public static Tween TweenRotation(this Transform2D input, float target, float duration, EasingFunction easingFunction, int loops, RepeatType repeatType)
       {
            FloatTween tween = new FloatTween(input.Rotation, target, duration, easingFunction, (float x) => {
                input.Rotation = x;
            },loops, repeatType);
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }

       public static Tween TweenColor(this Sprite2D input, Color target, float duration)
       {
            Vector4Tween tween = new Vector4Tween(input.CurrentColour.ToVector4(), target.ToVector4(), duration, Easing.Linear, (Vector4 x) => {
                input.CurrentColour = new Color(x);
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }

       public static Tween TweenColor(this Sprite2D input, Color target, float duration, EasingFunction easingFunction)
       {
            Vector4Tween tween = new Vector4Tween(input.CurrentColour.ToVector4(), target.ToVector4(), duration, easingFunction, (Vector4 x) => {
                input.CurrentColour = new Color(x);
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }
        public static Tween TweenColor(this Sprite2D input, Color target, float duration, EasingFunction easingFunction, int loops, RepeatType repeatType)
        {
                Vector4Tween tween = new Vector4Tween(input.CurrentColour.ToVector4(), target.ToVector4(), duration, easingFunction, (Vector4 x) => {
                    input.CurrentColour = new Color(x);
                }, loops, repeatType);
            
                Tweener.Instance.AddTween(tween);
                return tween;
        }
    }    
}