using Asteroids.Game;

using System;

using UnityEngine;
namespace Asteroids
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerPrefab;
        [SerializeField] private AsteroidView _asteroidPrefab;

        private Game.Game _game;

        private void Awake()
        {
            _game = new Game.Game(new SpaceField(Camera.main), Instantiate(_playerPrefab), Instantiate(_asteroidPrefab));

            _game.StartRound();
        }

        private void FixedUpdate()
        {
            _game.Update(Time.fixedDeltaTime);
        }
    }
}
