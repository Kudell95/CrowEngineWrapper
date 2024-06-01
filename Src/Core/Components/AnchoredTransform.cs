using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core.Components
{
    public class AnchoredTransform : Component
    {
        private GraphicsDevice _graphicsDevice;
        public Vector2 LocalPosition;
        public AnchorPoint Anchor = AnchorPoint.MiddleCenter;

        private AnchoredTransform(AnchorPoint bottomCenter)
        {
            
        }


        public AnchoredTransform(GraphicsDevice graphicsDevice)
        {
            this._graphicsDevice = graphicsDevice;
        }
        public AnchoredTransform(Entity entity, GraphicsDevice graphicsDevice) : base(entity)
        {
            this._graphicsDevice = graphicsDevice;
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Initialize()
        {
            Entity.transform.Position = GetAnchoredPosition(LocalPosition);
        }

        public override void Update(GameTime gameTime)
        {   
            Entity.transform.Position = GetAnchoredPosition(LocalPosition);
        }


        public Vector2 GetAnchoredPosition(Vector2 offset)
        {
            // _viewport = GameWrapper.Main.GraphicsDevice.Viewport;

            int width = _graphicsDevice.Viewport.Width;
            int height = _graphicsDevice.Viewport.Height;


            Vector2 anchoredPosition = new Vector2();
            switch(Anchor){
                case AnchorPoint.TopLeft:
                    anchoredPosition = new Vector2(0, 0);
                    break;
                case AnchorPoint.TopCenter:
                    anchoredPosition = new Vector2(width / 2, 0);
                    break;
                case AnchorPoint.TopRight:
                    anchoredPosition = new Vector2(width, 0);
                    break;
                case AnchorPoint.MiddleLeft:
                    anchoredPosition = new Vector2(0, height / 2);
                    break;
                case AnchorPoint.MiddleCenter:
                    anchoredPosition = new Vector2(width / 2, height / 2);
                    break;
                case AnchorPoint.MiddleRight:
                    anchoredPosition = new Vector2(width, height / 2);
                    break;
                case AnchorPoint.BottomLeft:
                    anchoredPosition = new Vector2(0, height);
                    break;
                case AnchorPoint.BottomCenter:
                    anchoredPosition = new Vector2(width / 2,height);
                    break;
                case AnchorPoint.BottomRight:
                    anchoredPosition = new Vector2(width,height);
                    break;
                default:
                    break;
            }
            
            return anchoredPosition + offset;

        }
    }

    public enum AnchorPoint{
        None,
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }
}