

using System;
using CosmicCrowGames.Core;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Scenes;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UntitledCardGame.Components;

namespace UntitledCardGame.Scenes
{

    public class GameScene : Scene
    {
        GUIEntity PauseMenu;

        public GameScene(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) : base(graphicsDevice, spriteBatch)
        {
            
        }

        public override void Initialize()
        {
            // throw new System.NotImplementedException();

              var gm1 = Instantiate(new GameObject(Vector2.Zero))
            .AddComponent(new Renderer2D(SpriteBatch))
            .AddComponent(new Sprite2D(GameWrapper.Main.Content.Load<Texture2D>("Images/book bg")))
            .AddComponent(new SceneSwitcher())
            .AddComponent(new AnchoredTransform(GameWrapper.Main.GraphicsDevice) {
                Anchor = AnchorPoint.BottomCenter
                })
            .AddProp(EntityProperty.Background);
            
            gm1.GetComponent<Sprite2D>().SetPivot(AnchorPoint.BottomCenter);
            gm1.SetScale(new Vector2(2.8f,2));  



            PauseMenu = (GUIEntity)Instantiate(new GUIEntity(Vector2.Zero, SpriteBatch));

            PauseMenu.GetComponent<AnchoredTransform>().Anchor = AnchorPoint.MiddleCenter;
            PauseMenu.Width = 600;
            PauseMenu.Height = 800;
            PauseMenu.GetComponent<Sprite2D>().SpriteColor = Color.DarkSlateGray;
            PauseMenu.GetComponent<Sprite2D>().CurrentColour = Color.Transparent;
            PauseMenu.Active = false;

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

            if(InputManager.GetKeyDown(Keys.T))
                PauseMenu.ToggleGUI();

            if(InputManager.GetKeyDown(Keys.A))
                Console.WriteLine("Test");

        }

        public override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.Beige);
        }


       

    }
}