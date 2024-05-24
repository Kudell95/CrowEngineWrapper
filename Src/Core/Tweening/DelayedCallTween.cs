using System;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core.Tweening
{
    public class DelayedCall: Tween{

        public DelayedCall(float _duration, Tweener tweener) : base(_duration)
        {
            tweener.AddTween(this);
        }        

        public static Tween CreateDelayedCall(float duration)
        {
            return new DelayedCall(duration, Tweener.Instance);
        }


        public override bool Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime >= duration)
            {
                isCompleted = true;
                onComplete?.Invoke();
                return false;
            }

            return true;
        }
    }
}