

namespace CosmicCrowGames.Core
{
    public class GameManager : Manager
    {
        public static GameManager Instance { get; private set;}    


        public GameManager() 
        { 
            Initialize();
        }

        public override void Initialize()
        {
             if(Instance == null)
                Instance = this;
        }
        
    }
}