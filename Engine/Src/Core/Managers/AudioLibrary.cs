using System.Collections.Generic;
using CosmicCrowGames.Core;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace CrowEngine.Core.Managers;

public class AudioLibrary : Manager
{
    public Dictionary<string,SoundEffect> SoundEffects { get; private set; }
    
    public Dictionary<string, Song> Music { get;private set; }
    
    


    public override void Initialize()
    {
        SoundEffects = new Dictionary<string, SoundEffect>();
        Music = new Dictionary<string, Song>();
    }


    public virtual void InitSounds(Dictionary<string, SoundEffect> sounds)
    {
        SoundEffects = sounds;
    }

    public virtual void InitMusic(Dictionary<string, Song> music)
    {
        Music = music;
    }
}