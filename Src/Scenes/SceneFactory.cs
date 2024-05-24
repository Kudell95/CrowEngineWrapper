
using System.Collections.Generic;
using CosmicCrowGames.Core.Scenes;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledCardGame.Scenes
{
    public class SceneFactory{

        public static Scene CreateScene(SceneType sceneType, GraphicsDevice graphicsDevice){
            switch(sceneType){
                case SceneType.MainMenu:
                    return new MainMenuScene(graphicsDevice);
                case SceneType.GameScene:
                    return new GameScene(graphicsDevice);
                default:
                    return null;
            }
        }


        public static Dictionary<SceneType, Scene> CreateScenes(GraphicsDevice graphicsDevice){
            Dictionary<SceneType,Scene> scenes = new Dictionary<SceneType, Scene>
            {
                { SceneType.MainMenu, new MainMenuScene(graphicsDevice) },
                { SceneType.GameScene, new GameScene(graphicsDevice) }
            };

            return scenes;
        }
    }
}