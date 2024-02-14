using System;
namespace Asteroids.Game
{
    public interface IObservable<T>
    {
        event Action<T> Changed;
        T Value { get; set; }
    }
}
