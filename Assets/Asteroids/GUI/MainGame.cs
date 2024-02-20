using System;

using Asteroids.Game;

using UnityEngine;

namespace Asteroids.GUI
{
    public class MainGame : IDisposable
    {
        private readonly MainGameView _view;
        private readonly IObservableVariable<Vector2> _playerPos;
        private readonly IObservableVariable<int> _playerAngle;
        private readonly IObservableVariable<float> _playerSpeed;
        private readonly IObservableVariable<int> _laserCharges;
        private readonly IObservableVariable<float> _laserTimer;
        private readonly IObservableVariable<int> _round;

        public MainGame(MainGameView view,
            IObservableVariable<Vector2> playerPos, IObservableVariable<int> playerAngle, IObservableVariable<float> playerSpeed,
            IObservableVariable<int> laserCharges, IObservableVariable<float> laserTimer,
            IObservableVariable<int> round)
        {
            _view = view;

            _playerPos = playerPos;
            _playerPos.Changed += OnPlayerPositionChanged;

            _playerAngle = playerAngle;
            _playerAngle.Changed += OnPlayerAngleChanged;

            _playerSpeed = playerSpeed;
            _playerSpeed.Changed += OnPlayerSpeedChanged;

            _laserCharges = laserCharges;
            _laserCharges.Changed += OnLaserChargesChanged;

            _laserTimer = laserTimer;
            _laserTimer.Changed += OnLaserTimerChanged;

            _round = round;
            _round.Changed += OnRoundChanged;
        }

        public void Dispose()
        {
            _playerPos.Changed -= OnPlayerPositionChanged;
            _playerAngle.Changed -= OnPlayerAngleChanged;
            _playerSpeed.Changed -= OnPlayerSpeedChanged;

            _laserCharges.Changed -= OnLaserChargesChanged;
            _laserTimer.Changed -= OnLaserTimerChanged;

            _round.Changed -= OnRoundChanged;
        }

        private void OnPlayerPositionChanged(Vector2 value)
        {
            _view.PlayerPosition.text = value.x.ToString("00.0") + ":" + value.y.ToString("00.0");
        }

        private void OnPlayerAngleChanged(int value)
        {
            _view.PlayerAngle.text = value.ToString("000");
        }

        private void OnPlayerSpeedChanged(float value)
        {
            _view.PlayerVelocity.text = value.ToString("00.0");
        }

        private void OnLaserChargesChanged(int value)
        {
            _view.LaserCharges.text = value.ToString();
        }

        private void OnLaserTimerChanged(float value)
        {
            _view.LaserRestoreTimer.gameObject.SetActive(!value.Equals(0));
            _view.LaserRestoreTimer.text = "(" + value.ToString("00.0") + ")";
        }

        private void OnRoundChanged(int value)
        {
            _view.Round.text = $"Round {value}";

            //start autoplay animation:
            _view.Round.gameObject.SetActive(false);
            _view.Round.gameObject.SetActive(true);
        }
    }
}
