using System;

using UnityEngine;
namespace Asteroids.Game
{
    public class Bullet : IMovable, ICollideable, IDestroyable<Bullet>
    {
        private readonly BulletView _view;
        private readonly Vector3 _velocity;

        public event Action<Bullet> Destroyed;

        public Bullet(BulletView view, Vector3 velocity)
        {
            _view = view;
            _velocity = velocity;
        }

        public Collider2D Collider => _view.Collider;

        public void Collide()
        {
            Destroy();
        }

        public Vector3 Positon => _view.Self.position;

        public void Move(float deltaTime)
        {
            _view.Self.position += deltaTime * _velocity;
        }

        public void Destroy()
        {
            _view.Self.gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }

    }
}
