

using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Components
{

    public class MoveRight : Component
    {
        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if(Entity.HasProp(EntityProperty.Moveable))
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Console.WriteLine("Running update for Sprite2D + {0}", gameTime.TotalGameTime.Seconds);
                Entity.transform.Position = new Vector2(Entity.transform.Position.X + (5f * deltaTime), Entity.transform.Position.Y);
            }
        }
    }
}