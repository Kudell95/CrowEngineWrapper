using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using CosmicCrowGames.Core.Scenes;
using CosmicCrowGames.Core.Tweening;
using CosmicCrowGames.Core.Fonts;

namespace CosmicCrowGames.Core;

public class GameWrapper : Game
{
    private GraphicsDeviceManager _graphics;
    public SpriteBatch MainSpriteBatch {get; private set;}
    private FrameCounter _frameCounter = new FrameCounter();

    private SpriteFont _spriteFont;
    
    public static GameWrapper Main;

    public EntityManager GlobalEntityManager;

    public Tweener Tweener;

    public SceneManager SceneManager { get; private set; }
    public SceneTransitionManager SceneTransitionManager { get; private set; }

    public ResolutionScaleManager ScreenScaleManager { get; private set; }

    public GameWrapper()
    {
        //we are gonna make a singleton for the game wrapper, as this will contain references to things like the service locator etc...
        if(Main == null)
            Main = this;

        if(Tweener.Instance == null)
        {
            Tweener.Instance = new Tweener();
        }

        Tweener = Tweener.Instance;

        FontManager.Initialize();
        
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
        
        Window.ClientSizeChanged += OnClientSizeChanged;

    }

    protected override void Initialize()
    {
        base.Initialize();
        SceneManager.LoadScene(SceneType.MainMenu);
    }

    protected override void LoadContent()
    {
        MainSpriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteFont = Content.Load<SpriteFont>("Fonts/Consolas");

        SceneManager = new SceneManager(GraphicsDevice,MainSpriteBatch);
        // SceneManager.AddScenes(SceneFactory.CreateScenes(GraphicsDevice));    

        SceneTransitionManager = new SceneTransitionManager(GraphicsDevice, MainSpriteBatch);
        SceneTransitionManager.Initialize(); 

        GlobalEntityManager = new EntityManager(MainSpriteBatch);
        GlobalEntityManager.Initialize();

        ScreenScaleManager = new ResolutionScaleManager(_graphics,1920,1080); 
        ScreenScaleManager.UpdateScaleMatrix();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        // _entityManager.Update(gameTime);
        SceneManager?.Update(gameTime);
        SceneTransitionManager?.Update(gameTime);
        Tweener?.Update(gameTime);
        InputManager.Update(gameTime);
        GlobalEntityManager?.Update(gameTime);
        MouseUserInput.Update();
        base.Update(gameTime);
    }

    

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // _entityManager.Draw(gameTime);

        SceneManager?.Draw(gameTime);
        SceneTransitionManager?.Draw(gameTime);
        GlobalEntityManager?.Draw(gameTime);

    ///DEBUG-------------------------------------------
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

         _frameCounter.Update(deltaTime);

         var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);


        MainSpriteBatch.Begin();
        MainSpriteBatch.DrawString(_spriteFont, fps, new Vector2(1, 1), Color.Black);
        MainSpriteBatch.End();
        base.Draw(gameTime);
    }

    /// <summary>
    /// Handles the client window size change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OnClientSizeChanged(object sender, EventArgs e)
    {
        // base.OnClientSizeChanged(sender, e);
        ScreenScaleManager.UpdateScaleMatrix();
    }


    public void QuitGame()
    {
        //Do any cleanup if required

        Exit();
    }

}
