using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using CosmicCrowGames.Core.Scenes;
using CosmicCrowGames.Core.Tweening;
using CosmicCrowGames.Core.Fonts;
using CrowEngine.Core.Managers;
using FontStashSharp;
using FontStashSharp.RichText;

namespace CosmicCrowGames.Core;

public class GameWrapper : Game
{
    private GraphicsDeviceManager _graphics;
    public SpriteBatch MainSpriteBatch {get; private set;}
    private FrameCounter _frameCounter = new FrameCounter();
    public RenderTarget2D IDBuffer;

    private SpriteFontBase _spriteFont;
    
    public static GameWrapper Main;

    public EntityManager GlobalEntityManager;

    public Tweener Tweener;

    public SceneManager SceneManager { get; private set; }
    public SceneTransitionManager SceneTransitionManager { get; private set; }

    public ResolutionScaleManager ScreenScaleManager { get; private set; }
    public MouseGuiInteractionManager MGUIInteractionMGR { get; private set; }

    public Action OnDraw;
    public Action OnSceneChanged;

    RichTextLayout rtl;


    private bool m_Resizing;

    public int ScreenWidth = 1920;
    public int ScreenHeight = 1080;

    public GameWrapper(int width, int height) 
    {
        ScreenWidth = width;
        ScreenHeight = height;
        
        SetupGame();
    }


    public GameWrapper()
    {
        SetupGame();

    }

    private void SetupGame()
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
        _spriteFont = FontManager.GetFont(AvailableFonts.ConsolasItalics,26);

        rtl = new RichTextLayout{
            Font = _spriteFont,
            Text = "Test",
        };
        
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = ScreenWidth,
            PreferredBackBufferHeight = ScreenHeight,
            IsFullScreen = false,
            GraphicsProfile = GraphicsProfile.HiDef
        };
        _graphics.ApplyChanges();
        Window.AllowUserResizing = true;
        // this.IsFixedTimeStep = false; //-- Unlimited (needs testing for tearing.)

        // this.Window.AllowUserResizing = true;
        // this.Window.ClientSizeChanged += Window_ClientSizeChanged;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        this.Window.ClientSizeChanged += OnClientSizeChanged;

        IDBuffer = new RenderTarget2D(_graphics.GraphicsDevice, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
    }
    

    protected override void Initialize()
    {
        base.Initialize();
        SceneManager = new SceneManager(GraphicsDevice,MainSpriteBatch);
        
        // SceneManager.AddScenes(SceneFactory.CreateScenes(GraphicsDevice));    

        SceneTransitionManager = new SceneTransitionManager(GraphicsDevice, MainSpriteBatch);
        SceneTransitionManager.Initialize();
        
        GlobalEntityManager = new EntityManager(MainSpriteBatch);
        GlobalEntityManager.Initialize();

        ScreenScaleManager = new ResolutionScaleManager(_graphics,ScreenWidth,ScreenHeight); 
        ScreenScaleManager.ApplyResolutionSettings();

        MGUIInteractionMGR = new MouseGuiInteractionManager();
        MGUIInteractionMGR.Initialize();

        //Cleanup Events
        OnSceneChanged += GUIEntity.OnSceneChanged;
    }

    protected override void LoadContent()
    {
        MainSpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {     
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
        GraphicsDevice.SetRenderTarget(IDBuffer);
        GraphicsDevice.Clear(Color.Transparent);
        OnDraw?.Invoke();
        MGUIInteractionMGR?.Draw(gameTime);
        GraphicsDevice.SetRenderTarget(null);
        
        
        GraphicsDevice.Clear(Color.Black);
        // _entityManager.Draw(gameTime);
        GraphicsDevice.Viewport = ScreenScaleManager.ScreenViewport;
        
        SceneManager?.Draw(gameTime);
        SceneTransitionManager?.Draw(gameTime);
        GlobalEntityManager?.Draw(gameTime);

    ///DEBUG-------------------------------------------
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

         _frameCounter.Update(deltaTime);

        var fps = string.Format("FPS: /c[red]{0}", _frameCounter.AverageFramesPerSecond);
        rtl.Text = fps;

        MainSpriteBatch.Begin();
        // // MainSpriteBatch.DrawString(_spriteFont, fps, new Vector2(1, 1), Color.Black);
        rtl.Draw(MainSpriteBatch, new Vector2(1,1), Color.Gray);
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

        if (!m_Resizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
        {
            m_Resizing = true;
            ScreenScaleManager.ApplyResolutionSettings();
            m_Resizing = false;
        }
    }


    public void QuitGame()
    {
        //Do any cleanup if required

        Exit();
    }

}
