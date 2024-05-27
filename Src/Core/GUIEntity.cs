using System;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core
{
    public class GUIEntity : Entity
    {
        private SpriteBatch _spritebatch;

        public Texture2D Texture;

        private float _width;
        private float _height;

        public bool FillScreen;

        bool _TweenStarted = false;

        public float Width {
            get{
                return _width;
            }
            set{
                _width = value;
                if(HasComponent<Sprite2D>())
                {
                    Sprite2D sprite2D = GetComponent<Sprite2D>();
                    sprite2D.ImageRectangle = new Rectangle(0, 0, (int)_width, (int)_height);
                    sprite2D.SetPivot(sprite2D.CurrentPivotAnchor);
                }             
            }
        }

        public float Height {
            get{
                return _height;
            }
            set{
                _height = value;
                if(HasComponent<Sprite2D>())
                {
                    Sprite2D sprite2D = GetComponent<Sprite2D>();
                    sprite2D.ImageRectangle = new Rectangle(0, 0, (int)_width, (int)_height);
                    sprite2D.SetPivot(sprite2D.CurrentPivotAnchor);
                }             
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            if(_spritebatch == null)
                Console.WriteLine("WARNING! No SpriteBatch set for GUIEntity");
            
            if(Texture == null)
            {
                try{
                    Texture = GameWrapper.Main.Content.Load<Texture2D>("Images/Square");
                }catch{
                    Console.WriteLine("WARNING! No Texture set for GUIEntity");
                }
            }        
        
            

            AddComponent(new Renderer2D(_spritebatch));
            Sprite2D sprite2D = new Sprite2D(Texture);
            AddComponent(sprite2D);
            AddComponent(new AnchoredTransform(GameWrapper.Main.GraphicsDevice));
            sprite2D.SetPivot(AnchorPoint.MiddleCenter);
            sprite2D.ImageRectangle = new Rectangle(0, 0, (int)Width, (int)Height);
            sprite2D.UseRectangle = true;
            AddProp(EntityProperty.GUIElement);
        }

        private GUIEntity() : base()
        {
            
        }

        public GUIEntity(SpriteBatch spriteBatch) : base()
        {
            _spritebatch = spriteBatch;
        }

        public GUIEntity(Vector2 position, SpriteBatch spriteBatch) : base(position)
        {
            _spritebatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(FillScreen)
            {
                Width = GameWrapper.Main.GraphicsDevice.Viewport.Width;
                Height = GameWrapper.Main.GraphicsDevice.Viewport.Height;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override Entity AddComponent(Component component)
        {
            //feel like there must be a better way to handle this, 
            return base.AddComponent(component);
        }

        public void ToggleGUI(bool fade = true, float fadeTime = 0.1f)
        {
            if (this.Active)
            {
                DisableGUI(fade, fadeTime);
            }
            else
            {
                EnableGUI(fade, fadeTime);
            }
        }

        public void EnableGUI(bool fade = true, float fadeTime = 0.1f)
        {
            if(_TweenStarted)
                return;

            this.Active = true;
            if (fade)
            {
                _TweenStarted = true;
                // this.Active = true;
                Sprite2D sprite2D = GetComponent<Sprite2D>();

                sprite2D.TweenColor(sprite2D.SpriteColor,fadeTime,Easing.EaseInOutSine,0,RepeatType.None).OnComplete(()=>{
                    _TweenStarted = false;
                });

            }else{
                
                Sprite2D sprite2D = GetComponent<Sprite2D>();
                sprite2D.CurrentColour = sprite2D.SpriteColor;
            }
        }

        public void DisableGUI(bool fade = true, float fadeTime = 0.2f)
        {
            if(_TweenStarted)
                return;


            if (fade)
            {
                _TweenStarted = true;
                Sprite2D sprite2D = GetComponent<Sprite2D>();
                sprite2D.TweenColor(Color.Transparent, fadeTime, Easing.EaseInOutSine, 0, RepeatType.None).OnComplete(() => {
                    this.Active = false;
                    _TweenStarted = false;
                });
            }
            else
            {
                Sprite2D sprite2D = GetComponent<Sprite2D>();
                sprite2D.CurrentColour = Color.Transparent;
                this.Active = false;
            }
        }
    }
}