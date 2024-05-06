
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core.Scenes
{

    public abstract class Scene
    {
        public SpriteBatch SpriteBatch;
        public EntityManager EntityManager;
        public GraphicsDevice GraphicsDevice;

        private Guid _id;
        public string ID {get { return _id.ToString(); } }

        private Scene(){
            _id = Guid.NewGuid();
        }

        public Scene(GraphicsDevice graphicsDevice) : base(){
            GraphicsDevice = graphicsDevice;
            SpriteBatch = new SpriteBatch(graphicsDevice);
            EntityManager = new EntityManager(SpriteBatch);
        }


        public abstract void OnSceneLoaded();
        public virtual void OnSceneUnloaded()
        {
            EntityManager.OnDestroy();
            EntityManager.Dispose();
            EntityManager = null;
        }


        public abstract void Initialize();
        public virtual void Update(GameTime gameTime){
            EntityManager.Update(gameTime);
        }
        public virtual void Draw(GameTime gameTime){
            EntityManager.Draw(gameTime);
        }


    }
}