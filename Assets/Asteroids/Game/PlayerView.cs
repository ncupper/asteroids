using Asteroids.Inputs;

using UnityEngine;

namespace Asteroids.Game
{
    public class PlayerView : MonoBehaviour
    {
        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
        }

        private void Update()
        {
            if (_gameInput.Gameplay.Rotate.IsInProgress())
            {
                var angles = transform.localEulerAngles;
                angles.z += _gameInput.Gameplay.Rotate.ReadValue<float>();
                transform.localEulerAngles = angles;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(Camera.main.WorldToViewportPoint(transform.position));
            }
        }
    }
}
