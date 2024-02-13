using System;

namespace Asteroids.Game
{
    public interface IDestroyable<out T>
    {
        event Action<T> Destroyed;

        void Destroy();
    }
}
