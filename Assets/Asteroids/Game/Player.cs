using Asteroids.Inputs;

using UnityEngine;
namespace Asteroids.Game
{
    public class Player
    {
        private const float Braking = 20;
        private const float Acceleration = 20;
        private const float MaxVelocity = 20;

        private readonly PlayerView _view;
        private GameInput _gameInput;

        private Vector3 _velocity;
        private Vector3 _accelerate;

        private SpaceField _field;

        public Player(PlayerView view, SpaceField field)
        {
            _view = view;

            _field = field;

            _gameInput = new GameInput();
            _gameInput.Enable();

            VelocityValue = new ObservableVar<float>();
        }

        public ObservableVar<float> VelocityValue { get; private set; }

        public void UpdateInput(float deltaTime)
        {
            if (_gameInput.Gameplay.Rotate.IsInProgress())
            {
                Vector3 angles = _view.Self.localEulerAngles;
                angles.z += _gameInput.Gameplay.Rotate.ReadValue<float>();
                _view.Self.localEulerAngles = angles;
            }

            if (_gameInput.Gameplay.Accelerate.IsPressed())
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

        public void Simulate(float deltaTime)
        {
            _view.Self.position = _field.CorrectPosition(_view.Self.position + deltaTime * VelocityValue.Value * _velocity.normalized);
        }
    }
}
