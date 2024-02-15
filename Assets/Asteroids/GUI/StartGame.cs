using System;
namespace Asteroids.GUI
{
    public class StartGame : IDisposable
    {
        private StartGameView _view;

        public event Action StartClicked;

        public StartGame(StartGameView view)
        {
            _view = view;

            _view.Start.onClick.AddListener(OnStartClick);
        }

        public void Dispose()
        {
            _view.Start.onClick.RemoveListener(OnStartClick);
        }

        private void OnStartClick()
        {
            StartClicked?.Invoke();
        }
    }
}
