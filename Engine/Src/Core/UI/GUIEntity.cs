using System;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CosmicCrowGames.Enums;
using CrowEngine.Core;
using CrowEngine.Core.Helpers;

namespace CosmicCrowGames.Core
{
    public class GUIEntity : InteractableEntity
    {
        
        private SpriteBatch _spritebatch;

        public Texture2D Texture;

        
        //Each GUI Entity has:
        // - a new rectangle that spans the bounds of the entity.
        // - a guid.
        // - a corresponding UNIQUE colour
        // 
        // mouse will sample the pixel it's under (maybe only when it moves).
        // interaction manager will store the current selected element.


        

        private float _width;
        private float _height;

        public bool FillScreen;

        bool _TweenStarted = false;


        public float Width {
            get{
                return _width;
            }
            set{
                if(value != _width && HasComponent<Sprite2D>())
                {
                    Sprite2D sprite2D = GetComponent<Sprite2D>();
                    sprite2D.ImageRectangle = new Rectangle(0, 0, (int)value, (int)_height);
                    sprite2D.SetPivot(sprite2D.CurrentPivotAnchor);
                }             
                _width = value;
            }
        }

        public float Height {
            get{
                return _height;
            }
            set{
                if(value != _height && HasComponent<Sprite2D>())
                {
                    Sprite2D sprite2D = GetComponent<Sprite2D>();
                    sprite2D.ImageRectangle = new Rectangle(0, 0, (int)_width, (int)value);
                    sprite2D.SetPivot(sprite2D.CurrentPivotAnchor);
                }
                _height = value;
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
            var sprite2D = new Sprite2D(Texture)
            {
                IsGUI = true
            };
            
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

        protected GUIEntity(SpriteBatch spriteBatch) : base()
        {
            _spritebatch = spriteBatch;
        }

        protected GUIEntity(Vector2 position, SpriteBatch spriteBatch) : base(position)
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

            this.SetEnabled(true);
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
                    this.SetEnabled(false);
                    _TweenStarted = false;
                });
            }
            else
            {
                Sprite2D sprite2D = GetComponent<Sprite2D>();
                sprite2D.CurrentColour = Color.Transparent;
                this.SetEnabled(false);
            }
        }

        public static GUIEntity NewInstance(Vector2 position, SpriteBatch spriteBatch)
        {
            return new GUIEntity(position, spriteBatch);
        }

        public static void OnSceneChanged()
        {
            //Reset the next id.
            m_NextId = 1;
        }
    }
}