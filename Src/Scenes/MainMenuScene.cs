
using System;
using CosmicCrowGames.Core;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Components.UI;
using CosmicCrowGames.Core.Scenes;
using CosmicCrowGames.Core.Tweening;
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UntitledCardGame.Components;


namespace UntitledCardGame.Scenes
{
    public class MainMenuScene : Scene
    {
        GUIEntity BackgroundElement;
        GUIEntity LaunchButton;


        public MainMenuScene(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) : base(graphicsDevice, spriteBatch)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Initialize()
        {

        }

        /// <summary>
        /// Load Content
        /// </summary>
        public override void OnSceneLoaded()
        {
            // Initialize();
            // .AddComponent(new SceneSwitcher());

            // gm1.GetComponent<Sprite2D>().SpriteColor = Color.White;

            //SETUP BACKGROUND
            BackgroundElement = (GUIEntity)Instantiate(new GUIEntity(Vector2.Zero, SpriteBatch));
            BackgroundElement.GetComponent<AnchoredTransform>().Anchor = AnchorPoint.MiddleCenter;
            BackgroundElement.GetComponent<Sprite2D>().layerDepth = 0;
            BackgroundElement.Width = 1920;
            BackgroundElement.Height = 1080;
            BackgroundElement.FillScreen = true;
            BackgroundElement.GetComponent<Sprite2D>().SpriteColor = Color.DarkSlateGray;
            BackgroundElement.Active = true;  

            var gm1 = Instantiate(new GameObject())
            .AddComponent(new Renderer2D(SpriteBatch))
            .AddComponent(new Sprite2D(GameWrapper.Main.Content.Load<Texture2D>("Images/Book bg"),1));

            var t = new AnchoredTransform(GameWrapper.Main.GraphicsDevice);
            t.Anchor = AnchorPoint.TopLeft;

            gm1.AddComponent(t);

            gm1.Active = false;

            LaunchButton = BuildButton(new Vector2(0,-250), new Vector2(300,100), "Start", Color.Gray, Color.Black, AnchorPoint.MiddleCenter, ()=>{
                GameWrapper.Main.SceneTransitionManager.LoadScene(SceneType.GameScene,Color.Black,0.3f);
            });

            var test = BuildButton(new Vector2(0,-50), new Vector2(300,100), "Options", Color.Gray, Color.Black, AnchorPoint.MiddleCenter, ()=>{
                gm1.Active = true;
                gm1.transform.TweenRotation((float)(Math.PI), 1, Easing.EaseInOutElastic, -1, RepeatType.PingPong);

            });

            var test2 = BuildButton(new Vector2(0,150), new Vector2(300,100), "Quit", Color.Gray, Color.Black, AnchorPoint.MiddleCenter, ()=>{
                GameWrapper.Main.QuitGame();
            });
        }

        //TODO: move this function to a UI utility class or something similar
        //or maybe button should be it's own class.
        GUIEntity BuildButton(Vector2 position, Vector2 Bounds, string text, Color color, Color fontColor, AnchorPoint anchorPoint, Action OnClickAction)
        {
            GUIEntity button = (GUIEntity)Instantiate(new GUIEntity(position, SpriteBatch));
            button.GetComponent<AnchoredTransform>().Anchor = anchorPoint;
            button.GetComponent<AnchoredTransform>().LocalPosition = position;
            button.Width = Bounds.X;
            button.Height = Bounds.Y;
            Color ButtonEntryColor = color;
            Color ButtonDefaultColor = Color.White;

            Color ButtonPressedColor = Color.DarkGray;
            Sprite2D sprite = button.GetComponent<Sprite2D>();
            sprite.SpriteColor = Color.White;
            Text textObject = new Text(text,sprite.ImageRectangle, 70).SetColour(fontColor);
            button.AddComponent(textObject);
            button.Active = true;
            button.AddComponent(new Button(OnClickAction));
            button.GetComponent<Button>().OnMouseEnter += ()=>{
                sprite.SpriteColor = ButtonEntryColor;
            };

            button.GetComponent<Button>().OnMouseLeave += ()=>{
                sprite.SpriteColor = ButtonDefaultColor;
            };

            button.GetComponent<Button>().OnMouseButtonDown += ()=>{
                sprite.SpriteColor = ButtonPressedColor;
            };

            button.GetComponent<Button>().OnMouseButtonUp += ()=>{
                sprite.SpriteColor = ButtonEntryColor;
            };

            return button;
        }

        public override void OnSceneUnloaded()
        {            
            base.OnSceneUnloaded();
            //unload content
        }

        public override void Update(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.SkyBlue);
        }
        

    }
}