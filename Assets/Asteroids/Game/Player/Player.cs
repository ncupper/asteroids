using System;
using System.Collections.Generic;
using System.Linq;

using Asteroids.Game.Actors;
using Asteroids.Inputs;

using UnityEngine;
namespace Asteroids.Game
{
    public class Player : Actor<PlayerView>
    {
        private const float Braking = 20;
        private const float Acceleration = 20;
        private const float MaxVelocity = 20;

        private readonly SpaceField _field;

        private Vector3 _velocity;
        private Vector3 _accelerate;

        public Player(PlayerView view, SpaceField field) : base(view)
        {
            _field = field;

            VelocityValue = new ObservableVariable<float>();
        }

        public ObservableVariable<float> VelocityValue { get; private set; }

        public void Spawn()
        {
            View.gameObject.SetActive(true);
            View.Self.position = Vector3.zero;
        }

        public void UpdateInput(GameInput gameInput, float deltaTime)
        {
            if (gameInput.Gameplay.Rotate.IsInProgress())
            {
                Vector3 angles = View.Self.localEulerAngles;
                angles.z += gameInput.Gameplay.Rotate.ReadValue<float>();
                View.Self.localEulerAngles = angles;
            }

            if (gameInput.Gameplay.Accelerate.IsPressed())
            {
                _velocity += deltaTime * Acceleration * View.Self.up;
            }
            else
            {
                float velValue = _velocity.magnitude;
                velValue -= deltaTime * Braking;
                if (velValue > 0)
                {
                    _velocity = _velocity.normalized * velValue;
                }
                else
                {
                    _velocity = Vector3.zero;
                }
            }

            float velVal = _velocity.magnitude;
            velVal = Mathf.Clamp(velVal, 0, MaxVelocity);

            VelocityValue.Value = velVal;
        }

        public override void Move(float deltaTime)
        {
            View.Self.position = _field.CorrectPosition(View.Self.position + deltaTime * VelocityValue.Value * _velocity.normalized);
        }
    }
}
