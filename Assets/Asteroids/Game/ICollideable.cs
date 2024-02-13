using UnityEngine;
namespace Asteroids.Game
{
    public interface ICollideable
    {
        Collider2D Collider { get; }

        void Collide();
    }
}
