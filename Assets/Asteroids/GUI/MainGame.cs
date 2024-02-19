using System;

using Asteroids.Game;

namespace Asteroids.GUI
{
    public class MainGame : IDisposable
    {
        private readonly MainGameView _view;
        private readonly IObservableVariable<float> _playerSpeed;
        private readonly IObservableVariable<int> _laserCharges;
        private readonly IObservableVariable<float> _laserTimer;
        private readonly IObservableVariable<int> _round;

        public MainGame(MainGameView view,
            IObservableVariable<float> playerSpeed,
            IObservableVariable<int> laserCharges, IObservableVariable<float> laserTimer,
            IObservableVariable<int> round)
        {
            _view = view;

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
            _playerSpeed.Changed -= OnPlayerSpeedChanged;

            _laserCharges.Changed -= OnLaserChargesChanged;
            _laserTimer.Changed -= OnLaserTimerChanged;

            _round.Changed -= OnRoundChanged;
        }

        private void OnPlayerSpeedChanged(float value)
        {
            _view.PlayerVelocity.text = value.ToString("0.0");
        }

        private void OnLaserChargesChanged(int value)
        {
            _view.LaserCharges.text = value.ToString();
        }

        private void OnLaserTimerChanged(float value)
        {
            _view.LaserRestoreTimer.gameObject.SetActive(!value.Equals(0));
            _view.LaserRestoreTimer.text = "(" + value.ToString("0.0") + ")";
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
