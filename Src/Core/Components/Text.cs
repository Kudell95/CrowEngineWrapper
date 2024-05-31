using CosmicCrowGames.Core.Fonts;
using FontStashSharp;
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
        private int _fontSize = 20;
        public Color TextColour = Color.Black;
        private SpriteBatch _spriteBatch;

        public Vector2 Bounds;

        SpriteFontBase _font;
        

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, TextValue, Entity.transform.Position, TextColour);
            _spriteBatch.End();
        }

        public override void Initialize()
        {
            // Font = GameWrapper.Main.Content.Load<SpriteFont>("Fonts/Consolas");
            _font = FontManager.MainFontSystem.GetFont(_fontSize);
            
            _spriteBatch = GameWrapper.Main.MainSpriteBatch;
        }


        public void SetFontSize(int size)
        {
            _fontSize = size;
            _font = FontManager.MainFontSystem.GetFont(_fontSize);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}