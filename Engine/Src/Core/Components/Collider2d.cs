using System;
using System.Collections.Generic;
using CosmicCrowGames.Core;
using Microsoft.Xna.Framework;

namespace CrowEngine.Core.Components;



//TODO: May need some refactoring in the future.
public class Collider2d : Component
{
    //public Rectangle Bounds;

    #region private variables

    private float m_Width;
    private float m_Height;

    #endregion


    public float Width
    {
        get { return m_Width; }
        set { m_Width = value; }
    }

    public float Height
    {
        get { return m_Height; }
        set { m_Height = value; }
    }

    public Vector2 PositionOffset; //TODO: impl this.
    
    
    
    public bool IsTrigger;

    public Action<Collider2d> OnCollisionEnter;
    public Action<Collider2d> OnCollision;
    public Action<Collider2d> OnCollisionExit;

    private List<string> _activeCollisions = new List<string>();
    

    public Collider2d(Vector2 Bounds)
    {
        Width = Bounds.X;
        Height = Bounds.Y;
    }

    public Collider2d(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public override void Initialize()
    {
        _activeCollisions = new List<string>();
    }

    public override void Update(GameTime gameTime)
    {
        
    }

    public override void Draw(GameTime gameTime)
    {
        
    }

    public override void LateUpdate(GameTime gameTime)
    {
       
    }

    public virtual void CheckCollision(Collider2d other)
    {
        Console.WriteLine($"Checking collision with: {other.Entity.ID}");
        if (other.Entity.ID.Equals(this.Entity.ID)) return;
        
        if (Intersects(other))
        {
            if (_activeCollisions.Contains(other.Entity.ID))
            {
                OnCollision?.Invoke(other);
            }
            else
            {
                _activeCollisions.Add(other.Entity.ID);
                OnCollisionEnter?.Invoke(other);
            }
        }
        else
        {
            if (_activeCollisions.Contains(other.Entity.ID))
            {
                _activeCollisions.Remove(other.Entity.ID);
                OnCollisionExit?.Invoke(other);
                
            }
        }
    }


    public virtual bool Intersects(Collider2d collider)
    {
        if (Entity == null || collider?.Entity == null)
            return false;
        
        
        var posA = this.Entity.transform.Position;
        var posB = collider.Entity.transform.Position;

        return (
            posA.X < posB.X + collider.Width &&
            posA.X + this.Width > posB.X &&
            posA.Y < posB.Y + collider.Height &&
            posA.Y + this.Height > posB.Y
        ); 
        
    }
    
}