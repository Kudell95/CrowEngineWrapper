using System;
using CosmicCrowGames.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CosmicCrowGames.Core
{

    /// <summary>
    /// Base class for all components
    /// </summary>
    public abstract class Component
    {
        private bool _enabled = true;

        public Action<Component> OnEnabled;

        public Action<Component> OnDisabled;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;

                if (_enabled)
                    OnEnabled?.Invoke(this);
                else
                    OnDisabled?.Invoke(this);
            }
        }

        public Component(){
            
        }
        public Component(Entity entity)
        {
            Entity = entity;
        }
        

        public Entity Entity { get; set; }

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);
        
        public abstract void Draw(GameTime gameTime);
    }

}