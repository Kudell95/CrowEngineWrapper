
using System;
using CrowEngine.Core.Data;
using CrowEngine.Core.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core.Scenes
{

    public abstract class Scene
    {
        public SpriteBatch SpriteBatch;
        public EntityManager EntityManager;
        public GraphicsDevice GraphicsDevice;
        public CollisionManager CollisionManager;
        

        private Guid _id;
        public string ID {get { return _id.ToString(); } }

        private Scene(){
            _id = Guid.NewGuid();
        }

        protected Scene(GraphicsDevice graphicsDevice) : base()
        {
            GraphicsDevice = graphicsDevice;
            SpriteBatch = new SpriteBatch(graphicsDevice);
            EntityManager = new EntityManager(SpriteBatch);
            CollisionManager = new CollisionManager(graphicsDevice);

        }

        [Obsolete("Sprite batch has the potential to be null, see comment in constructor. Using the constructor that just takes the graphics device is preferred.")]
        protected Scene(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) : base()
        {
            GraphicsDevice = graphicsDevice;
            //FIXME: i have no fucking clue why, but passing the spritebatch through here causes the graphics device to be null.
            // So create the sprite batch fresh when loading scene.
            //REASON WHY IS BECAUSE WE"RE DISPOSING SPRITE BATCH ON UNLOADED!!! Might not be a bug, keeping here for now and monitoring memory.
            SpriteBatch = new SpriteBatch(graphicsDevice);
            EntityManager = new EntityManager(SpriteBatch);
        }


        public abstract void OnSceneLoaded();
        public virtual void OnSceneUnloaded()
        {
            EntityManager.OnDestroy();
            
            EntityManager = null;
            SpriteBatch.Dispose();
            CollisionManager.Dispose();
            CollisionManager = null;
        }


        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
      
        protected T Instantiate<T>(T entity) where T : Entity
        {
            EntityManager.AddEntity(entity);
            return entity;
        }

        
        


    }
}