using System;

namespace Asteroids.GUI
{
    public class MainGame : IDisposable
    {
        private readonly MainGameView _view;
        private readonly Game.IObservable<float> _playerSpeed;

        public MainGame(MainGameView view, Game.IObservable<float> playerSpeed)
        {
            _view = view;
            _playerSpeed = playerSpeed;
            _playerSpeed.Changed += OnPlayerSpeedChanged;
        }

        private void OnPlayerSpeedChanged(float value)
        {
            _view.PlayerVelocity.text = value.ToString("0.0");
        }

        public void Dispose()
        {
            _playerSpeed.Changed -= OnPlayerSpeedChanged;
        }
    }
}
