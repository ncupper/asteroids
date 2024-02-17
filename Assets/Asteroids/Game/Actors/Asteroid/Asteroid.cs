using Asteroids.Game.Actors;

using UnityEngine;
namespace Asteroids.Game
{
    public class Asteroid : Actor
    {
        private readonly SpaceField _field;
        private readonly Vector3 _velocity;

        public Asteroid(AsteroidView view, SpaceField field, Vector3 velocity)
            : base(view)
        {
            _field = field;
            _velocity = velocity;
        }

        public override void Move(float deltaTime)
        {
            Vector3 pos = View.Self.position + deltaTime * _velocity;
            pos = _field.CorrectPosition(pos);
            View.Self.position = pos;
        }
    }
}
