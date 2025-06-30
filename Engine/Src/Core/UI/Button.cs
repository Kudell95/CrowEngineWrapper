using System;
using System.Runtime.CompilerServices;
using CosmicCrowGames.Core.Components;
using CosmicCrowGames.Core.Components.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicCrowGames.Core;

public class Button : GUIEntity
{
    //button params.
    private string m_ButtonText;
    private Vector2 m_startingPosition;
    private Color m_ButtonColor;
    private Color m_FontColor;
    private Color m_PressedColor;
    private Vector2 m_Bounds;
    private Action m_OnEnter;
    private Action m_OnLeave;
    private Action m_OnMButtonDown;
    private Action m_OnMButtonUp;
    private AnchorPoint m_AnchorPoint;
    
    
    protected Button(SpriteBatch spriteBatch) : base(spriteBatch)
    {
    }

    protected Button(Vector2 position, SpriteBatch spriteBatch) : base(position, spriteBatch)
    {
    }


    public override void Initialize()
    {
        base.Initialize();
        
        //setup button.
        GetComponent<AnchoredTransform>().Anchor = m_AnchorPoint;
        GetComponent<AnchoredTransform>().LocalPosition = m_startingPosition;
        Width = m_Bounds.X;
        Height = m_Bounds.Y;
        
        Sprite2D sprite = GetComponent<Sprite2D>();
        sprite.SpriteColor = Color.White;
        Text textObject = new Text(m_ButtonText,sprite.ImageRectangle, 70).SetColour(m_FontColor);
        AddComponent(textObject);
        
        //UI Events
        OnMouseEnter += m_OnEnter;
        OnMouseLeave += m_OnLeave;
        OnMouseButtonDown += m_OnMButtonDown;
        OnMouseButtonUp += m_OnMButtonUp;
    }

    public static Button NewInstance(string buttonText, Vector2 position, Vector2 bounds, SpriteBatch spriteBatch, Color buttonColour, Color fontColour, Color hoverColor, Color pressedColor, 
        Action onMouseOver, Action onMouseExit, Action onMouseButtonDown, Action onMouseButtonUp, AnchorPoint anchorPoint)
    {
        Button button = new Button(position, spriteBatch)
        {
            m_ButtonColor = buttonColour,
            m_ButtonText = buttonText,
            m_Bounds = bounds,
            m_OnEnter = onMouseOver,
            m_OnLeave = onMouseExit,
            m_OnMButtonDown = onMouseButtonDown,
            m_OnMButtonUp = onMouseButtonUp,
            m_AnchorPoint = anchorPoint,
            m_FontColor = fontColour,
            m_startingPosition =  position
            
        };

        button.OnMouseEnter += () =>
        {
            button.SetColour(hoverColor);
        };

        button.OnMouseLeave += () =>
        {
            button.SetColour(buttonColour);
        };
        
        button.OnMouseButtonDown += () =>
        {
            button.SetColour(pressedColor);
        };

        button.OnMouseButtonUp += () =>
        {
            button.SetColour(hoverColor);
        };


        return button;
    }

    public void SetColour(Color colour)
    {
        Sprite2D sprite = GetComponent<Sprite2D>();
        sprite.SpriteColor = colour;
    }

    public static Button NewInstance(string buttonText, Vector2 position, AnchorPoint anchor, Action onClick)
    {
        Vector2 bounds = new Vector2(300, 100);
        return NewInstance(buttonText, onClick, position, bounds, GameWrapper.Main.SceneManager.CurrentScene.SpriteBatch, anchor);
    }
    public static Button NewInstance(string buttonText, Action onClick, Vector2 position, SpriteBatch spriteBatch, AnchorPoint anchor)
    {
        Vector2 bounds = new Vector2(300, 100);
        return NewInstance(buttonText, onClick, position, bounds, spriteBatch, anchor);
    }
    public static Button NewInstance(string buttonText, Action onClick, Vector2 position, Vector2 bounds, SpriteBatch spriteBatch, AnchorPoint anchor = AnchorPoint.None)
    {

        Color fontColor = Color.Black;
        Color buttonColor = Color.White;
        Color hoverColor = Color.DarkGray;
        Color pressedColor = Color.Gray;

        return NewInstance(buttonText, position, bounds, spriteBatch, buttonColor, fontColor, hoverColor, pressedColor, null, null,
            null, onClick, anchor);
    }
    
    
    /*
     *GUIEntity BuildButton(Vector2 position, Vector2 Bounds, string text, Color color, Color fontColor, AnchorPoint anchorPoint, Action OnClickAction)
        {
            GUIEntity button = (GUIEntity)Instantiate(GUIEntity.NewInstance(position, SpriteBatch));
            button.GetComponent<AnchoredTransform>().Anchor = anchorPoint;
            button.GetComponent<AnchoredTransform>().LocalPosition = position;
            button.Width = Bounds.X;
            button.Height = Bounds.Y;
            Color ButtonEntryColor = color;
            Color ButtonDefaultColor = Color.White;

            Color ButtonPressedColor = Color.DarkGray;
            Sprite2D sprite = button.GetComponent<Sprite2D>();
            sprite.SpriteColor = Color.White;
            Text textObject = new Text(text,sprite.ImageRectangle, 70).SetColour(fontColor);
            button.AddComponent(textObject);
            button.OnMouseEnter += ()=>{
                sprite.SpriteColor = ButtonEntryColor;
            };

            button.OnMouseLeave += ()=>{
                sprite.SpriteColor = ButtonDefaultColor;
            };

            button.OnMouseButtonDown += ()=>{
                sprite.SpriteColor = ButtonPressedColor;
            };

            button.OnMouseButtonUp += ()=>{
                sprite.SpriteColor = ButtonEntryColor;
            };

            return button;
        }
     * 
     */
}