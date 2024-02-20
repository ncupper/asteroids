using Asteroids.Game.Actors;
using Asteroids.Inputs;
using Asteroids.Models;

using UnityEngine;
namespace Asteroids.Game
{
    public class Player : Actor
    {
        private readonly PlayerModel _model;
        private readonly IField _field;
        private readonly Laser _laser;

        private Vector3 _velocity;
        private Vector3 _accelerate;

        public Player(PlayerModel model, PlayerView view, IField field) : base(view)
        {
            _model = model;
            _field = field;
            _laser = new Laser(_model, view.Laser);
            VelocityValue = new ObservableVariable<float>();
        }

        public ObservableVariable<float> VelocityValue { get; }
        public ObservableVariable<float> LaserChargeTimer => _laser.ChargeTimer;
        public ObservableVariable<int> LaserChargesCount => _laser.ChargesCount;

        public override void Spawn()
        {
            base.Spawn();
            View.Self.position = Vector3.zero;
            LaserChargesCount.Value = _model.MaxLaserCharges;
            LaserChargeTimer.Value = 0;
        }

        public void UpdateInput(GameInput gameInput, float deltaTime, ActiveActorsContainer container)
        {
            if (gameInput.Gameplay.Rotate.IsInProgress())
            {
                Vector3 angles = View.Self.localEulerAngles;
                angles.z += gameInput.Gameplay.Rotate.ReadValue<float>();
                View.Self.localEulerAngles = angles;
            }

            if (gameInput.Gameplay.Accelerate.IsPressed())
            {
                _velocity += deltaTime * _model.Acceleration * View.Self.up;
            }
            else
            {
                float velValue = _velocity.magnitude;
                velValue -= deltaTime * _model.Braking;
                _velocity = velValue > 0
                    ? _velocity.normalized * velValue
                    : Vector3.zero;
            }

            _laser.UpdateInput(gameInput, deltaTime, container);

            float velVal = _velocity.magnitude;
            velVal = Mathf.Clamp(velVal, 0, _model.MaxVelocity);
            _velocity = _velocity.normalized * velVal;

            VelocityValue.Value = velVal;
        }

        public override void Move(float deltaTime)
        {
            View.Self.position = _field.CorrectPosition(View.Self.position + deltaTime * VelocityValue.Value * _velocity.normalized);
        }
    }
}
