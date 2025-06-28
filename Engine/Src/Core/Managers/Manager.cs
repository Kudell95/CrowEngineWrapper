

using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core
{
    public abstract class Manager
    {
        public abstract void Initialize();

        public virtual void Update(GameTime gameTime)
        {
            //do nothing
        }

        public virtual void Draw(GameTime gameTime)
        {
            //do nothing
        }
    }
}