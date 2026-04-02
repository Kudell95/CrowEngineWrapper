

using System;
using System.Collections.Generic;
using System.Linq;
using CrowEngine.Core.Components;
using CrowEngine.Core.Data;
using CrowEngine.Core.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core
{
    //TODO: add support for global entities, that exist outside of the current scene.
    //maybe just pass a global flag in with the event when we create a new entitiy. and if the entity manager is flagged as global it will pick it up. Will multiple entity managers cause problems though??
    //Other option is to just add a way to manually add an entity to the entity manager.

    //Could just have GUIDS for the entity MGR - if we want additively loaded guids

    public class EntityManager : Manager, IDisposable
    {
        private SpriteBatch _spriteBatch;
        private Action<GameTime> onUpdate;
        private Action<GameTime> onLateUpdate;
        private Action onInitialise;
        private Action<GameTime> onDraw;

        private List<Collider2d> cachedColliders;

        private List<Entity> _entities = new List<Entity>();

        public EntityManager()
        {

        }
        public EntityManager (SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;

            Entity.onEntityCreated += OnEntityCreated;
        }

        private void OnEntityCreated(Entity entity)
        {
            AddEntity(entity);
        }

        public EntityManager (SpriteBatch spriteBatch, List<Entity> entities)
        {
            _spriteBatch = spriteBatch;
            entities = _entities;
            foreach(var entity in entities)
            {
                // onInitialise += entity.Initialize;
                onUpdate += entity.Update;
                onDraw += entity.Draw;
                onLateUpdate += entity.LateUpdate;

                entity.Initialize();
            }
        }

        public EntityManager AddEntity(Entity entity)
        {
            cachedColliders = null;
            _entities.Add(entity);
            entity.Initialize();
            onUpdate += entity.Update;
            onDraw += entity.Draw;
            onLateUpdate += entity.LateUpdate;
            return this;
        }


        public EntityManager DestroyEntity(Entity entity)
        {
            cachedColliders = null;
            RemoveEntity(entity);
            entity.Destroy();

            return this;
        }

        public EntityManager RemoveEntity(Entity entity)
        {
            cachedColliders = null;
            onInitialise -= entity.Initialize;
            onUpdate -= entity.Update;
            onDraw -= entity.Draw;
            onLateUpdate -= entity.LateUpdate;
            _entities.Remove(entity);
            return this;
        }

        public override void Initialize()
        {
        }

        public List<Collider2d> GetCollideableEntities()
        {
            Console.WriteLine($"Entity count = {_entities.Count}");
            if (cachedColliders != null && cachedColliders.Count > 0)
                return cachedColliders;
            
            List<Collider2d> cols = new List<Collider2d>();
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i].HasComponent<Collider2d>())
                {
                    cols.Add(_entities[i].GetComponent<Collider2d>());
                }
            }
            
            Console.WriteLine($"Collider count = {cols.Count}");

            cachedColliders = cols;
            return cols;
        }


        public void Update(GameTime gameTime)
        {
            onUpdate?.Invoke(gameTime);
           
            
            onLateUpdate?.Invoke(gameTime);
        }


        public void Draw(GameTime gameTime)
        {
            onDraw?.Invoke(gameTime);
        } 


        public void OnDestroy() {
            foreach(var entity in _entities){
                onInitialise -= entity.Initialize;
                onUpdate -= entity.Update;
                onDraw -= entity.Draw;
                //TODO: need to look into disposing and ensure we are actually unloading resources here, no point leaving a scene loaded in memory. and if we reload we want that shit fresh...
                entity.SetEnabled(false);
                entity.Destroy();
            }


            Entity.onEntityCreated -= OnEntityCreated;

            Dispose();
        }

        public void Dispose()
        {
            _spriteBatch = null;
            _entities = null;
        }
    }

}
