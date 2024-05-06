using System;
using CosmicCrowGames.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core.Components
{
    public class Sprite2D : Component
    {
        public Texture2D Texture;

        public int layerDepth = 0;

        public bool UseRectangle = false;

        public Rectangle ImageRectangle;

        public Sprite2D() : base(){}

        public Sprite2D(Texture2D texture) : base()
        {
            Texture = texture;    
        }               

        public Sprite2D(Texture2D texture, int layer) : base()
        {
            Texture = texture;    
            layerDepth = layer;
        }

        public Sprite2D(Texture2D texture, Entity entity) : base(entity)
        {
            Texture = texture;
        }

        public Sprite2D(Entity entity) : base(entity)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            // _spriteBatch.Draw(Texture, Entity.transform.Position,null, Color.White, Entity.transform.Rotation, Vector2.Zero, Entity.transform.Scale, SpriteEffects.None, layerDepth);
            
            if(!UseRectangle)
                Entity.TryGetComponent<Renderer2D>()?.RenderItem(Texture, Entity.transform.Position, layerDepth);
            else
                Entity.TryGetComponent<Renderer2D>()?.RenderItem(Texture, Entity.transform.Position, ImageRectangle, layerDepth);
        }
        
        

        public override void Initialize()
        {
            // throw new System.NotImplementedException();

            if(!Entity.HasComponent<Renderer2D>())
                Console.WriteLine("WARNING: Entity does not have a Renderer2D component");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Destroy()
        {
            base.Destroy();
            // throw new NotImplementedException();
        }
    }
}

