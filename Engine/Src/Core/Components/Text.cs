using System;
using CosmicCrowGames.Core.Fonts;
using FontStashSharp;
using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core.Components.UI
{
    public class Text : Component
    {
        public string TextValue;
        private int _fontSize = 20;
        public Color TextColour = Color.Black;
        private SpriteBatch _spriteBatch;
        private Rectangle? _textBounds = null;
        SpriteFontBase _font;

        private string _fontName;

        public TextVerticalAlignment VerticalAlignment = TextVerticalAlignment.Center;

        public TextHorizontalAlignment HorizontalAlignment = TextHorizontalAlignment.Center;

        private RichTextLayout rtl;

        public Text()
        {
            _font = FontManager.GetFont(_fontName, _fontSize);
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
            rtl.Draw(_spriteBatch, GetPosition(), TextColour);
            _spriteBatch.End();
        }

        public override void Initialize()
        {
            SetFont(_fontName);
            
            _spriteBatch = GameWrapper.Main.MainSpriteBatch;

            if(_textBounds == null && Entity != null && Entity.HasComponent<Sprite2D>()){

                Sprite2D sprite = Entity.GetComponent<Sprite2D>();

                if(sprite.UseRectangle){
                    _textBounds = sprite.ImageRectangle;
                }
            }
            
            rtl = new RichTextLayout{
                Font = _font,
                Text = TextValue
            };
        }

        public Text SetFont(string font = "")
        {
            if(string.IsNullOrEmpty(font))
                font = AvailableFonts.ConsolasBold;

            _fontName = font;

            _font = FontManager.GetFont(font, _fontSize);
            return this;
        }


        public Text SetFontSize(int size)
        {
            _fontSize = size;
            SetFont(_fontName);
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
            
            switch(HorizontalAlignment){
                case TextHorizontalAlignment.Left:
                    xPos = Entity.transform.Position.X + _textBounds.Value.X;
                    break;
                case TextHorizontalAlignment.Center:
                    xPos = (Entity.transform.Position.X + _textBounds.Value.Center.X) - textSize.X / 2;
                    break;
                case TextHorizontalAlignment.Right:
                    xPos = (Entity.transform.Position.X + _textBounds.Value.Right) - textSize.X;
                    break;
            }

            switch(VerticalAlignment){
                case TextVerticalAlignment.Top:
                    yPos = Entity.transform.Position.Y + _textBounds.Value.Y;
                    break;
                case TextVerticalAlignment.Center:
                    yPos = (Entity.transform.Position.Y +  _textBounds.Value.Center.Y) - textSize.Y / 2;
                    break;
                case TextVerticalAlignment.Bottom:
                    yPos = (Entity.transform.Position.Y + _textBounds.Value.Bottom) - textSize.Y;
                    break;
            }

            return new Vector2(xPos, yPos);
        }
    }

    public enum TextVerticalAlignment
    {
        Top,
        Center,
        Bottom
    }

    public enum TextHorizontalAlignment
    {
        Left,
        Center,
        Right
    }
}