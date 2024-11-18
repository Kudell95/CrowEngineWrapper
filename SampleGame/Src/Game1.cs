using CosmicCrowGames.Core;
using SampleGame.Enums;
using SampleGame.Scenes;
namespace SampleGame;
public class Game1 : GameWrapper
{
   protected override void Initialize()
   {
      base.Initialize();
      SceneManager.LoadScene(SceneFactory.CreateScene(SceneType.MainMenu, GraphicsDevice, MainSpriteBatch));
   }
}
