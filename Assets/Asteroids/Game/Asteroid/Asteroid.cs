using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Asteroids.Game
{
    public class Asteroid : IMovable, IDestroyable<Asteroid>
    {
        private readonly AsteroidView _view;
        private readonly SpaceField _field;
        private readonly Vector3 _velocity;

        public event Action<Asteroid> Destroyed;

        public Asteroid(AsteroidView view, int size, SpaceField field, Vector3 velocity)
        {
            _view = view;
            Size = size;
            _field = field;
            _velocity = velocity;
        }

        public Vector3 Positon => _view.Self.position;

        public int Size { get; }

        public void Move(float deltaTime)
        {
            Vector3 pos = _view.Self.position + deltaTime * _velocity;
            pos = _field.CorrectPosition(pos);
            _view.Self.position = pos;
        }

        public bool IsAnyTouch(IReadOnlyList<ICollideable> bullets)
        {
            ICollideable hit = bullets.FirstOrDefault(x => _view.Collider.Distance(x.Collider).isOverlapped);
            if (hit != null)
            {
                hit.Collide();
            }
            return hit != null;
        }

        public void Destroy()
        {
            _view.gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }
    }
}
