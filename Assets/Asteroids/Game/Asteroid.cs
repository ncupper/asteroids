using UnityEngine;
namespace Asteroids.Game
{
    public class Asteroid : IMovable
    {
        private readonly AsteroidView _view;
        private readonly int _size;
        private readonly SpaceField _field;

        public Asteroid(AsteroidView view, int size, SpaceField field, Vector3 velocity)
        {
            _view = view;
            _size = size;
            _field = field;
            Velocity = velocity;
        }

        public Vector3 Velocity { get; set; }

        public void Move(float deltaTime)
        {
            Vector3 pos = _view.Self.position + deltaTime * Velocity;
            pos = _field.CorrectPosition(pos);
            _view.Self.position = pos;
        }
    }
}
