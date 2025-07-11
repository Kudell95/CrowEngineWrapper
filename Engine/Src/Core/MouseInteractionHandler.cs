using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CosmicCrowGames.Core
{
    public class MouseInteractionHelper
    {
        private Rectangle InteractionRectangle;
        private bool m_IsMouseOver;
        public Vector2 Bounds;
        public Action OnMouseOver;
        public Action OnMouseEnter;
        public Action OnMouseLeave;
        public Action OnMouseDown;
        public Action OnMouseUp;

        public MouseInteractionHelper(Vector2 position, Vector2 bounds)
        {
            Bounds = bounds;

            InteractionRectangle = new Rectangle((int)position.X, (int)position.Y, (int)bounds.X, (int)bounds.Y);
        }


        public MouseInteractionHelper(Rectangle rectangle)
        {
            InteractionRectangle = rectangle;

            Bounds = new Vector2(InteractionRectangle.Width, InteractionRectangle.Height);
        }


        public void UpdateRectangle(Vector2 position)
        {
            InteractionRectangle = new Rectangle((int)position.X, (int)position.Y, (int)InteractionRectangle.Width, (int)InteractionRectangle.Height);
        }


        public void Update(GameTime gameTime)
        {
            if(InteractionRectangle.Contains(Mouse.GetState().Position))
            {
                //Mouse entering the rectangle for first time.
                if(m_IsMouseOver == false)
                {
                    OnMouseEnter?.Invoke();
                }
                else{
                    OnMouseOver?.Invoke();
                }

                m_IsMouseOver = true;

                if(MouseUserInput.IsLeftClicked){
                    OnMouseDown?.Invoke();
                }else if(MouseUserInput.IsLeftJustReleased){
                    OnMouseUp?.Invoke();
                }
            }else{
                if(m_IsMouseOver)
                    OnMouseLeave?.Invoke();
                    
                m_IsMouseOver = false;

            }
        }

    }
}