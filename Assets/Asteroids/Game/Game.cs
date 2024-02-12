using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class Game
    {
        private static readonly int[] SpawnQuadrants = { 0, 1, 2, 3, 4, 7, 8, 11, 12, 13, 14, 15 };

        private readonly AsteroidsPool _asteroidsPool;
        private readonly SpaceField _field;
        private readonly List<IMovable> _asteroids = new List<IMovable>();

        public Game(SpaceField field, PlayerView playerView, AsteroidView asteroidView)
        {
            _field = field;
            _asteroidsPool = new AsteroidsPool(asteroidView, 10);
        }

        private void SpawnAsteroid(Vector3 position, Vector3 velocity, int size)
        {
            AsteroidView asteroid = _asteroidsPool.Get();
            asteroid.Self.position = position;
            asteroid.Size = size;
            _asteroids.Add(new Asteroid(asteroid, size, _field, velocity));
        }

        public void StartRound(int round = 0)
        {
            _asteroidsPool.HideAll();

            for (var i = 0; i < 6 + round; ++i)
            {
                Vector3 pos = _field.GetRandomPosion(SpawnQuadrants[Random.Range(0, SpawnQuadrants.Length)]);
                Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 10;
                SpawnAsteroid(pos, velocity, 100);
            }
        }

        public void Update(float deltaTime)
        {
            foreach (IMovable asteroidView in _asteroids)
            {
                asteroidView.Move(deltaTime);
            }
        }
    }
}
