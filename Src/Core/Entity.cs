

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using CosmicCrowGames.Core.Transform;

namespace CosmicCrowGames.Core;


/// <summary>
/// Base class for all entities
/// </summary>
public abstract class Entity
{
    #region  Private Variables
    private List<EntityProperty> _properties = new List<EntityProperty>();
    private List<Component> _components = new List<Component>();
    private Action onInitialise;
    private Action<GameTime> onDraw;
    private Action<GameTime> onUpdate;
    public static Action<Entity> onEntityCreated;
    private bool _active = true;
    private Guid _id;
    private string _name;
    #endregion

    #region  Properties
    public bool Active {
        get { return _active; }
        set { _active = value;}        
    }

    public Transform2D transform;
    public bool IsDestroyed { get; private set; }


    public string ID {
        get { return _id.ToString(); }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    #endregion
      
    protected Entity()
    {
        IsDestroyed = false;
        transform = new Transform2D();
        _id = Guid.NewGuid();
        // onEntityCreated?.Invoke(this);
        Name = this.GetType().Name;
    }

    protected Entity(Vector2 position) : this()
    {
        transform.Position = position;
    }

    public virtual void Initialize(){
        if(!Active)
            return;

        onInitialise?.Invoke();
    }
    public virtual void Update(GameTime gameTime)
    {
        if(!Active)
            return;

        onUpdate?.Invoke(gameTime);
    }
    public virtual void Draw(GameTime gameTime){
        if(!Active)
            return;
        onDraw?.Invoke(gameTime);
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
        for(int i = _components.Count-1; i >= 0; i--){
            _components[i].Destroy();
        }
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
    public virtual Entity RemoveProp(EntityProperty property)
    {
        _properties.Remove(property);
        return this;
    }

    
    public virtual Entity AddComponent(Component component)
    {
        if(component.Entity == null)
            component.Entity = this;
        _components.Add(component);

        onInitialise += component.Initialize;
        onUpdate += component.Update;
        onDraw += component.Draw;


        component.OnDisabled += OnComponentDisabled;
        component.OnEnabled += OnComponentEnabled;

        component.Initialize();

        return this;
    }


    public void OnComponentEnabled(Component component)
    {
        //Unsubscribe here to ensure we are only listening once.
        try{
            onInitialise -= component.Initialize;
            onUpdate -= component.Update;
            onDraw -= component.Draw;
            onInitialise += component.Initialize;
            onUpdate += component.Update;
            onDraw += component.Draw;
        }catch{}
    }

    public void OnComponentDisabled(Component component)
    {
        try{
            onInitialise -= component.Initialize;
            onUpdate -= component.Update;
            onDraw -= component.Draw;
        }catch{}
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

        parent.transform.AddChild(this.transform);
        return this;
    }


    public Entity ClearParent()
    {
        this.transform.Parent.RemoveChild(this.transform);
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

    public virtual void OnDestroy()
    {
        //unsubscribe from events.
        try{
            foreach(var component in _components)
            {
                component.OnDisabled -= OnComponentDisabled;
                component.OnEnabled -= OnComponentEnabled;
                onDraw -= component.Draw;
                onUpdate -= component.Update;
                onInitialise -= component.Initialize;

                component.Destroy();
            }
        }
        catch{}
        Active = false;
    }

}
