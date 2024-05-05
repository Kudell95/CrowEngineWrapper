

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CosmicCrowGames.Components;
using System;
using CosmicCrowGames.Foundation.Transform;

namespace CosmicCrowGames.Foundation;


//TODO: parent/child behaviours.

/// <summary>
/// Base class for all entities
/// </summary>
public abstract class Entity
{
    private List<EntityProperty> _properties = new List<EntityProperty>();
    
    public bool IsDestroyed { get; private set; }

    //TODO: probably switch to something like so for the transform : https://github.com/dotnet-ad/Transform/blob/master/src/Transform/Transform2D.cs
    //Current transform has no support for child/parents. - or implement own transform.
    public Transform2D transform;

    private List<Component> _components = new List<Component>();

    private Action onInitialise;
    private Action<GameTime> onDraw;
    private Action<GameTime> onUpdate;

    public static Action<Entity> onEntityCreated;

      
    protected Entity()
    {
        IsDestroyed = false;
        transform = new Transform2D();

        onEntityCreated?.Invoke(this);
    }

    protected Entity(Vector2 position){
        IsDestroyed = false;
        transform = new Transform2D();
        transform.Position = position;
        onEntityCreated?.Invoke(this);
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

    public Entity SetParent(Entity parent)
    {
        if(parent == null)
            return this;

        transform.Parent = parent.transform;
        return this;
    }


    public Entity ClearParent()
    {
        transform.Parent = null;
        return this;
    }

    
    public Entity SetPosition(Vector2 position)
    {
        transform.Position = position;
        return this;
    }

    public Entity SetRotation(float rotation)
    {
        transform.Rotation = rotation;
        return this;
    }

    public Entity SetScale(Vector2 scale)
    {
        transform.Scale = scale;
        return this;
    }

}
