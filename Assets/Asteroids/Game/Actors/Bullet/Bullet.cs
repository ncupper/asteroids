using Asteroids.Game.Actors;

using UnityEngine;
namespace Asteroids.Game
{
    public class Bullet : Actor
    {
        private readonly IField _field;

        public Bullet(BulletView view, IField field)
            : base(view)
        {
            _field = field;
        }

        public Vector3 Velocity { get; set; }

        public override void Move(float deltaTime)
        {
            View.Self.position += deltaTime * Velocity;
            if (_field.IsOut(Positon))
            {
                Destroy();
            }
        }

    }
}
