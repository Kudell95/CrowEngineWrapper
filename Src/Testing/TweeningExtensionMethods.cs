
using System;
using System.Runtime.CompilerServices;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;

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

       public static Tween TweenTo(this Entity input, Vector2 target, float duration)
       {
            float x = input.transform.Position.X;
            FloatTween tween = new FloatTween(x, target.X, duration, Easing.Linear, (float x) => {
                Console.WriteLine("Updating value by " + x);
                input.SetPosition(new Vector2(x, input.transform.Position.Y));
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }

       public static Tween TweenTo(this Entity input, Vector2 target, float duration, EasingFunction easingFunction)
       {
            float x = input.transform.Position.X;
            FloatTween tween = new FloatTween(x, target.X, duration, easingFunction, (float x) => {
                Console.WriteLine("Updating value by " + x);
                input.SetPosition(new Vector2(x, input.transform.Position.Y));
            });
           
            Tweener.Instance.AddTween(tween);
            return tween;
       }


    }    
}