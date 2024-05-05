using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Components;
using CosmicCrowGames.Core;
using System;

namespace UntitledCardGame;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;
    private FrameCounter _frameCounter = new FrameCounter();

    private EntityManager _entityManager;
    private SpriteFont _spriteFont;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this) {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1080,
            IsFullScreen = false,    
            
        };

        _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        this.Window.IsBorderless = false;
        Window.AllowUserResizing = true;
        // this.IsFixedTimeStep = false; //-- Unlimited (needs testing for tearing.)

        // this.Window.AllowUserResizing = true;
        // this.Window.ClientSizeChanged += Window_ClientSizeChanged;
        

        Content.RootDirectory = "Content";
        IsMouseVisible = true;        
        
        Console.WriteLine("Creating Entity Manager");
        _entityManager = new EntityManager(_spriteBatch);

    }

    protected override void Initialize()
    {
        base.Initialize();
        _gameManager = new GameManager();
        Console.WriteLine("Initialized game manager");
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Console.WriteLine("Loading content");
        //This has to be created first.


        //Would be nice if the GameObjects could try and register themselves, to make it easier. Maybe could just use an entity factory. or just have a singleton for the entity manager and set it up in the constructor. Or a static event???
        Entity gm1 = new GameObject(Vector2.Zero)
            .AddComponent(new Renderer2D(_spriteBatch))
            .AddComponent(new Sprite2D(Content.Load<Texture2D>("Images/book bg")));

        gm1.SetScale(new Vector2(2.8f,2));  

        gm1.SetPosition(new Vector2(0, Window.ClientBounds.Height - gm1.GetComponent<Sprite2D>().Texture.Height * gm1.transform.Scale.Y)); 


        _spriteFont = Content.Load<SpriteFont>("Fonts/Consolas");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _entityManager.Update(gameTime);
        
        base.Update(gameTime);
    }

    

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        _entityManager.Draw(gameTime);

    ///DEBUG-------------------------------------------
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

         _frameCounter.Update(deltaTime);

         var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);


        _spriteBatch.Begin();
        _spriteBatch.DrawString(_spriteFont, fps, new Vector2(1, 1), Color.Black);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
