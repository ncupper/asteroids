using Asteroids.Game.Actors;

using UnityEngine;
namespace Asteroids.Game
{
    public class Asteroid : Actor
    {
        private readonly IField _field;

        public Asteroid(AsteroidView view, IField field)
            : base(view)
        {
            _field = field;
        }

        public Vector3 Velocity { get; set; }

        public override void Move(float deltaTime)
        {
            Vector3 pos = View.Self.position + deltaTime * Velocity;
            pos = _field.CorrectPosition(pos);
            View.Self.position = pos;
        }
    }
}
