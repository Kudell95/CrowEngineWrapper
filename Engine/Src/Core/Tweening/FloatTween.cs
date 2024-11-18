using System;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core
{
    public class FloatTween : Tween
    {
    private float startValue;
    private float endValue;
    private float currentValue;
    
    private EasingFunction easingFunction;
    private Action<float> updateAction;

    public float CurrentValue => currentValue;

    public FloatTween(float startValue, float endValue, float duration, EasingFunction easingFunction, Action<float> updateAction, int loops = 0, RepeatType repeatType = RepeatType.None)
        : base(duration, loops, repeatType)
    {
        this.startValue = startValue;
        this.endValue = endValue;
        this.easingFunction = easingFunction;
        this.updateAction = updateAction;
        this.currentValue = startValue;
    }
    
  public FloatTween(float startValue, float endValue, float duration, EasingFunction easingFunction)
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
            currentValue = MathHelper.Lerp(startValue, endValue, easeT);
        }
        else
        {
            currentValue = MathHelper.Lerp(endValue, startValue, easeT);
        }

        updateAction(currentValue);
        if (elapsedTime >= duration)
        {

            if(!HandleCompletion())
            {
                currentValue = isReversal ? startValue : endValue;
                return false;
            }
            // updateAction(endValue);
            // isCompleted = true;
            // onComplete?.Invoke();
        }

        return true;
    }
    }
}