using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core.Components.UI
{
    //NOTE: POSSIBLY DEPRECATED.
    public class Button : Component
    {

        public MouseInteractionHelper InteractionHelper { get; set; }
        public Vector2 Bounds;

        public Rectangle? InteractionRectangle = null; 
        public Action OnMouseButtonUp { get; set; }
        public Action OnMouseButtonDown { get; set; }
        public Action OnMouseEnter {get;set;}
        public Action OnMouseLeave {get;set;}

        //TODO:with changes to the GUIEntity, this component may not be needed

        public Button(Action onClick) : base(){
            if(Entity != null && Entity.HasComponent<Sprite2D>())
            {
                Sprite2D sprite = Entity.GetComponent<Sprite2D>();
                if(sprite.UseRectangle)
                {
                    Bounds = sprite.ImageRectangle.Size.ToVector2();
                    InteractionRectangle = sprite.ImageRectangle;
                }else{
                    Bounds = sprite.Texture.Bounds.Size.ToVector2();
                    InteractionRectangle = sprite.Texture.Bounds;
                }
            }     
            OnMouseButtonUp = onClick;          
        }


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
            if(Entity != null && Entity.HasComponent<Sprite2D>() && InteractionRectangle == null)
            {
                Sprite2D sprite = Entity.GetComponent<Sprite2D>();
                if(sprite.UseRectangle)
                {
                    Bounds = sprite.ImageRectangle.Size.ToVector2();
                    InteractionRectangle = sprite.ImageRectangle;
                }else{
                    Bounds = sprite.Texture.Bounds.Size.ToVector2();
                    InteractionRectangle = sprite.Texture.Bounds;
                }
            }     

            if(InteractionRectangle != null){
            InteractionHelper = new MouseInteractionHelper((Rectangle)InteractionRectangle);
            InteractionHelper.OnMouseUp += OnClickReleased;
            InteractionHelper.OnMouseEnter += OnMouseOver;
            InteractionHelper.OnMouseLeave += OnMouseExit;
            InteractionHelper.OnMouseDown += OnCLickPressed;
            }
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