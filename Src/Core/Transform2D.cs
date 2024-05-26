

namespace CosmicCrowGames.Core.Transform
{

    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    public class Transform2D
    {
        private Vector2 localPosition;
        public Vector2 LocalPosition
        {
            get => localPosition;
            set
            {
                localPosition = value;
                UpdateWorldTransform();
            }
        }

        private float localRotation;
        public float LocalRotation
        {
            get => localRotation;
            set
            {
                localRotation = value;
                UpdateWorldTransform();
            }
        }

        private Vector2 localScale;
        public Vector2 LocalScale
        {
            get => localScale;
            set
            {
                localScale = value;
                UpdateWorldTransform();
            }
        }

        public Vector2 Pivot { get; set; }

        public Transform2D Parent { get; private set; }
        public List<Transform2D> Children { get; private set; }

        private Matrix worldTransform;
        public Matrix WorldTransform => worldTransform;

        public Transform2D()
        {
            localPosition = Vector2.Zero;
            localRotation = 0f;
            localScale = Vector2.One;
            Pivot = new Vector2(0.5f, 0.5f);
            Children = new List<Transform2D>();
            UpdateWorldTransform();
        }

        private void UpdateWorldTransform()
        {
            Matrix translationMatrix = Matrix.CreateTranslation(new Vector3(-(Pivot * Scale), 0)) * Matrix.CreateTranslation(new Vector3(localPosition, 0));
            Matrix rotationMatrix = Matrix.CreateRotationZ(localRotation);
            Matrix scaleMatrix = Matrix.CreateScale(new Vector3(localScale, 1));

            var localTransform = scaleMatrix * rotationMatrix * translationMatrix;

            if (Parent != null)
            {
                worldTransform = localTransform * Parent.WorldTransform;
            }
            else
            {
                worldTransform = localTransform;
            }

            foreach (var child in Children)
            {
                child.UpdateWorldTransform();
            }
        }

        public Vector2 Position
        {
            get => Vector2.Transform(Vector2.Zero, worldTransform);
            set
            {
                if (Parent == null)
                {
                    LocalPosition = value;
                }
                else
                {
                    Matrix parentWorldTransform = Parent.WorldTransform;
                    Matrix inverseParentWorldTransform = Matrix.Invert(parentWorldTransform);
                    LocalPosition = Vector2.Transform(value, inverseParentWorldTransform);
                }
            }
        }

        public float Rotation
        {
            get => localRotation + (Parent?.Rotation ?? 0);
            set
            {
                if (Parent == null)
                {
                    LocalRotation = value;
                }
                else
                {
                    LocalRotation = value - Parent.Rotation;
                }
            }
        }

        public Vector2 Scale
        {
            get => localScale * (Parent?.Scale ?? Vector2.One);
            set
            {
                if (Parent == null)
                {
                    LocalScale = value;
                }
                else
                {
                    LocalScale = new Vector2(value.X / Parent.Scale.X, value.Y / Parent.Scale.Y);
                }
            }
        }

        public void AddChild(Transform2D child)
        {
            if (child.Parent != null)
            {
                child.Parent.Children.Remove(child);
            }
            child.Parent = this;
            Children.Add(child);
            child.UpdateWorldTransform();
        }

        public void RemoveChild(Transform2D child)
        {
            if (Children.Contains(child))
            {
                child.Parent = null;
                Children.Remove(child);
                child.UpdateWorldTransform();
            }
        }
       
    }

    
}