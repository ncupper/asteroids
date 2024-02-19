using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public interface ICollideable
    {
        public bool IsAlive { get; }

        Collider2D Collider { get; }
        int Layer { get; }

        void Collide();

        ICollideable GetTouch(IReadOnlyCollection<ICollideable> collideables, int layer);
    }
}
