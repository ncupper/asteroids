using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Asteroids.Game.Actors
{
    public abstract class Actor : IMovable, ICollideable, IDestroyable
    {
        protected readonly MovableView View;

        protected Actor(MovableView view)
        {
            View = view;
        }

        public bool IsAlive => View.gameObject.activeSelf;

        public event Action<IDestroyable> Destroyed;

        public void Destroy()
        {
            View.gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }

        public Vector3 Positon => View.Self.position;

        public abstract void Move(float deltaTime);

        public Collider2D Collider => View.Collider;
        public int Layer => View.gameObject.layer;

        public void Collide()
        {
            Destroy();
        }

        public ICollideable GetTouch(IReadOnlyCollection<ICollideable> collideables, int layer)
        {
            return collideables.FirstOrDefault(
                x => x.IsAlive
                    && x.Collider.gameObject.layer == layer
                    && View.Collider.Distance(x.Collider).isOverlapped);
        }

        public virtual void Spawn()
        {
            View.gameObject.SetActive(true);
        }
    }
}
