using Asteroids.Inputs;

using System;

using UnityEngine;

namespace Asteroids.Game
{
    public class PlayerView : MonoBehaviour
    {
        private const float Braking = 20;
        private const float Acceleration = 20;
        private const float MaxVelocity = 20;

        [field: SerializeField] public Transform BulletPivot { get; private set; }

        private GameInput _gameInput;

        private Vector3 _velocity;
        private Vector3 _accelerate;

        private SpaceField _field;

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();

            _field = new SpaceField(Camera.main);
        }

        private void Update()
        {
            if (_gameInput.Gameplay.Rotate.IsInProgress())
            {
                Vector3 angles = transform.localEulerAngles;
                angles.z += _gameInput.Gameplay.Rotate.ReadValue<float>();
                transform.localEulerAngles = angles;
            }

            if (_gameInput.Gameplay.Accelerate.IsPressed())
            {
                _velocity += Time.deltaTime * Acceleration * transform.up;
            }
            else
            {
                float velValue = _velocity.magnitude;
                velValue -= Time.deltaTime * Braking;
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

            transform.position = _field.CorrectPosition(transform.position + Time.deltaTime * velVal * _velocity.normalized);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(Camera.main.WorldToViewportPoint(transform.position));
            }
        }
    }
}
