
using System.Collections.Generic;
using CosmicCrowGames.Core.Scenes;
using Microsoft.Xna.Framework.Graphics;
using SampleGame.Enums;

namespace SampleGame.Scenes
{
    public class SceneFactory
    {
        
        public static HashSet<SceneType> SupportedSceneTypes { get; } = new HashSet<SceneType>() { SceneType.MainMenu};
        

        public static Scene CreateScene(SceneType sceneType, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch){
            switch(sceneType){
                case SceneType.MainMenu:
                    return new MainMenuScene(graphicsDevice,spriteBatch);
                default:
                    return null;
            }
        }

    }
}