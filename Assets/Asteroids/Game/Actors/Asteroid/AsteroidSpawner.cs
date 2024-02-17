using Asteroids.Game.Actors;

using System.Collections.Generic;

using UnityEngine;

namespace Asteroids.Game
{
    public class AsteroidSpawner
    {
        private const int BigSpeed = 5;
        private const int SmallSpeed = 10;

        private readonly ViewsPool<AsteroidView> _viewsPool;
        private readonly Dictionary<AsteroidView, Asteroid> _asteroids;
        private readonly SpaceField _field;
        private readonly ItemsContainer<Actor> _container;

        public AsteroidSpawner(SpaceField field, AsteroidView sample, ItemsContainer<Actor> container)
        {
            _field = field;
            _container = container;

            _viewsPool = new ViewsPool<AsteroidView>(sample, 10);
            _asteroids = new Dictionary<AsteroidView, Asteroid>();
        }

        public IReadOnlyCollection<Asteroid> Asteroids => _asteroids.Values;

        public void HideAll()
        {
            _viewsPool.HideAll();
        }

        public void Spawn(int count)
        {
            for (var i = 0; i < count; ++i)
            {
                Vector3 pos = _field.GetRandomPositionForPerimeterArea();
                Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * BigSpeed;
                Spawn(pos, velocity);
            }
        }

        public void Spawn(Vector3 pivot, int count)
        {
            for (var i = 0; i < count; ++i)
            {
                Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * SmallSpeed;
                Spawn(pivot, velocity);
            }
        }

        private void Spawn(Vector3 position, Vector3 velocity)
        {
            AsteroidView view = _viewsPool.Get();
            view.gameObject.SetActive(true);
            view.Self.position = position;

            if (!_asteroids.TryGetValue(view, out Asteroid asteroid))
            {
                asteroid = new Asteroid(view, _field, velocity);
                _asteroids.Add(view, asteroid);
            }
            else
            {
                //setup velocity
            }
            _container.Add(asteroid);
        }
    }
}
