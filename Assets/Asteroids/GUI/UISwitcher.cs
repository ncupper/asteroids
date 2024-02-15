using UnityEngine;
namespace Asteroids.GUI
{
    public enum UIMode
    {
        StartGame,
        MainGame,
    }

    public class UISwitcher : MonoBehaviour
    {
        [field: SerializeField] public MainGameView MainGame { get; private set; }
        [field: SerializeField] public StartGameView StartGame { get; private set; }

        private StartGame _startGame;
        private MainGame _mainGame;

        public void Setup(Game.IObservableVariable<float> playerSpeed, Game.IObservableVariable<int> scores)
        {
            _startGame = new StartGame(StartGame);
            _mainGame = new MainGame(MainGame, playerSpeed, scores);
        }

        private void OnDestroy()
        {
            _mainGame?.Dispose();
        }

        public void SwitchTo(UIMode mode)
        {
            StartGame.gameObject.SetActive(mode == UIMode.StartGame);
            MainGame.gameObject.SetActive(mode == UIMode.MainGame);
        }
    }
}
