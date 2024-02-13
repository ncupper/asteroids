using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Asteroids.Game
{
    public class Ufo : IMovable, IDestroyable<Ufo>
    {
        private const float UfoSpeed = 10.0f;

        private readonly UfoView _view;
        private readonly Transform _target;

        public Ufo(UfoView view, SpaceField field, Transform target)
        {
            _view = view;
            _target = target;
            _view.Self.position = field.GetRandomPositionForPerimeterArea();
        }

        public event Action<Ufo> Destroyed;

        public Vector3 Positon => _view.Self.position;

        public void Move(float deltaTime)
        {
            Vector3 position = _view.Self.position;
            Vector3 targetDir = (_target.position - position).normalized;
            Vector3 angles = Quaternion.FromToRotation(_view.Self.up, targetDir).eulerAngles;

            _view.Self.localEulerAngles += new Vector3(0, 0, angles.z);
            _view.Self.position = position + deltaTime * UfoSpeed * _view.Self.up;
        }

        public void Destroy()
        {
            _view.gameObject.SetActive(false);
            Destroyed?.Invoke(this);
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
    }
}
