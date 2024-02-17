using Asteroids.Game.Actors;

using UnityEngine;
namespace Asteroids.Game
{
    public class Bullet : Actor
    {
        private readonly Vector3 _velocity;

        public Bullet(BulletView view, Vector3 velocity)
            : base(view)
        {
            _velocity = velocity;
        }

        public override void Move(float deltaTime)
        {
            View.Self.position += deltaTime * _velocity;
        }

    }
}
