using System;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UntitledCardGame;

namespace CosmicCrowGames.Core{

    public class SceneTransitionManager : Manager
    {
        public static SceneTransitionManager Main;
        public EntityManager EntityManager;

        SpriteBatch _sptiteBatch;

        Entity ScreenBackground;

        private bool _transitionInProgress;

        public SceneTransitionManager(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            _sptiteBatch = spriteBatch;
            EntityManager = new EntityManager(_sptiteBatch);
        }

        public override void Initialize()
        {
            if(Main != null)
                Console.WriteLine("WARNING! more than one SceneTransitionManager exists!");

            Main = this;
            
            ScreenBackground = new GameObject(Vector2.Zero);
            ScreenBackground.AddComponent(new Renderer2D(_sptiteBatch));
            ScreenBackground.AddComponent(new Sprite2D(GameWrapper.Main.Content.Load<Texture2D>("Images/Square"),0));
            ScreenBackground.AddProp(EntityProperty.Background);
            Sprite2D sprite = ScreenBackground.GetComponent<Sprite2D>();
            
            sprite.SpriteColor = Color.Transparent;
            sprite.UseRectangle = true;
            sprite.ImageRectangle = new Rectangle(0, 0, 2560, 1440);
            ScreenBackground.transform.Scale = new Vector2(50,50);
            EntityManager.AddEntity(ScreenBackground);
        }

        public void LoadScene(SceneType sceneType, Color transitionColor, float Delay = 0.1f)
        {
            if(_transitionInProgress)
                return;

            _transitionInProgress = true;
            ScreenBackground.GetComponent<Sprite2D>().TweenColor(transitionColor, 1f, Easing.EaseInSine).OnComplete(()=>{
                DelayedCall.CreateDelayedCall(Delay).OnComplete(()=>{                    
                    GameWrapper.Main.SceneManager.LoadScene(sceneType);
                    ScreenBackground.GetComponent<Sprite2D>().TweenColor(Color.Transparent, 1f, Easing.EaseOutSine).OnComplete(()=>{
                        _transitionInProgress = false;
                    });
                });
            });

            
        }
        public void LoadScene(SceneType sceneType, float Delay = 0.1f)
        {
            LoadScene(sceneType,Color.Black,Delay);
        }



        public void Update(GameTime gameTime){
            EntityManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime){
            EntityManager.Draw(gameTime);
        }


    }



}