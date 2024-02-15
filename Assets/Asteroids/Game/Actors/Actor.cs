using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Collide()
        {
            Destroy();
        }

        public bool IsTouchWith(ICollideable collideable)
        {
            return View.Collider.Distance(collideable.Collider).isOverlapped;
        }

        public ICollideable GetTouch(IReadOnlyCollection<ICollideable> collideables)
        {
            return collideables.FirstOrDefault(x => View.Collider.Distance(x.Collider).isOverlapped);
        }
    }
}
