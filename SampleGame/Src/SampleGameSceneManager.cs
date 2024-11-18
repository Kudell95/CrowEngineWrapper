
using System.Collections.Generic;
using CosmicCrowGames.Core.Scenes;
using Microsoft.Xna.Framework.Graphics;
using CosmicCrowGames.Core.Scenes;
using CosmicCrowGames.Core;
using SampleGame.Scenes;
using CosmicCrowGames.Enums;

namespace SampleGame;

public class SampleGameSceneManager : SceneManager
{


        public void LoadScene(SceneType sceneType)
        {
            if(!SceneFactory.SupportedSceneTypes.Contains(sceneType))
                return;

            if(CurrentScene != null)
            {
                CurrentScene.OnSceneUnloaded(); // may need to await for this... or listen for event.
            }
            
            CurrentScene = SceneFactory.CreateScene(sceneType, graphicsDevice, spriteBatch);

            CurrentScene.OnSceneLoaded();

            // unload current scene.
            //
        }

}