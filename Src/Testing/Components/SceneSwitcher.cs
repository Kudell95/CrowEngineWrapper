
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Core;
using System;

namespace UntitledCardGame.Components{

    public class SceneSwitcher : Component
    {
        Color transitionColor = new Color(26,27,38);

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
            if(InputManager.GetKeyDown(Keys.O))
            {
                Console.WriteLine("Pressed L");
               GameWrapper.Main.SceneTransitionManager.LoadScene(SceneType.MainMenu,transitionColor,0.3f);
            }

            if(InputManager.GetKeyDown(Keys.I))
            {
               GameWrapper.Main.SceneTransitionManager.LoadScene(SceneType.GameScene,transitionColor,0.3f);
            }

        }
        public override void Destroy()
        {
            base.Destroy();
        }
    }
}