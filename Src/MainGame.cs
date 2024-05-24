using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core;
using System;
using FmodForFoxes;
using UntitledCardGame.Components;
using CosmicCrowGames.Core.Scenes;
using UntitledCardGame.Scenes;
using CosmicCrowGames.Core.Tweening;

namespace UntitledCardGame;

public class GameWrapper : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private FrameCounter _frameCounter = new FrameCounter();

    private SpriteFont _spriteFont;
    
    public static GameWrapper main;

    public Tweener Tweener;

    public SceneManager SceneManager { get; private set; }

    public GameWrapper()
    {
        //we are gonna make a singleton for the game wrapper, as this will contain references to things like the service locator etc...
        if(main == null)
            main = this;

        if(Tweener.Instance == null)
        {
            Tweener.Instance = new Tweener();
        }

        Tweener = Tweener.Instance;
        
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1080,
            IsFullScreen = false,
            GraphicsProfile = GraphicsProfile.HiDef
        };
        this.Window.IsBorderless = false;
        Window.AllowUserResizing = true;
        // this.IsFixedTimeStep = false; //-- Unlimited (needs testing for tearing.)

        // this.Window.AllowUserResizing = true;
        // this.Window.ClientSizeChanged += Window_ClientSizeChanged;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;        
        


    }

    protected override void Initialize()
    {
        base.Initialize();
        SceneManager.LoadScene(SceneType.MainMenu);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteFont = Content.Load<SpriteFont>("Fonts/Consolas");

        SceneManager = new SceneManager();
        SceneManager.AddScenes(SceneFactory.CreateScenes(GraphicsDevice));     
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        // _entityManager.Update(gameTime);

        SceneManager?.Update(gameTime);
        Tweener.Instance?.Update(gameTime);
        
        base.Update(gameTime);
    }

    

    protected override void Draw(GameTime gameTime)
    {
        // GraphicsDevice.Clear(Color.CornflowerBlue);
        // _entityManager.Draw(gameTime);

        SceneManager?.Draw(gameTime);

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
