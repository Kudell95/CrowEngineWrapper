
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CosmicCrowGames.Core
{
    public class GameObject : Entity
    {

        public override void Initialize()
        {
            base.Initialize();

        }

        public GameObject(Vector2 position) : base(position){
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}