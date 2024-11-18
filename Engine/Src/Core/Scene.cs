
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

        public Scene(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) : base(){
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
            EntityManager.Dispose();
            EntityManager = null;
            SpriteBatch.Dispose();
        }


        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        public Entity Instantiate(Entity entity)
        {
            EntityManager.AddEntity(entity);
            return entity;
        }


    }
}