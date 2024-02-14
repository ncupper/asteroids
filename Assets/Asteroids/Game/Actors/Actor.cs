using System;

using UnityEngine;
namespace Asteroids.Game.Actors
{
    public abstract class Actor<TView> : IMovable, ICollideable, IDestroyable where TView : MovableView
    {
        protected readonly TView View;

        protected Actor(TView view)
        {
            View = view;
        }

        public event Action<IDestroyable> Destroyed;

        public void Destroy()
        {
            View.gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }

        public Vector3 Positon => View.Self.position;

        public abstract void Move(float deltaTime);

        public Collider2D Collider => View.Collider;

        public void Collide()
        {
            Destroy();
        }
    }
}
