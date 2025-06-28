using System;
using CosmicCrowGames.Core;
using CrowEngine.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrowEngine.Core.Managers;

public class MouseGuiInteractionManager : Manager
{
    public int CurrentHoveredGuiId = -1;

    private int m_currentMX = -1, m_currentMY = -1;

    public Action<int> OnMouseOver;
    public Action<int> OnMouseLeave;
    
    public override void Initialize()
    {
        
    }

    public override void Draw(GameTime gameTime)
    {
        base.Update(gameTime);

        int mx = Mouse.GetState().X;
        int my = Mouse.GetState().Y;
        if (mx != m_currentMX || my != m_currentMY)
        {
            m_currentMX = mx;
            m_currentMY = my;
            Color[] pixel = new Color[1];
            //there is a coloured pixel below the mouse.
            try
            {
                GameWrapper.Main.IDBuffer.GetData(0, new Rectangle(mx, my, 1, 1), pixel, 0, 1);
                if (pixel != null && pixel.Length > 0)
                {
                    int id = ColourHelpers.IDFromColor(pixel[0]);

                    if (id != CurrentHoveredGuiId)
                    {
                        if (CurrentHoveredGuiId != -1)
                            OnMouseLeave?.Invoke(CurrentHoveredGuiId);

                        CurrentHoveredGuiId = id;
                        OnMouseOver?.Invoke(id);
                        Console.WriteLine($"Mouse over: id {id}");
                    }

                }
                else
                {
                    if (CurrentHoveredGuiId != -1)
                    {
                        OnMouseLeave?.Invoke(CurrentHoveredGuiId);
                        CurrentHoveredGuiId = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (CurrentHoveredGuiId != -1)
                {
                    OnMouseLeave?.Invoke(CurrentHoveredGuiId);
                    CurrentHoveredGuiId = -1;
                }
            }
            

        }

    }
}