using System;
using System.ComponentModel.DataAnnotations;
using CrowEngine.Core.Components;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core.Components
{

    
    public class Physics2D : Component
    {

        public override void Draw(GameTime gameTime)
        {

        }

        public override void LateUpdate(GameTime gameTime)
        {
            
        }

        public override void Initialize()
        {
            //Require a collider 
            if (!Entity.HasComponent<Collider2d>())
                throw new Exception("Physics2d Requires a collider");

            var collider = Entity.GetComponent<Collider2d>();
            collider.OnCollisionEnter += OnCollisionEnter;
            collider.OnCollision += OnCollision;
            collider.OnCollisionExit += OnCollisionExit;
            
        }

        private void OnCollisionExit(Collider2d obj)
        {
            
        }

        private void OnCollision(Collider2d obj)
        {
            
        }

        private void OnCollisionEnter(Collider2d obj)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}