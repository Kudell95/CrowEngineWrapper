using System;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core.Tweening
{
    //TODO: add repeating/reverse - ping-pong etc...
    //TODO: add support for more types. sprite color, etc.
    public abstract class Tween
    {
        protected float duration;
        protected float elapsedTime;
        protected bool isCompleted;
        protected Action onComplete;

        public Tween(){}
        public Tween(float _duration)
        {
            duration = _duration;
        }

        public Tween OnComplete(Action _onComplete)
        {
            onComplete = _onComplete;
            return this;
        }

        public abstract bool Update(GameTime gameTime);
    }

    public delegate float EasingFunction(float t);

    public static class Easing
    {
        public static float Linear(float t) => t;
        public static float EaseInQuad(float t) => t*t;
        public static float EaseOutQuad(float t) => t * (2-t);

        public static float EaseInBack(float t) {
            float c1 = 1.70158f;
            float c3 = c1 + 1;

            return c3 * t * t *t - c1 * t * t;
        }

        public static float EaseInOutBack(float t){
            float c1 = 1.70158f;
            float c3 = c1 * 1.525f;

            var result = t < 0.5 
            ? (Math.Pow(2 * t, 2) * ((c3 + 1) * 2 * t - c3)) / 2
            : (Math.Pow(2 * t - 2, 2) * ((c3 + 1) * (t * 2 - 2) + c3) + 2) / 2;

            return (float)result;
        }

        public static float EaseInOutElastic(float t){
            var c5 = (2 * Math.PI) / 4.5;

            var result = t == 0
            ? 0
            : t == 1
            ? 1
            : t < 0.5
            ? -(Math.Pow(2, 20 * t - 10) * Math.Sin((20 * t - 11.125) * c5)) / 2
            : (Math.Pow(2, -20 * t + 10) * Math.Sin((20 * t - 11.125) * c5)) / 2 + 1;

            return (float) result;
        }
        public static float EaseOutCirc(float t){
            return (float)Math.Sqrt(1 - Math.Pow(t - 1, 2));
        }

        public static float EaseInCirc(float t){
            return 1 - (float)Math.Sqrt(1 - Math.Pow(t, 2));
        }

        public static float EaseInOutCubic(float t){
            if(t < 0.5)
                return 4 * t * t * t;
            else
                return 1 - (float)Math.Pow(-2 * t + 2, 3) / 2;
        }

        public static float EaseOutBounce(float t){
            float n1 = 7.5625f;
            float d1 = 2.75f;
            if (t < 1 / d1)
            {
                return n1 * t * t;
            }
            else if (t < 2 / d1)
            {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            }
            else if (t < 2.5 / d1)
            {
                return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            }
            else {
                return n1 * (t -= 2.625f / d1) * t + 0.984375f;
            }
        }

        public static float EaseInOutBounce(float t){
            return t < 0.5f 
            ? (1 - EaseOutBounce(1 - 2 * t)) / 2
            : (1 + EaseOutBounce(2 * t - 1)) / 2;
        }

    }
}