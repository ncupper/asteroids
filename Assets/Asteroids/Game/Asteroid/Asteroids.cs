using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class Asteroids
    {
        private static readonly int[] SpawnQuadrants = { 0, 1, 2, 3, 4, 7, 8, 11, 12, 13, 14, 15 };

        private readonly AsteroidViewsPool _asteroidViewsPool;
        private readonly SpaceField _field;
        private readonly List<IMovable> _asteroids = new();

        public Asteroids(SpaceField field, AsteroidView asteroidView)
        {
            _field = field;
            _asteroidViewsPool = new AsteroidViewsPool(asteroidView, 10);
        }

        private void SpawnAsteroid(Vector3 position, Vector3 velocity, int size)
        {
            AsteroidView asteroid = _asteroidViewsPool.Get();
            asteroid.Self.position = position;
            _asteroids.Add(new Asteroid(asteroid, size, _field, velocity));
        }

        public void HideAll()
        {
            _asteroidViewsPool.HideAll();
            _asteroids.Clear();
        }

        public void StartupSpawn(int count)
        {
            _asteroidViewsPool.HideAll();

            for (var i = 0; i < count; ++i)
            {
                Vector3 pos = _field.GetRandomPosition(SpawnQuadrants[Random.Range(0, SpawnQuadrants.Length)]);
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
