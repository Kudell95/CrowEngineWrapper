using System;
using CosmicCrowGames.Core;
using CrowEngine.Core.Helpers;
using Microsoft.Xna.Framework;

namespace CrowEngine.Core;

/// <summary>
/// 
/// </summary>
public class InteractableEntity : Entity
{
    public Color UniqueColour {get; private set;}
    
    protected static int m_NextId = 1;
    
    protected int m_ElementID = -1;
    
    private bool m_mouseOverElement;
    public Action OnMouseEnter;
    public Action OnMouseButtonDown;
    public Action OnMouseButtonUp;
    public Action OnMouseLeave;

    protected InteractableEntity()
    {
        
    }
    protected InteractableEntity(Vector2 position) : base(position)
    {
        
    }
    
    public override void Initialize()
    {
        base.Initialize();
        
         //setup ID/unique colour
         m_ElementID = m_NextId++;
         UniqueColour = ColourHelpers.ColorFromID(m_ElementID);
         GameWrapper.Main.MGUIInteractionMGR.Entities.TryAdd(m_ElementID, this);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
         if (m_mouseOverElement)
         {
             if(MouseUserInput.IsLeftClicked){
                 OnMouseButtonDown?.Invoke();
             }else if(MouseUserInput.IsLeftJustReleased){
                 OnMouseButtonUp?.Invoke();
             }
         }
    }

    public void MouseEnterListener()
    {
        if (m_mouseOverElement)
            return;
    
        m_mouseOverElement = true;
        OnMouseEnter?.Invoke();
    }
    
    public void MouseLeaveListener()
    {
        if (!m_mouseOverElement)
            return;
    
        m_mouseOverElement = false;
        OnMouseLeave?.Invoke();
    
    }
}