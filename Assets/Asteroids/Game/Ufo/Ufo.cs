using System.Collections.Generic;
using System.Linq;

using Asteroids.Game.Actors;

using UnityEngine;
namespace Asteroids.Game
{
    public class Ufo : Actor<UfoView>
    {
        private const float UfoSpeed = 10.0f;

        private readonly Transform _target;

        public Ufo(UfoView view, SpaceField field, Transform target)
            : base(view)
        {
            _target = target;
            View.Self.position = field.GetRandomPositionForPerimeterArea();
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
