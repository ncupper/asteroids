using System.Collections.Generic;
using System.Linq;

using Asteroids.Game.Actors;

using UnityEngine;
namespace Asteroids.Game
{
    public class Ufo : Actor
    {
        private const float UfoSpeed = 5.0f;

        private readonly IField _field;
        private readonly Transform _target;

        public Ufo(UfoView view, IField field, Transform target)
            : base(view)
        {
            _field = field;
            _target = target;
        }

        public void Spawn()
        {
            View.gameObject.SetActive(true);
            View.Self.position = _field.GetRandomPositionForPerimeterArea();
        }

        public override void Move(float deltaTime)
        {
            Vector3 position = View.Self.position;
            Vector3 targetDir = (_target.position - position).normalized;
            Vector3 angles = Quaternion.FromToRotation(View.Self.up, targetDir).eulerAngles;

            View.Self.localEulerAngles += new Vector3(0, 0, angles.z);
            View.Self.position = position + deltaTime * UfoSpeed * View.Self.up;
        }

        public bool IsAnyTouch(IReadOnlyList<ICollideable> bullets)
        {
            ICollideable hit = bullets.FirstOrDefault(x => View.Collider.Distance(x.Collider).isOverlapped);
            if (hit != null)
            {
                hit.Collide();
            }
            return hit != null;
        }
    }
}
