using System;
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
        private int _fontSize = 20;
        public Color TextColour = Color.Black;
        private SpriteBatch _spriteBatch;
        private Rectangle? _textBounds = null;

        SpriteFontBase _font;

        public TextVerticalAlignment VerticalAlignment = TextVerticalAlignment.Center;

        public TextHorizontalAlignment HorizontalAlignment = TextHorizontalAlignment.Center;

        public Text()
        {
            _font = FontManager.MainFontSystem.GetFont(_fontSize);
        }

        public Text(string Text, int fontSize = 20)
        {
            SetFontSize(fontSize);
            TextValue = Text;
        }


        public Text(string Text, Rectangle spriteRect, int fontSize = 20){
            SetFontSize(fontSize);
            TextValue = Text;
            _textBounds = spriteRect;
        }

        public Text(string Text, Vector2 position, Vector2 bounds, int fontSize = 20)
        {
            SetFontSize(fontSize);
            TextValue = "";
            _textBounds = new Rectangle((int)position.X, (int)position.Y, (int)bounds.X, (int)bounds.Y);
        }
        

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, TextValue, GetPosition(), TextColour);
            _spriteBatch.End();
        }

        public override void Initialize()
        {
            // Font = GameWrapper.Main.Content.Load<SpriteFont>("Fonts/Consolas");
            _font = FontManager.MainFontSystem.GetFont(_fontSize);
            
            _spriteBatch = GameWrapper.Main.MainSpriteBatch;

            if(_textBounds == null && Entity != null && Entity.HasComponent<Sprite2D>()){

                Sprite2D sprite = Entity.GetComponent<Sprite2D>();

                if(sprite.UseRectangle){
                    _textBounds = sprite.ImageRectangle;
                }
            }
        }


        public Text SetFontSize(int size)
        {
            _fontSize = size;
            _font = FontManager.MainFontSystem.GetFont(_fontSize);
            return this;
        }

        public Text SetColour(Color color)
        {
            TextColour = color;
            return this;
        }

        public override void Update(GameTime gameTime)
        {
        }


        public Vector2 GetPosition()
        {
            if(_textBounds == null)
                return Entity.transform.Position;

            Vector2 textSize = _font.MeasureString(TextValue);    
            float xPos = _textBounds.Value.Width;
            float yPos = _textBounds.Value.Height;

            Console.WriteLine(_textBounds.Value);
            
            switch(HorizontalAlignment){
                case TextHorizontalAlignment.Left:
                    xPos = (Entity.transform.Position.X + _textBounds.Value.X) - textSize.X / 2;
                    break;
                case TextHorizontalAlignment.Center:
                    xPos = (Entity.transform.Position.X + _textBounds.Value.Center.X) - textSize.X / 2;
                    break;
                case TextHorizontalAlignment.Right:
                    xPos = (Entity.transform.Position.X + _textBounds.Value.Right) - textSize.X / 2;
                    break;
            }

            switch(VerticalAlignment){
                case TextVerticalAlignment.Left:
                    yPos = (Entity.transform.Position.Y + _textBounds.Value.Y) - textSize.Y / 2;
                    break;
                case TextVerticalAlignment.Center:
                    yPos = (Entity.transform.Position.Y +  _textBounds.Value.Center.Y) - textSize.Y / 2;
                    break;
                case TextVerticalAlignment.Right:
                    yPos = (Entity.transform.Position.Y + _textBounds.Value.Bottom) - textSize.Y / 2;
                    break;
            }

            return new Vector2(xPos, yPos);
        }
    }

    public enum TextVerticalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum TextHorizontalAlignment
    {
        Left,
        Center,
        Right
    }
}