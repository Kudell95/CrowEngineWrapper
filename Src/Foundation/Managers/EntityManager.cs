

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Foundation
{


    public class EntityManager : Manager
    {
        private SpriteBatch _spriteBatch;
        private Action<GameTime> onUpdate;
        private Action onInitialise;
        private Action<GameTime> onDraw;

        private List<Entity> _entities = new List<Entity>();


        public EntityManager(){

        }


        public EntityManager (SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }
        public EntityManager (SpriteBatch spriteBatch, List<Entity> entities)
        {
            _spriteBatch = spriteBatch;
            entities = _entities;
            foreach(var entity in entities)
            {
                onInitialise += entity.Initialize;
                onUpdate += entity.Update;
                onDraw += entity.Draw;
            }
        }

        public EntityManager AddEntity(Entity entity){
            _entities.Add(entity);
            onInitialise += entity.Initialize;
            onUpdate += entity.Update;
            onDraw += entity.Draw;
            return this;
        }


        public EntityManager DestroyEntity(Entity entity){
            RemoveEntity(entity);
            entity.Destroy();

            return this;
        }

        public EntityManager RemoveEntity(Entity entity)
        {
            onInitialise -= entity.Initialize;
            onUpdate -= entity.Update;
            onDraw -= entity.Draw;
            _entities.Remove(entity);
            return this;
        }

        public override void Initialize()
        {
           onInitialise?.Invoke();
        }


        public void Update(GameTime gameTime)
        {
            onUpdate?.Invoke(gameTime);
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
                entity.Destroy();
            }
        }
    }

}
