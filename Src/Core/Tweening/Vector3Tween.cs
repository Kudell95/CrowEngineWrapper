
using System;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core
{
    public class Vector3Tween : Tween
    {
    private Vector3 startValue;
    private Vector3 endValue;
    private Vector3 currentValue;
    
    private EasingFunction easingFunction;
    private Action<Vector3> updateAction;

    public Vector3 CurrentValue => currentValue;

    public Vector3Tween(Vector3 startValue, Vector3 endValue, float duration, EasingFunction easingFunction, Action<Vector3> updateAction, int loops = 0, RepeatType repeatType = RepeatType.None)
        : base(duration, loops, repeatType)
    {
        this.startValue = startValue;
        this.endValue = endValue;
        this.easingFunction = easingFunction;
        this.updateAction = updateAction;
        this.currentValue = startValue;
    }
    
  public Vector3Tween(Vector3 startValue, Vector3 endValue, float duration, EasingFunction easingFunction)
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
            currentValue = new Vector3(MathHelper.Lerp(startValue.X, endValue.X, easeT), MathHelper.Lerp(startValue.Y, endValue.Y, easeT), MathHelper.Lerp(startValue.Z, endValue.Z, easeT));
        }
        else
        {
            currentValue = new Vector3(MathHelper.Lerp(endValue.X, startValue.X, easeT), MathHelper.Lerp(endValue.Y, startValue.Y, easeT), MathHelper.Lerp(endValue.Z, startValue.Z, easeT));
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