using Asteroids.Game;

using System;

using Asteroids.GUI;

using UnityEngine;
namespace Asteroids
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerPrefab;
        [SerializeField] private AsteroidView _asteroidBigPrefab;
        [SerializeField] private AsteroidView _asteroidSmallPrefab;
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private UfoView _ufoPrefab;
        [Header("GUI")]
        [SerializeField] private UISwitcher _uiSwitcher;

        private Game.Game _game;

        private void Awake()
        {
            PlayerView playerView = Instantiate(_playerPrefab);
            _game = new Game.Game(new SpaceField(Camera.main), playerView, _uiSwitcher)
                   .SetupAsteroids(Instantiate(_asteroidBigPrefab), _asteroidSmallPrefab)
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
