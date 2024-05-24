

using System;
using CosmicCrowGames.Core;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledCardGame.Scenes
{

    public class GameScene : Scene
    {

        public GameScene(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            
        }

        public override void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnSceneLoaded()
        {
            // throw new System.NotImplementedException();
             var gm1 = Instantiate(new GameObject(Vector2.Zero))
            .AddComponent(new Renderer2D(SpriteBatch))
            .AddComponent(new Sprite2D(GameWrapper.main.Content.Load<Texture2D>("Images/book bg")))
            .AddProp(EntityProperty.Background);
            
            
            gm1.SetScale(new Vector2(2.8f,2));  
            gm1.SetPosition(new Vector2(0, GameWrapper.main.Window.ClientBounds.Height - gm1.GetComponent<Sprite2D>().Texture.Height * gm1.transform.Scale.Y)); 
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
            GraphicsDevice.Clear(Color.Beige);

        }
    }
}