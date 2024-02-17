using Asteroids.Game.Actors;

using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class BulletSpawner
    {
        private const float BulletSpeed = 30.0f;
        private const float BulletSpawnDelaySeconds = 0.15f;

        private readonly ViewsPool<BulletView> _viewsPool;
        private readonly SpaceField _field;
        private readonly Transform _spawnPivot;
        private readonly ItemsContainer<Actor> _container;
        private readonly Dictionary<BulletView, Bullet> _bullets;

        private float _spawnTimer;

        public BulletSpawner(SpaceField field, BulletView bulletSample, Transform spawnPivot, ItemsContainer<Actor> container)
        {
            _field = field;
            _spawnPivot = spawnPivot;
            _container = container;
            _viewsPool = new ViewsPool<BulletView>(bulletSample, 10);
            _bullets = new Dictionary<BulletView, Bullet>();
        }

        public void HideAll()
        {
            _viewsPool.HideAll();
        }

        public void DestroyOutOfFieldBullets()
        {
            foreach (Bullet bullet in _bullets.Values)
            {
                if (_field.IsOut(bullet.Positon))
                {
                    bullet.Destroy();
                }
            }
        }

        public void IncSpawnTimer(float deltaTime)
        {
            _spawnTimer += deltaTime;
            if (_spawnTimer.CompareTo(BulletSpawnDelaySeconds) >= 0)
            {
                _spawnTimer -= BulletSpawnDelaySeconds;
                Spawn();
            }
        }

        private void Spawn()
        {
            BulletView view = _viewsPool.Get();
            view.Self.position = _spawnPivot.position;
            view.gameObject.SetActive(true);

            Vector3 velocity = _spawnPivot.up * BulletSpeed;
            if (!_bullets.TryGetValue(view, out Bullet bullet))
            {
                bullet = new Bullet(view, velocity);
                _bullets.Add(view, bullet);
            }
            _container.Add(bullet);
        }

    }
}
