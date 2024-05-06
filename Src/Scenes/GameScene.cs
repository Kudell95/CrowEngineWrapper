

using CosmicCrowGames.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledCardGame.Scenes
{

    public class GameScene : Scene
    {
        public GameScene(GraphicsDevice graphicsDevice) : base(graphicsDevice){

        }

        public override void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnSceneLoaded()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnSceneUnloaded()
        {
            // throw new System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.Beige);
        }
    }
}