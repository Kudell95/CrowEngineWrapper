

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Core;
using CosmicCrowGames.Components;
using System.Collections.ObjectModel;

namespace UntitledCardGame.Components{

    public class SimpleMovementController : Component
    {
        bool keyPressed = false;

        public float cooldown = 0.1f;

        private float timer = 0f;

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

           if(Entity.HasComponent<Sprite2D>() && Keyboard.GetState().IsKeyDown(Keys.L) && !keyPressed)
           {
               Entity.GetComponent<Sprite2D>().Enabled = !Entity.GetComponent<Sprite2D>().Enabled;
               timer = 0;
               keyPressed = true;
           }

            if(keyPressed)
                timer += deltaTime;

            if(keyPressed && !Keyboard.GetState().IsKeyUp(Keys.L) && timer > cooldown)
                keyPressed = false;
        }
    }
}