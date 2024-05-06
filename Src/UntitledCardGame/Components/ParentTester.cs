

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Core;

namespace UntitledCardGame.Components{

    public class ParentTester : Component
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

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Entity.ClearParent();
            }
        }
    }
}