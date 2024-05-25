
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Core;
using System;

namespace UntitledCardGame.Components{

    public class SceneSwitcher : Component
    {

        public override void Draw(GameTime gameTime)
        {
            // throw new System.NotImplementedException();
        }

        public override void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            // throw new System.NotImplementedException();
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(InputManager.GetKeyDown(Keys.O))
            {
                Console.WriteLine("Pressed L");
               GameWrapper.Main.SceneTransitionManager.LoadScene(SceneType.MainMenu,Color.Red,0.5f);
            }

            if(InputManager.GetKeyDown(Keys.I))
            {
               GameWrapper.Main.SceneTransitionManager.LoadScene(SceneType.GameScene,Color.Red,0.5f);
            }

        }
        public override void Destroy()
        {
            base.Destroy();
            // throw new System.NotImplementedException();
        }
    }
}