using System;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core.Tweening
{
    public class PropertyTween<T> : Tween
    {
       private Func<T> getter;
        private Action<T> setter;
        private T startValue;
        private T endValue;
        private EasingFunction easingFunction;

        public PropertyTween(Func<T> getter, Action<T> setter, T endValue, float duration, EasingFunction easingFunction) 
            : base(duration)
        {
            this.getter = getter;
            this.setter = setter;
            this.startValue = getter();
            this.endValue = endValue;
            this.easingFunction = easingFunction;
        }

        public override bool Update(GameTime gameTime)
        {
            if (isCompleted) return false;

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float t = MathHelper.Clamp(elapsedTime / duration, 0, 1);
            float easeT = easingFunction(t);

            // Interpolation logic based on type
            setter(Interpolate(startValue, endValue, easeT));

            if (elapsedTime >= duration)
            {
                setter(endValue);
                isCompleted = true;
                onComplete?.Invoke();
            }

            return !isCompleted;
        }

        private T Interpolate(T start, T end, float t)
        {
            // Implement interpolation logic based on T type (Vector2, float, etc.)
            // For example, if T is float:
            //TODO: add more types here.
            return (T)(object)MathHelper.Lerp((float)(object)start, (float)(object)end, t);
        }
    }
}