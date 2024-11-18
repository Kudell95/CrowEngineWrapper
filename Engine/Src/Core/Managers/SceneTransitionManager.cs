using System;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Scenes;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CosmicCrowGames.Enums;

namespace CosmicCrowGames.Core{

    public class SceneTransitionManager : Manager
    {
        public static SceneTransitionManager Main;
        public EntityManager EntityManager;

        SpriteBatch _sptiteBatch;

        Entity ScreenBackground;

        public float SceneTransitionTweenTime = 0.3f;

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

        public virtual void LoadScene(Scene scene, Color transitionColor, float Delay = 0.1f)
        {
            if(_transitionInProgress)
                return;

            _transitionInProgress = true;
            ScreenBackground.GetComponent<Sprite2D>().TweenColor(transitionColor, SceneTransitionTweenTime, Easing.EaseInSine).OnComplete(()=>{
                DelayedCall.CreateDelayedCall(Delay).OnComplete(()=>{                    
                    GameWrapper.Main.SceneManager.LoadScene(scene);
                    ScreenBackground.GetComponent<Sprite2D>().TweenColor(Color.Transparent, SceneTransitionTweenTime, Easing.EaseOutSine).OnComplete(()=>{
                        _transitionInProgress = false;
                    });
                });
            });

            
        }
        public void LoadScene(Scene scene, float Delay = 0.1f)
        {
            LoadScene(scene,Color.Black,Delay);
        }

        public void Update(GameTime gameTime){
            EntityManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime){
            EntityManager.Draw(gameTime);
        }


    }



}