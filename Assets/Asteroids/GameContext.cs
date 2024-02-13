using Asteroids.Game;

using System;

using UnityEngine;
namespace Asteroids
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerPrefab;
        [SerializeField] private AsteroidView _asteroidPrefab;
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private UfoView _ufoPrefab;

        private Game.Game _game;

        private void Awake()
        {
            _game = new Game.Game(
                new SpaceField(Camera.main),
                Instantiate(_playerPrefab),
                Instantiate(_asteroidPrefab),
                Instantiate(_bulletPrefab),
                Instantiate(_ufoPrefab));

            _game.StartRound();
        }

        private void FixedUpdate()
        {
            _game.Update(Time.fixedDeltaTime);
        }
    }
}
