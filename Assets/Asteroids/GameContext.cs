using Asteroids.Game;

using System;

using Asteroids.GUI;

using UnityEngine;
namespace Asteroids
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerPrefab;
        [SerializeField] private AsteroidView _asteroidPrefab;
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private UfoView _ufoPrefab;
        [Header("GUI")]
        [SerializeField] private UISwitcher _uiSwitcher;

        private Game.Game _game;

        private void Awake()
        {
            _game = new Game.Game(
                new SpaceField(Camera.main),
                Instantiate(_playerPrefab),
                Instantiate(_asteroidPrefab),
                Instantiate(_bulletPrefab),
                Instantiate(_ufoPrefab),
                _uiSwitcher);
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
