using Asteroids.Game;

using Asteroids.Game.Actors.Asteroid;
using Asteroids.Game.Actors.Bullet;
using Asteroids.GUI;
using Asteroids.Models;

using UnityEngine;
namespace Asteroids
{
    public class GameContext : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private TextAsset _config;
        [Header("Prefabs")]
        [SerializeField] private PlayerView _playerPrefab;
        [SerializeField] private AsteroidView _asteroidBigPrefab;
        [SerializeField] private AsteroidView _asteroidSmallPrefab;
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private UfoView _ufoPrefab;
        [Header("GUI")]
        [SerializeField] private UISwitcher _uiSwitcher;

        private GameController _game;

        private void Awake()
        {
            var config = JsonUtility.FromJson<GameConfigData>(_config.text);
            PlayerView playerView = Instantiate(_playerPrefab);
            _game = new GameController(config, new SpaceField(Camera.main), playerView, _uiSwitcher)
                   .SetupAsteroids(Instantiate(_asteroidBigPrefab), Instantiate(_asteroidSmallPrefab))
                   .SetupBullets(Instantiate(_bulletPrefab), playerView.BulletPivot)
                   .SetupUfo(Instantiate(_ufoPrefab), playerView.transform);

            _game.Pause();
        }

        private void Update()
        {
            _game.UpdateInput(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _game.Simulate(Time.fixedDeltaTime);
        }

        private void OnDestroy()
        {
            _game.Dispose();
        }
    }
}
