using UnityEngine;
namespace Asteroids.Game
{
    public class Asteroid : IMovable
    {
        private readonly AsteroidView _view;
        private readonly int _size;
        private readonly SpaceField _field;
        private readonly Vector3 _velocity;

        public Asteroid(AsteroidView view, int size, SpaceField field, Vector3 velocity)
        {
            _view = view;
            _size = size;
            _field = field;
            _velocity = velocity;

            _view.Size = _size;
        }

        public void Move(float deltaTime)
        {
            Vector3 pos = _view.Self.position + deltaTime * _velocity;
            pos = _field.CorrectPosition(pos);
            _view.Self.position = pos;
        }
    }
}
