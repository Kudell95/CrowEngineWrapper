using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core;

public class Checkbox : GUIEntity
{
    private bool m_IsChecked;

    public bool IsChecked
    {
        get
        {
            return m_IsChecked;
        }
        set
        {
            m_IsChecked = value;
            //notify check changed.
            onCheckChanged(value);
        }
    }

    public Action<bool> OnValueChanged;




    protected Checkbox(SpriteBatch spriteBatch) : base(spriteBatch)
    {
    }

    protected Checkbox(Vector2 position, SpriteBatch spriteBatch) : base(position, spriteBatch)
    {
    }


    private void onCheckChanged(bool newValue)
    {
        OnValueChanged?.Invoke(newValue);
    }
    
    
    
}