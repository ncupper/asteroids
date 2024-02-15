using System.Collections.Generic;
using System.Linq;

using Asteroids.Game.Actors;

using UnityEngine;
namespace Asteroids.Game
{
    public class Asteroid : Actor<AsteroidView>
    {
        private readonly SpaceField _field;
        private readonly Vector3 _velocity;

        public Asteroid(AsteroidView view, int size, SpaceField field, Vector3 velocity)
            : base(view)
        {
            Size = size;
            _field = field;
            _velocity = velocity;
        }

        public int Size { get; }

        public override void Move(float deltaTime)
        {
            Vector3 pos = View.Self.position + deltaTime * _velocity;
            pos = _field.CorrectPosition(pos);
            View.Self.position = pos;
        }
    }
}
