

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core.Scenes
{
    //Scene Manager ~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-
    // Whats it do 🤔
    // stores a dictionary of scenes with a string key?? or enum key?? or id key??
    // manages unloading previous scene, waits until unloading is finished.
    // similtaneously loads up scene transition  (with optional loading screen)
    // loads new scene and closes scene transition.
    // scene transition acts essentially like a scene, but can run concurrently with the rest of the game and listens for different events.
    // initial load up of the game will call a scene factory and pass results to the scene manager.
    // Factory will also set what the first scene will be.
    // scene manager will start the first scene.
    // game will play.


    public class SceneManager
    {
        private Dictionary<SceneType, Scene> _scenes = new Dictionary<SceneType, Scene>();

        public Scene CurrentScene {get; private set;}

        public static Action<GameTime> OnDraw;
        public static Action<GameTime> OnUpdate;


        public void Update(GameTime gameTime){
            CurrentScene?.Update(gameTime);
            CurrentScene?.EntityManager?.Update(gameTime);
        }

        public void Draw(GameTime gameTime){
            CurrentScene?.Draw(gameTime);
            CurrentScene?.EntityManager?.Draw(gameTime);
        }


        public SceneManager AddScene(Scene scene, SceneType sceneType){
            if(_scenes.ContainsKey(sceneType))
                return this;
            
            _scenes.Add(sceneType, scene);
            return this;
        }

        public bool HasScene(SceneType sceneType){
            return _scenes.ContainsKey(sceneType);
        }

        public bool RemoveScene(SceneType sceneType){
            if(!_scenes.ContainsKey(sceneType))
                return false;
            
            _scenes.Remove(sceneType);
            return true;
        }

        public SceneManager AddScenes(Dictionary<SceneType, Scene> scenes)
        {
            _scenes = scenes;

            return this;
        }


        public void LoadScene(SceneType sceneType)
        {
            if(!_scenes.ContainsKey(sceneType))
                return;

            if(CurrentScene != null)
            {
                CurrentScene.OnSceneUnloaded(); // may need to await for this... or listen for event.
            }
            
            CurrentScene = _scenes[sceneType];

            CurrentScene.OnSceneLoaded();

            // unload current scene.
            //
        }
    }
}