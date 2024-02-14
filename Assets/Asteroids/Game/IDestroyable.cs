using System;

namespace Asteroids.Game
{
    public interface IDestroyable
    {
        event Action<IDestroyable> Destroyed;

        void Destroy();
    }
}
