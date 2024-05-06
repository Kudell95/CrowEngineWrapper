
using CosmicCrowGames.Core;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Scenes;
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
           base.Draw(gameTime);
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
        }

        public override void OnSceneUnloaded()
        {
            //unload content
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GraphicsDevice.Clear(Color.SkyBlue);
        }

    }
}