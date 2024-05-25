

using System;
using CosmicCrowGames.Core;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Scenes;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UntitledCardGame.Components;

namespace UntitledCardGame.Scenes
{

    public class GameScene : Scene
    {

        public GameScene(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) : base(graphicsDevice, new SpriteBatch(graphicsDevice))
        {
            
        }

        public override void Initialize()
        {
            // throw new System.NotImplementedException();

              var gm1 = Instantiate(new GameObject(Vector2.Zero))
            .AddComponent(new Renderer2D(SpriteBatch))
            .AddComponent(new Sprite2D(GameWrapper.Main.Content.Load<Texture2D>("Images/book bg")))
            .AddComponent(new SceneSwitcher())
            .AddProp(EntityProperty.Background);
            
            
            gm1.SetScale(new Vector2(2.8f,2));  
            gm1.SetPosition(new Vector2(0, GameWrapper.Main.Window.ClientBounds.Height - gm1.GetComponent<Sprite2D>().Texture.Height * gm1.transform.Scale.Y)); 

            gm1.GetComponent<Sprite2D>().TweenColor(Color.Red, 1f, Easing.EaseInSine, -1, RepeatType.PingPong);
        }

        public override void OnSceneLoaded()
        {
            Console.WriteLine("Loading the Game Scene");
            // throw new System.NotImplementedException();
            Initialize();
        }

        public override void OnSceneUnloaded()
        {
            base.OnSceneUnloaded();
            // throw new System.NotImplementedException();

        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.Beige);
        }
    }
}