using System;
namespace Asteroids.Game
{
    public interface IObservableVariable<T>
    {
        event Action<T> Changed;
        T Value { get; set; }
    }
}
