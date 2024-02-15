using System;

using Asteroids.Game;

namespace Asteroids.GUI
{
    public class MainGame : IDisposable
    {
        private readonly MainGameView _view;
        private readonly IObservableVariable<float> _playerSpeed;
        private readonly IObservableVariable<int> _scores;

        public MainGame(MainGameView view, IObservableVariable<float> playerSpeed, IObservableVariable<int> scores)
        {
            _view = view;

            _playerSpeed = playerSpeed;
            _playerSpeed.Changed += OnPlayerSpeedChanged;

            _scores = scores;
            _scores.Changed += OnScoresChanged;
        }

        public void Dispose()
        {
            _playerSpeed.Changed -= OnPlayerSpeedChanged;
            _scores.Changed -= OnScoresChanged;
        }

        private void OnPlayerSpeedChanged(float value)
        {
            _view.PlayerVelocity.text = value.ToString("0.0");
        }

        private void OnScoresChanged(int value)
        {
            _view.Scores.text = value.ToString();
        }
    }
}
