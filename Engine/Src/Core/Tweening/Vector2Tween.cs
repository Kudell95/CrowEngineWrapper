
using System;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core
{
    public class Vector2Tween : Tween
    {
    private Vector2 startValue;
    private Vector2 endValue;
    private Vector2 currentValue;
    
    private EasingFunction easingFunction;
    private Action<Vector2> updateAction;

    public Vector2 CurrentValue => currentValue;

    public Vector2Tween(Vector2 startValue, Vector2 endValue, float duration, EasingFunction easingFunction, Action<Vector2> updateAction, int loops = 0, RepeatType repeatType = RepeatType.None)
        : base(duration, loops, repeatType)
    {
        this.startValue = startValue;
        this.endValue = endValue;
        this.easingFunction = easingFunction;
        this.updateAction = updateAction;
        this.currentValue = startValue;
    }
    
  public Vector2Tween(Vector2 startValue, Vector2 endValue, float duration, EasingFunction easingFunction)
        : base(duration)
    {
        this.startValue = startValue;
        this.endValue = endValue;
        this.easingFunction = easingFunction;        
        this.currentValue = startValue;
    }
  

    public override bool Update(GameTime gameTime)
    {
        if (isCompleted) return false;

        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        float t = MathHelper.Clamp(elapsedTime / duration, 0, 1);
        float easeT = easingFunction(t);
        if(!isReversal)
        {
            currentValue = new Vector2(MathHelper.Lerp(startValue.X, endValue.X, easeT), MathHelper.Lerp(startValue.Y, endValue.Y, easeT));
        }
        else
        {
            currentValue = new Vector2(MathHelper.Lerp(endValue.X, startValue.X, easeT), MathHelper.Lerp(endValue.Y, startValue.Y, easeT));
        }

        updateAction(currentValue);
        if (elapsedTime >= duration)
        {

            if(!HandleCompletion())
            {
                currentValue = isReversal ? startValue : endValue;
                return false;
            }
        }

        return true;
    }
    }
}