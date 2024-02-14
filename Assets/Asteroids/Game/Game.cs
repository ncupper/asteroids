using Asteroids.GUI;
using Asteroids.Inputs;
namespace Asteroids.Game
{
    public class Game
    {
        private readonly UISwitcher _uiSwitcher;
        private readonly AsteroidSimulator _asteroidSimulator;
        private readonly BulletSimulator _bullets;
        private Ufo _ufo;
        private readonly Player _player;
        private readonly GameInput _gameInput;

        private IObservableVariable<int> _scores;

        public Game(SpaceField field,
            PlayerView playerView, AsteroidView asteroidView, BulletView bulletView, UfoView ufoView,
            UISwitcher uiSwitcher)
        {
            _uiSwitcher = uiSwitcher;

            _gameInput = new GameInput();
            _gameInput.Enable();

            _scores = new ObservableVariable<int>();

            _player = new Player(playerView, field);
            _asteroidSimulator = new AsteroidSimulator(field, asteroidView);
            _bullets = new BulletSimulator(field, bulletView, playerView.BulletPivot);
            _ufo = new Ufo(ufoView, field, playerView.transform);

            _uiSwitcher.Setup(_player.VelocityValue, _scores);
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();
        }

        public void StartRound(int round = 0)
        {
            _uiSwitcher.SwitchTo(UIMode.MainGame);

            _player.Spawn();

            _bullets.HideAll();

            _asteroidSimulator.StartupSpawn(round + 6);
        }

        public void UpdateInput(float deltaTime)
        {
            _player.UpdateInput(_gameInput, deltaTime);
        }

        public void Simulate(float deltaTime)
        {
            _player.Simulate(deltaTime, _asteroidSimulator.ActiveAsteroids, _ufo);
            _bullets.Simulate(deltaTime);
            _asteroidSimulator.Simulate(deltaTime, _bullets.ActiveBullets);

            if (_ufo != null)
            {
                _ufo.Move(deltaTime);
                if (_ufo.IsAnyTouch(_bullets.ActiveBullets))
                {
                    _ufo.Destroy();
                    _ufo = null;
                }
            }
        }
    }
}
