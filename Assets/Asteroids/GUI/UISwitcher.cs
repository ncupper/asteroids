using UnityEngine;
namespace Asteroids.GUI
{
    public enum UIMode
    {
        None,
        MainGame,
        GameOver,
    }

    public class UISwitcher : MonoBehaviour
    {
        [field: SerializeField] public MainGameView MainGame { get; private set; }

        private MainGame _mainGame;

        public void Setup(Game.IObservableVariable<float> playerSpeed, Game.IObservableVariable<int> scores)
        {
            _mainGame = new MainGame(MainGame, playerSpeed, scores);
        }

        private void OnDestroy()
        {
            _mainGame?.Dispose();
        }

        public void SwitchTo(UIMode mode)
        {
        }
    }
}
