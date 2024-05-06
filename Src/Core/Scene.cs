
using System;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core.Scenes
{

    public abstract class Scene
    {
        private Guid _id;
        public string ID {get { return _id.ToString(); } }

        public Scene(){
            _id = Guid.NewGuid();
        }


        public abstract void OnSceneLoaded();
        public abstract void OnSceneUnloaded();


        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);


    }
}