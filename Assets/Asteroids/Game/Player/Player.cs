using System;
using System.Collections.Generic;
using System.Linq;

using Asteroids.Inputs;

using UnityEngine;
namespace Asteroids.Game
{
    public class Player : IDestroyable
    {
        private const float Braking = 20;
        private const float Acceleration = 20;
        private const float MaxVelocity = 20;

        private readonly PlayerView _view;
        private readonly SpaceField _field;

        private Vector3 _velocity;
        private Vector3 _accelerate;

        public Player(PlayerView view, SpaceField field)
        {
            _view = view;
            _field = field;

            VelocityValue = new ObservableVariable<float>();
        }

        public ObservableVariable<float> VelocityValue { get; private set; }

        public void Spawn()
        {
            _view.gameObject.SetActive(true);
            _view.Self.position = Vector3.zero;
        }

        public void UpdateInput(GameInput gameInput, float deltaTime)
        {
            if (gameInput.Gameplay.Rotate.IsInProgress())
            {
                Vector3 angles = _view.Self.localEulerAngles;
                angles.z += gameInput.Gameplay.Rotate.ReadValue<float>();
                _view.Self.localEulerAngles = angles;
            }

            if (gameInput.Gameplay.Accelerate.IsPressed())
            {
                _velocity += deltaTime * Acceleration * _view.Self.up;
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

        public void Simulate(float deltaTime, IReadOnlyList<ICollideable> asteroids, ICollideable ufo)
        {
            _view.Self.position = _field.CorrectPosition(_view.Self.position + deltaTime * VelocityValue.Value * _velocity.normalized);

            ICollideable hit = asteroids.FirstOrDefault(x => _view.Collider.Distance(x.Collider).isOverlapped);
            if (hit != null
                || (ufo != null && _view.Collider.Distance(ufo.Collider).isOverlapped))
            {
                Destroy();
            }
        }

        public event Action<IDestroyable> Destroyed;

        public void Destroy()
        {
            _view.gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }
    }
}
