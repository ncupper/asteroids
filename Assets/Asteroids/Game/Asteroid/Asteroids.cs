using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class Asteroids
    {
        private const int BigSize = 120;
        private const int BigSpeed = 10;
        private const int SmallSize = 50;
        private const int SmallSpeed = 20;
        private const int Pieces = 5;

        private readonly ViewsPool<AsteroidView> _viewsPool;
        private readonly ItemsContainer<Asteroid> _asteroids;
        private readonly SpaceField _field;

        public Asteroids(SpaceField field, AsteroidView asteroidSample)
        {
            _field = field;
            _viewsPool = new ViewsPool<AsteroidView>(asteroidSample, 10);
            _asteroids = new ItemsContainer<Asteroid>();
        }

        private void Spawn(Vector3 position, Vector3 velocity, int size)
        {
            AsteroidView view = _viewsPool.Get();
            view.Self.position = position;
            view.Size = size;
            _asteroids.Add(new Asteroid(view, size, _field, velocity));
        }

        public void HideAll()
        {
            _asteroids.ClearAll();
        }

        public void StartupSpawn(int count)
        {
            _viewsPool.HideAll();

            for (var i = 0; i < count; ++i)
            {
                Vector3 pos = _field.GetRandomPositionForPerimeterArea();
                Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * BigSpeed;
                Spawn(pos, velocity, BigSize);
            }
        }

        public void Update(float deltaTime, IReadOnlyList<ICollideable> bullets)
        {
            IReadOnlyList<Asteroid> items = _asteroids.GetItems();
            foreach (Asteroid asteroid in items)
            {
                asteroid.Move(deltaTime);
                if (asteroid.IsAnyTouch(bullets))
                {
                    if (asteroid.Size == BigSize)
                    {
                        for (var i = 0; i < Pieces; ++i)
                        {
                            Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * SmallSpeed;
                            Spawn(asteroid.Positon, velocity, SmallSize);
                        }
                    }
                    asteroid.Destroy();
                }
            }
        }

    }
}
