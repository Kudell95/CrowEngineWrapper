

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

    }
}