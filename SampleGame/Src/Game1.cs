using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CosmicCrowGames.Core;
using CosmicCrowGames.Enums;
using SampleGame.Scenes;
namespace SampleGame;
public class Game1 : GameWrapper
{
   protected override void Initialize()
   {
        base.Initialize();
      //   SceneManager.AddScenes(SceneFactory.CreateScenes(GraphicsDevice));      
         // SceneManager = new SampleGameSceneManager(GraphicsDevice,MainSpriteBatch);
         SceneManager.LoadScene(SceneFactory.CreateScene(SceneType.MainMenu, GraphicsDevice, MainSpriteBatch));
   }
}
