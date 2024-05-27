using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core.Components.UI
{
    public class Text : Component
    {

        //TODO: add justification & alignment - also, probably worth checking out something like this https://github.com/SometimesRain/MonogameDistanceFont 
        //monogame's font implementation just doesn't seem to cut it.
        public string TextValue;
        public SpriteFont Font;
        public int FontSize = 20;
        public Color TextColour = Color.Black;
        private SpriteBatch _spriteBatch;

        public Vector2 Bounds;
        

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(Font, TextValue, Entity.transform.Position, TextColour, 0, Vector2.Zero, FontSize /10, SpriteEffects.None, 0);
            _spriteBatch.End();
        }

        public override void Initialize()
        {
            Font = GameWrapper.Main.Content.Load<SpriteFont>("Fonts/Consolas");
            _spriteBatch = GameWrapper.Main.MainSpriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}