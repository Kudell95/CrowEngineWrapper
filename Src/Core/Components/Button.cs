using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core.Components.UI
{
    public class Button : Component
    {

        public MouseInteractionHelper InteractionHelper { get; set; }
        public Vector2 Bounds;

        public Rectangle InteractionRectangle;
        public Action OnMouseButtonUp { get; set; }
        public Action OnMouseButtonDown { get; set; }
        public Action OnMouseEnter {get;set;}
        public Action OnMouseLeave {get;set;}




        public Button(Vector2 bounds) : base()
        {
            Bounds = bounds;
            InteractionRectangle = new Rectangle(0, 0, (int)bounds.X, (int)bounds.Y);
        }


        public Button(Vector2 bounds, Action onClick) : base()
        {
            Bounds = bounds;
            OnMouseButtonUp = onClick;
        }


        // this is the recomended constructor
        public Button(Rectangle boundsRect, Action onClick) : base()
        {
            InteractionRectangle = boundsRect;
            OnMouseButtonUp = onClick;
        }

        public override void Draw(GameTime gameTime)
        {

        }

        public override void Initialize()
        {
            InteractionHelper = new MouseInteractionHelper(InteractionRectangle);
            InteractionHelper.OnMouseUp += OnClickReleased;
            InteractionHelper.OnMouseEnter += OnMouseOver;
            InteractionHelper.OnMouseLeave += OnMouseExit;
            InteractionHelper.OnMouseDown += OnCLickPressed;
        }

        public override void Update(GameTime gameTime)
        {
            InteractionHelper.UpdateRectangle(Entity.transform.Position);
            InteractionHelper?.Update(gameTime);
        }

        
        private void OnMouseOver(){

            OnMouseEnter?.Invoke();
        }

        private void OnMouseExit(){

            OnMouseLeave?.Invoke();
        }


        private void OnCLickPressed()
        {
            OnMouseButtonDown?.Invoke();
        }

        private void OnClickReleased()
        {
            OnMouseButtonUp?.Invoke();
        }
    }
}