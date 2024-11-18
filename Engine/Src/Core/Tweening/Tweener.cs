using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CosmicCrowGames.Core.Tweening
{
    /// <summary>
    /// This needs to be initialised in the main game class, and update called for tweens to work.
    /// </summary>
    public class Tweener
    {
        //TODO: should move this to the service locator. but here for testing.
        public static Tweener Instance = new Tweener();
        private List<Tween> tweens = new List<Tween>();
        public bool Pause;
        

        public void AddTween(Tween tween)
        {
            tweens.Add(tween);
        }

        public void Update(GameTime gameTime){

            if(Pause)
                return;


            for(int i = tweens.Count-1; i >= 0; i--)
            {
                if(!tweens[i].Update(gameTime))
                {
                    tweens.RemoveAt(i);
                }
            }
        }

        public void Clear()
        {
            tweens.Clear();
        }



    }   
}