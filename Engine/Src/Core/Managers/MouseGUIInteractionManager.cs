using System;
using System.Collections.Generic;
using System.Linq;
using CosmicCrowGames.Core;
using CrowEngine.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrowEngine.Core.Managers;

//surely we can come up with a better name than this!
public class MouseGuiInteractionManager : Manager
{
    public int CurrentHoveredGuiId = -1;

    private int m_currentMX = -1;
    private int m_currentMY = -1;
    
    public Dictionary<int, InteractableEntity> Entities = new Dictionary<int, InteractableEntity>();
    
    Color[] m_Pixel = new Color[1];
    
    public override void Initialize()
    {
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        int mx = Mouse.GetState().X;
        int my = Mouse.GetState().Y;
        if ((mx != m_currentMX || my != m_currentMY) && IsMouseInWindow(mx,my))
        {
            m_currentMX = mx;
            m_currentMY = my;
            //there is a coloured pixel below the mouse.
            try
            {
                GameWrapper.Main.IDBuffer.GetData(0, new Rectangle(mx, my, 1, 1), m_Pixel, 0, 1);
                if (m_Pixel != null && m_Pixel.Length > 0)
                {
                    int id = ColourHelpers.IDFromColor(m_Pixel[0]);

                    if (id != CurrentHoveredGuiId)
                    {
                        if (CurrentHoveredGuiId != -1)
                            NotifyOnMouseExit(CurrentHoveredGuiId);

                        CurrentHoveredGuiId = id;
                        NotifyOnMouseEnter(id);
                        Console.WriteLine($"Mouse over: id {id}");
                    }

                }
                else
                {
                    if (CurrentHoveredGuiId != -1)
                    {
                        NotifyOnMouseExit(CurrentHoveredGuiId);
                        CurrentHoveredGuiId = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (CurrentHoveredGuiId != -1)
                {
                    NotifyOnMouseExit(CurrentHoveredGuiId);
                    CurrentHoveredGuiId = -1;
                }
            } 
        }

    }

    private void NotifyOnMouseEnter(int id)
    {
        if (Entities != null && Entities.ContainsKey(id))
        {
            Entities[id]?.MouseEnterListener();
        }
    }

    private void NotifyOnMouseExit(int id)
    {
        if (Entities != null && Entities.ContainsKey(id))
        {
            Entities[id]?.MouseLeaveListener();
        }
    }

    private bool IsMouseInWindow(int x, int y)
    {
        Point pos = new Point(x, y);
        return GameWrapper.Main.GraphicsDevice.Viewport.Bounds.Contains(pos);
    }

    public void ClearEntities()
    {
        Entities.Clear();
    }

    public void LogEntities()
    {
        for (int i = 0; i < Entities?.Keys.Count; i++)
        {
            Console.WriteLine(Entities.Keys.ElementAt(i));
        }
    }
}