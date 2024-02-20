using System;
using System.Collections.Generic;

using Asteroids.Models;

using UnityEngine;

using Random = UnityEngine.Random;

namespace Asteroids.Game.Actors.Asteroid
{
    public class AsteroidSpawner
    {
        private readonly AsteroidModel _model;
        private readonly IField _field;
        private readonly ViewsPool<AsteroidView> _viewsPool;
        private readonly Dictionary<AsteroidView, Asteroid> _asteroids;

        public AsteroidSpawner(AsteroidModel model, IField field, AsteroidView sample)
        {
            _model = model;
            _field = field;

            _viewsPool = new ViewsPool<AsteroidView>(sample, 10);
            _asteroids = new Dictionary<AsteroidView, Asteroid>();
        }

        public event Action<Actor> Spawned;
        public event Action<Asteroid> Destroyed;

        public void HideAll()
        {
            _viewsPool.HideAll();
        }

        public void Spawn(int count)
        {
            for (var i = 0; i < count; ++i)
            {
                Vector3 pos = _field.GetRandomPositionForPerimeterArea();
                Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _model.BigSpeed;
                Spawn(pos, velocity);
            }
        }

        public void Spawn(Vector3 pivot, int count)
        {
            for (var i = 0; i < count; ++i)
            {
                Vector3 velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _model.SmallSpeed;
                Spawn(pivot, velocity);
            }
        }

        private void Spawn(Vector3 position, Vector3 velocity)
        {
            AsteroidView view = _viewsPool.Get();
            view.Self.position = position;
            view.gameObject.SetActive(true);

            if (!_asteroids.TryGetValue(view, out Asteroid asteroid))
            {
                asteroid = new Asteroid(view, _field);
                _asteroids.Add(view, asteroid);
                asteroid.Destroyed += OnAsteroidDestroyed;
            }

            asteroid.Velocity = velocity;
            asteroid.Spawn();
            Spawned?.Invoke(asteroid);
        }

        private void OnAsteroidDestroyed(IDestroyable asteroid)
        {
            Destroyed?.Invoke((Asteroid)asteroid);
        }
    }
}
