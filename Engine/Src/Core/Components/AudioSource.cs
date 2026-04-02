using CosmicCrowGames.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace CrowEngine.Core.Components;

public class AudioSource : Component
{
    
    public override void Draw(GameTime gameTime)
    {

    }

    public override void LateUpdate(GameTime gameTime)
    {
        
    }

    public override void Initialize()
    {
        
    }

    public virtual void PlayOneShot(SoundEffect sfx)
    {
        if (sfx == null) return;
        
        sfx.Play();
    }

    public virtual void PlayOneShot(string name)
    {
        PlayOneShot(GetSoundEffect(name));
    }

    public virtual void PlayMusic(Song song)
    {
        if (song == null) return;
        
        //TODO: add songs
    }

    public override void Update(GameTime gameTime)
    {
            
    }

    public virtual SoundEffect GetSoundEffect(string name)
    {
        if (!GameWrapper.Main.AudioLibrary.SoundEffects.ContainsKey(name)) return null;
        
        return GameWrapper.Main.AudioLibrary.SoundEffects[name];
    }
    
    public override void Destroy()
    {
        base.Destroy();
        // throw new System.NotImplementedException();
    }
}