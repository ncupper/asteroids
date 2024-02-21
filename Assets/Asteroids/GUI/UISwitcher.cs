using System;

using Asteroids.Game;

using UnityEngine;
namespace Asteroids.GUI
{
    public enum UIMode
    {
        StartGame,
        MainGame,
        GameOver
    }

    public class UISwitcher : MonoBehaviour
    {
        [field: SerializeField] public MainGameView MainGame { get; private set; }
        [field: SerializeField] public StartGameView StartGame { get; private set; }
        [field: SerializeField] public GameOverView GameOver { get; private set; }

        private StartGame _startGame;
        private MainGame _mainGame;
        private GameOver _gameOver;

        public event Action StartClicked;

        public void Setup(
            IObservableVariable<Vector2> playerPos, IObservableVariable<int> playerAngle, IObservableVariable<float> playerSpeed,
            IObservableVariable<int> laserCharges, IObservableVariable<float> laserTimer,
            IObservableVariable<int> round, IObservableVariable<int> scores)
        {
            _startGame = new StartGame(StartGame);
            _startGame.StartClicked += OnStartClicked;

            _mainGame = new MainGame(MainGame,
                playerPos, playerAngle, playerSpeed, laserCharges, laserTimer, round, scores);

            _gameOver = new GameOver(GameOver, scores);
            _gameOver.ContinueClicked += OnContinueClicked;
        }

        private void OnStartClicked()
        {
            StartClicked?.Invoke();
        }

        private void OnContinueClicked()
        {
            SwitchTo(UIMode.StartGame);
        }

        private void OnDestroy()
        {
            _startGame.StartClicked -= OnStartClicked;
            _startGame?.Dispose();

            _mainGame?.Dispose();

            _gameOver.ContinueClicked -= OnContinueClicked;
            _gameOver.Dispose();
        }

        public void SwitchTo(UIMode mode)
        {
            StartGame.gameObject.SetActive(mode == UIMode.StartGame);
            MainGame.gameObject.SetActive(mode == UIMode.MainGame);
            GameOver.gameObject.SetActive(mode == UIMode.GameOver);
        }
    }
}
