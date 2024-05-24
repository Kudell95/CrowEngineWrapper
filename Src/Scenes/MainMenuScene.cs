
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

            gm1.TweenTo(new Vector2(500,0), 1f, Easing.EaseInOutElastic).OnComplete(()=>{
            //  gm1.TweenTo(new Vector2(0,0), 1f, Easing.EaseInOutBack);
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