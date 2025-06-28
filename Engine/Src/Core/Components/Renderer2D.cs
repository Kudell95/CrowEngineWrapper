using System;
using CosmicCrowGames.Core;
using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UntitledCardGame;

namespace CosmicCrowGames.Core.Components
{

    public class Renderer2D : Component
    {
        protected SpriteBatch _spriteBatch;

        public bool UseScaling { get; set; } = false;
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
            RenderItem(texture,position, Color.White, null, layerDepth);
        }
        
        public void RenderItem(Texture2D texture, Vector2 position, Color color, float layerDepth = 0)
        {
            RenderItem(texture,position,color,null, layerDepth);
        }
        public void RenderItem(Texture2D texture, Vector2 position, Rectangle? rectangle = null, float layerDepth = 0)
        {
            RenderItem(texture,position, Color.White, rectangle, layerDepth);
        }
        public void RenderItem(Texture2D texture, Vector2 position, Color color, Rectangle? rectangle = null, float layerDepth = 0)
        {
            BeginDraw();
            _spriteBatch.Draw(texture, Entity.transform.Position,rectangle, color,
                Entity.transform.Rotation, Vector2.Zero, Entity.transform.Scale,
                SpriteEffects.None, layerDepth);
            EndDraw();

            
        }


        public void RenderGUIBuffer(Texture2D texture,  Color colour, Rectangle rectangle, float layerdepth)
        {
            BeginGUIBufferDraw();
            _spriteBatch.Draw(texture, Entity.transform.Position,rectangle,colour,Entity.transform.Rotation, Vector2.Zero,Entity.transform.Scale,SpriteEffects.None,layerdepth);
            EndDraw();
        }

        public void RenderItem(RichTextLayout richTextLayout, Vector2 position, Color textColour, float layerDepth = 0)
        {
            BeginDraw();
            richTextLayout.Draw(_spriteBatch, position, textColour); 
            EndDraw();
        }


        protected void BeginDraw()
        {
            if(UseScaling)
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: GameWrapper.Main.ScreenScaleManager.GetScaleMatrix());
            else
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }
        
        
        private void BeginGUIBufferDraw()
        {
            if(UseScaling)
                _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, samplerState: SamplerState.PointClamp, transformMatrix: GameWrapper.Main.ScreenScaleManager.GetScaleMatrix());
            else
                _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque,samplerState: SamplerState.PointClamp);
        }
        protected void EndDraw()
        {
            _spriteBatch.End();
        }

        public override void Destroy()
        {
            base.Destroy();
            // throw new System.NotImplementedException();
        }
    }
}