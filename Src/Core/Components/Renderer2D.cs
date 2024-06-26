using CosmicCrowGames.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UntitledCardGame;

namespace CosmicCrowGames.Core.Components
{

    public class Renderer2D : Component
    {
        private SpriteBatch _spriteBatch;
        public Renderer2D(){
            
        }

        public Renderer2D(SpriteBatch spriteBatch){
            _spriteBatch = spriteBatch;
        }
        public override void Draw(GameTime gameTime)
        {
            // throw new System.NotImplementedException();;
        }

        public override void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            // throw new System.NotImplementedException();
        }

        public void RenderItem(Texture2D texture, Vector2 position, float layerDepth = 0)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: GameWrapper.Main.ScreenScaleManager.GetScaleMatrix());
            _spriteBatch.Draw(texture, Entity.transform.Position, null, Color.White, Entity.transform.Rotation, Vector2.Zero, Entity.transform.Scale, SpriteEffects.None, layerDepth);
            _spriteBatch.End();
        }
        
        public void RenderItem(Texture2D texture, Vector2 position, Color color, float layerDepth = 0)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: GameWrapper.Main.ScreenScaleManager.GetScaleMatrix());
            _spriteBatch.Draw(texture, Entity.transform.Position, null, color, Entity.transform.Rotation, Vector2.Zero, Entity.transform.Scale, SpriteEffects.None, layerDepth);
            _spriteBatch.End();
        }
        public void RenderItem(Texture2D texture, Vector2 position, Rectangle? rectangle = null, float layerDepth = 0)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: GameWrapper.Main.ScreenScaleManager.GetScaleMatrix());
            _spriteBatch.Draw(texture, Entity.transform.Position,rectangle, Color.White, Entity.transform.Rotation, Vector2.Zero, Entity.transform.Scale, SpriteEffects.None, layerDepth);
            _spriteBatch.End();
        }
        public void RenderItem(Texture2D texture, Vector2 position, Color color, Rectangle? rectangle = null, float layerDepth = 0)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: GameWrapper.Main.ScreenScaleManager.GetScaleMatrix());
            _spriteBatch.Draw(texture, Entity.transform.Position,rectangle, color, Entity.transform.Rotation, Vector2.Zero, Entity.transform.Scale, SpriteEffects.None, layerDepth);
            _spriteBatch.End();
        }


        public override void Destroy()
        {
            base.Destroy();
            // throw new System.NotImplementedException();
        }
    }
}