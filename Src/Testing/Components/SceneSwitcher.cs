
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Core;
using CosmicCrowGames.Core.Components;

namespace UntitledCardGame.Components{

    public class SceneSwitcher : Component
    {
        bool keyPressed = false;
        public float cooldown = 0.2f;

        private float timer = 0f;

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

            if( Keyboard.GetState().IsKeyDown(Keys.I) && !keyPressed)
           {
               GameWrapper.main.SceneManager.LoadScene(SceneType.GameScene);
               timer = 0;
               keyPressed = true;
           }

            if(keyPressed)
                timer += deltaTime;

            if(keyPressed && !Keyboard.GetState().IsKeyUp(Keys.L) && timer > cooldown)
                keyPressed = false;
        }
        public override void Destroy()
        {
            base.Destroy();
            // throw new System.NotImplementedException();
        }
    }
}