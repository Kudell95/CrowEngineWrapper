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

    public FloatTween(float startValue, float endValue, float duration, EasingFunction easingFunction, Action<float> updateAction)
        : base(duration)
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
        Console.WriteLine("updating float tween");
        if (isCompleted) return false;

        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        float t = MathHelper.Clamp(elapsedTime / duration, 0, 1);
        float easeT = easingFunction(t);

        currentValue = MathHelper.Lerp(startValue, endValue, easeT);
        updateAction(currentValue);
        if (elapsedTime >= duration)
        {
            updateAction(endValue);
            isCompleted = true;
            onComplete?.Invoke();
        }

        return !isCompleted;
    }
    }
}