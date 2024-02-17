using System;
namespace Asteroids.Game
{
    public class ObservableVariable<T> : IObservableVariable<T>
    {
        private T _value;

        public event Action<T> Changed;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }
    }
}
