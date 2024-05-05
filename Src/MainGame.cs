using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Components;
using CosmicCrowGames.Foundation;

namespace UntitledCardGame;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;

    private EntityManager _entityManager;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this) {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1080,
            IsFullScreen = false
        
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;        
    }

    protected override void Initialize()
    {
        base.Initialize();
        _gameManager = new GameManager();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _entityManager = new EntityManager(_spriteBatch);
        _entityManager.AddEntity(
            new GameObject()
            .AddComponent(new Sprite2D(Content.Load<Texture2D>("Images/book bg"), _spriteBatch))
            .AddComponent(new MoveRight())
            .AddProp(EntityProperty.Moveable));
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
        _spriteBatch.Begin();
        _entityManager.Draw(gameTime);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
