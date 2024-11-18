
//See git-amends video https://www.youtube.com/watch?v=D4r5EyYQvwY would be nice to do something similar.
namespace CosmicCrowGames.Core
{

    public static class ServiceLocator
    {

        public static T GetService<T>()
        {
            return default(T);
        }
    }



}