using Asteroids.Game.Actors;
using Asteroids.Models;

using UnityEngine;
namespace Asteroids.Game
{
    public class Ufo : Actor
    {
        private readonly UfoModel _model;
        private readonly IField _field;
        private readonly Transform _target;

        public Ufo(UfoModel model, UfoView view, IField field, Transform target)
            : base(view)
        {
            _model = model;
            _field = field;
            _target = target;
        }

        public override void Spawn()
        {
            base.Spawn();
            View.Self.position = _field.GetRandomPositionForPerimeterArea();
        }

        public override void Move(float deltaTime)
        {
            Vector3 position = View.Self.position;
            Vector3 targetDir = (_target.position - position).normalized;
            Vector3 angles = Quaternion.FromToRotation(View.Self.up, targetDir).eulerAngles;

            View.Self.localEulerAngles += new Vector3(0, 0, angles.z);
            View.Self.position = position + deltaTime * _model.Speed * View.Self.up;
        }
    }
}
