

using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using CosmicCrowGames.Components;
using System;

namespace CosmicCrowGames.Foundation;


//TODO: parent/child behaviours.

/// <summary>
/// Base class for all entities
/// </summary>
public abstract class Entity
{
    private List<EntityProperty> _properties = new List<EntityProperty>();
    
    public bool IsDestroyed { get; private set; }

    public Transform2 transform;

    private List<Component> _components = new List<Component>();

    private Action onInitialise;
    private Action<GameTime> onDraw;
    private Action<GameTime> onUpdate;

      
    protected Entity()
    {
        IsDestroyed = false;

        transform = new Transform2();
    }

    public virtual void Initialize(){
        onInitialise?.Invoke();
    }
    public virtual void Update(GameTime gameTime)
    {
        onUpdate?.Invoke(gameTime);
    }
    public virtual void Draw(GameTime gameTime){
        onDraw?.Invoke(gameTime);
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        onDraw?.Invoke(gameTime);
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }
    

    /// <summary>
    /// Checks if a entity has a given property.
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public bool HasProp(EntityProperty property)
    {
        return _properties.Contains(property);
    }

    /// <summary>
    /// Adds a property to the entity, returns an entity so it can be chained together.
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public Entity AddProp(EntityProperty property)
    {
        if(HasProp(property))
            return this;

        _properties.Add(property);
        return this;
    }

    /// <summary>
    /// Removes the given property from the entity
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public Entity RemoveProp(EntityProperty property)
    {
        _properties.Remove(property);
        return this;
    }

    
    public Entity AddComponent(Component component)
    {
        if(component.Entity == null)
            component.Entity = this;
        _components.Add(component);

        onInitialise += component.Initialize;

        onUpdate += component.Update;

        onDraw += component.Draw;

        return this;
    }


    public Entity RemoveComponent(Component component)
    {
        _components.Remove(component);

        onInitialise -= component.Initialize;

        onUpdate -= component.Update;

        onDraw -= component.Draw;

        return this;
    }

    public bool HasComponent<T>() where T : Component
    {   
        if(_components == null || _components.Count == 0)
            return false;

        foreach(var component in _components)
        {
            if(component.GetType() == typeof(T))
                return true;
        }

        return false;
    }

    public T GetComponent<T>() where T : Component
    {
        if(_components == null || _components.Count == 0)
            return null;

        foreach(var component in _components)
        {
            if(component.GetType() == typeof(T))
                return (T)component;
        }

        return null;
    }

    public T TryGetComponent<T>() where T : Component
    {
        try{
            return GetComponent<T>();   
        }
        catch
        {
            //do nothing.
        }

        return default;
    }

}


