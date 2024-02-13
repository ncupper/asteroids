using System;

using UnityEngine;
namespace Asteroids.Game
{
    public class BulletView : MovableView, IDestroyable<BulletView>
    {
        [field: SerializeField] public Collider2D Collider { get; private set; }

        public event Action<BulletView> Destroyed;

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
