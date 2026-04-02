using CosmicCrowGames.Core;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace CrowEngine.Core.Managers;

public class AudioManager : Manager
{
    
    public override void Initialize()
    {
        
    }
    
    public  void PlayOneShot(string name)
    {
        PlayOneShot(GetSoundEffect(name));
    }
    
    public  void PlayOneShot(SoundEffect sfx)
    {
        if (sfx == null) return;
        
        sfx.Play();
    }
    
    public virtual void PlayMusic(Song song, bool loop = false)
    {
        if (song == null) return;
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = loop;
    }
    
    public  SoundEffect GetSoundEffect(string name)
    {
        if (!GameWrapper.Main.AudioLibrary.SoundEffects.ContainsKey(name)) return null;
        
        return GameWrapper.Main.AudioLibrary.SoundEffects[name];
    }
}