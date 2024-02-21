using System;

using Asteroids.Game;
namespace Asteroids.GUI
{
    public class GameOver
    {
        private readonly GameOverView _view;
        private readonly IObservableVariable<int> _scores;

        public event Action ContinueClicked;

        public GameOver(GameOverView view, IObservableVariable<int> scores)
        {
            _view = view;

            _view.Continue.onClick.AddListener(OnStartClick);

            _scores = scores;
            _scores.Changed += OnScoresChanged;
        }

        public void Dispose()
        {
            _view.Continue.onClick.RemoveListener(OnStartClick);
            _scores.Changed -= OnScoresChanged;
        }

        private void OnStartClick()
        {
            ContinueClicked?.Invoke();
        }

        private void OnScoresChanged(int value)
        {
            _view.Result.text = $"{value}";
        }

    }
}
