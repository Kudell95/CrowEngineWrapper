using System;
using System.Collections.Generic;
using CosmicCrowGames.Core;
using CrowEngine.Core.Components;
using CrowEngine.Core.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngine.Core.Managers;

public class CollisionManager : Manager, IDisposable
{
    private GraphicsDevice _graphicsDevice;

    private ColliderQuadTree _quadTree;

    public static Action<List<Collider2d>> OnUpdateColliders; 

    public CollisionManager(GraphicsDevice graphics)
    {
        _graphicsDevice = graphics;
        
        _quadTree = new ColliderQuadTree(0,
            new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height));
        OnUpdateColliders = null;
        OnUpdateColliders += HandleCollision;

    }
    
    public override void Initialize()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }


    private void HandleCollision(List<Collider2d> colliders)
    {
        _quadTree.Clear();
        if (colliders == null || colliders.Count == 0)
            return;

        for (int i = 0; i < colliders.Count; i++)
        {
            _quadTree.Insert(colliders[i]);
        }


        List<Collider2d> cols = new List<Collider2d>();
        for (int i = 0; i < colliders.Count; i++)
        {
            cols.Clear();
            _quadTree.Retrieve(ref cols, colliders[i]);

            for (int j = 0; j < cols.Count; j++)
            {
                colliders[i].CheckCollision(cols[j]);
            }
        }
    }
    

    public void Dispose()
    {
        _quadTree.Clear();
    }
}