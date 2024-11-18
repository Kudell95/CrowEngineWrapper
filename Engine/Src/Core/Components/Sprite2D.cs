using System;
using System.ComponentModel;
using System.Diagnostics;
using CosmicCrowGames.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core.Components
{
    public class Sprite2D : Component
    {
        public Texture2D Texture;

        public int layerDepth = 1;

        public bool UseRectangle = false;

        public Rectangle ImageRectangle;

        private Color _spriteColour;

        public Color SpriteColor {
            get {
                return _spriteColour;
            }
            set
            {
                _spriteColour = value;
                CurrentColour = value;
            }

        }
        public Color CurrentColour = Color.White;

        public AnchorPoint CurrentPivotAnchor = AnchorPoint.None;

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
                Entity.TryGetComponent<Renderer2D>()?.RenderItem(Texture, Entity.transform.Position, CurrentColour, layerDepth);
            else
                Entity.TryGetComponent<Renderer2D>()?.RenderItem(Texture, Entity.transform.Position, CurrentColour, ImageRectangle, layerDepth);
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

        public Sprite2D SetPivot(AnchorPoint anchor)
        {
            Vector2 PivotPoint = Texture.Bounds.Center.ToVector2();

            switch (anchor)
            {
                case AnchorPoint.TopLeft:
                    PivotPoint = new Vector2(0, 0);
                    break;
                case AnchorPoint.TopCenter:
                    break;
                case AnchorPoint.TopRight:
                    break;
                case AnchorPoint.MiddleLeft:
                    break;
                case AnchorPoint.MiddleCenter:
                    PivotPoint = UseRectangle ? ImageRectangle.Center.ToVector2() :Texture.Bounds.Center.ToVector2();
                    break;
                case AnchorPoint.MiddleRight:
                    break;
                case AnchorPoint.BottomLeft:
                    break;
                case AnchorPoint.BottomCenter:
                    PivotPoint = new Vector2(Texture.Bounds.Center.X, Texture.Bounds.Bottom);
                    break;
                case AnchorPoint.BottomRight:
                    break;
            }
            
            CurrentPivotAnchor = anchor;
            
            Entity.transform.Pivot = PivotPoint;

            return this;
        }

        public override void Destroy()
        {
            base.Destroy();
            // throw new NotImplementedException();
        }

        public override void Dispose()
        {
            //NOTE: we should never manually dispose of Texture2d's as they are managed by the content manager, any shared content is cached and rather than reinitialising every time.
            //So memory used by texture2d shouldn't be a problem anyway.
            Texture = null;
            base.Dispose();
            
        }
    }
}

