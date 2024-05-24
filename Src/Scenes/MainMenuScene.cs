
using System;
using CosmicCrowGames.Core;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Scenes;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UntitledCardGame.Components;


namespace UntitledCardGame.Scenes
{
    public class MainMenuScene : Scene
    {
        public MainMenuScene(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Initialize()
        {
            
        }

        public override void OnSceneLoaded()
        {
            //load content

            var gm1 = new GameObject()
            .AddComponent(new Renderer2D(SpriteBatch))
            .AddComponent(new Sprite2D(GameWrapper.main.Content.Load<Texture2D>("Images/Book bg")))
            .AddComponent(new SceneSwitcher());
            DelayedCall.CreateDelayedCall(1f).OnComplete(()=>{
                    gm1.transform.TweenPosition(new Vector2(800,500), 1f, Easing.EaseInOutCubic,-1,RepeatType.PingPong);  
                    gm1.transform.TweenScale(new Vector2(2,2),1f,Easing.EaseInOutQuad,-1,RepeatType.PingPong);
                    gm1.transform.TweenRotation((float)(Math.PI / 2) ,1f,Easing.EaseInOutQuad,-1,RepeatType.PingPong);
            });

        }

        public override void OnSceneUnloaded()
        {            
            base.OnSceneUnloaded();
            //unload content
        }

        public override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
        }

    }
}