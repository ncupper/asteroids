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
        [SerializeField] private MainGameView _mainGameView;

        private Game.Game _game;
        private MainGame _mainGame;

        private void Awake()
        {
            _game = new Game.Game(
                new SpaceField(Camera.main),
                Instantiate(_playerPrefab),
                Instantiate(_asteroidPrefab),
                Instantiate(_bulletPrefab),
                Instantiate(_ufoPrefab));

            _mainGame = new MainGame(_mainGameView, _game.Player.VelocityValue);

            _game.StartRound();
        }

        private void Update()
        {
            _game.Player.UpdateInput(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _game.Simulate(Time.fixedDeltaTime);
        }
    }
}
