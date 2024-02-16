using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class AsteroidSpawner
    {
        private const int BigSize = 120;
        private const int BigSpeed = 5;
        private const int SmallSize = 50;
        private const int SmallSpeed = 10;
        private const int Pieces = 5;

        private readonly ViewsPool<AsteroidView> _viewsPool;
        private readonly ItemsContainer<Asteroid> _asteroids;
        private readonly SpaceField _field;

        public AsteroidSpawner(SpaceField field, AsteroidView asteroidSample)
        {
            _field = field;

            _viewsPool = new ViewsPool<AsteroidView>(asteroidSample, 10);
            _asteroids = new ItemsContainer<Asteroid>();
        }

        public void HideAll()
        {
            _asteroids.ClearAll();
        }

        public IReadOnlyList<Asteroid> ActiveAsteroids => _asteroids.GetItems();

        public void StartupSpawn(int count)
        {
            _asteroids.ClearAll();

            for (var i = 0; i < count; ++i)
            {
                Vector3 pos = _field.GetRandomPositionForPerimeterArea();
                Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * BigSpeed;
                Spawn(pos, velocity, BigSize);
            }
        }

        public void SpawnPieces(Vector3 pivot)
        {
            for (var i = 0; i < Pieces; ++i)
            {
                Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * SmallSpeed;
                Spawn(pivot, velocity, SmallSize);
            }
        }

        private void Spawn(Vector3 position, Vector3 velocity, int size)
        {
            AsteroidView view = _viewsPool.Get();
            view.Self.position = position;
            view.Size = size;
            view.gameObject.SetActive(true);
            _asteroids.Add(new Asteroid(view, size, _field, velocity));
        }

    }
}
