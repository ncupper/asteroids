using System;

using Asteroids.Game;

namespace Asteroids.GUI
{
    public class MainGame : IDisposable
    {
        private readonly MainGameView _view;
        private readonly IObservableVariable<float> _playerSpeed;
        private readonly IObservableVariable<int> _scores;
        private readonly IObservableVariable<int> _round;

        public MainGame(MainGameView view,
            IObservableVariable<float> playerSpeed, IObservableVariable<int> scores, IObservableVariable<int> round)
        {
            _view = view;

            _playerSpeed = playerSpeed;
            _playerSpeed.Changed += OnPlayerSpeedChanged;

            _scores = scores;
            _scores.Changed += OnScoresChanged;

            _round = round;
            _round.Changed += OnRoundChanged;
        }

        public void Dispose()
        {
            _playerSpeed.Changed -= OnPlayerSpeedChanged;
            _scores.Changed -= OnScoresChanged;
            _round.Changed -= OnRoundChanged;
        }

        private void OnPlayerSpeedChanged(float value)
        {
            _view.PlayerVelocity.text = value.ToString("0.0");
        }

        private void OnScoresChanged(int value)
        {
            _view.Scores.text = value.ToString();
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
