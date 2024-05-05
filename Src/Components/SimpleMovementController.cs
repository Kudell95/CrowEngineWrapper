

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Core;

namespace CosmicCrowGames.Components{

    public class SimpleMovementController : Component
    {
        public override void Draw(GameTime gameTime)
        {
           
        }

        public override void Initialize()
        {
           
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

           if(Entity.HasProp(EntityProperty.Moveable) && Keyboard.GetState().IsKeyDown(Keys.Right))
           {
               Entity.SetPosition(new Vector2(Entity.transform.Position.X + 400 * deltaTime, Entity.transform.Position.Y));
           }
           else if(Entity.HasProp(EntityProperty.Moveable) && Keyboard.GetState().IsKeyDown(Keys.Left))
           {
               Entity.SetPosition(new Vector2(Entity.transform.Position.X - 400 * deltaTime, Entity.transform.Position.Y));
           }
        }
    }
}