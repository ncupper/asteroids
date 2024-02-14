using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class BulletSpawner
    {
        private const float BulletSpeed = 30.0f;
        private const float BulletSpawnDelaySeconds = 0.2f;

        private readonly ViewsPool<BulletView> _viewsPool;
        private readonly ItemsContainer<Bullet> _bullets;
        private readonly SpaceField _field;
        private readonly Transform _spawnPivot;

        private float _spawnTimer;

        public BulletSpawner(SpaceField field, BulletView bulletSample, Transform spawnPivot)
        {
            _field = field;
            _spawnPivot = spawnPivot;
            _viewsPool = new ViewsPool<BulletView>(bulletSample, 10);
            _bullets = new ItemsContainer<Bullet>();
        }

        public IReadOnlyList<ICollideable> BulletColliders => _bullets.GetItems();
        public IReadOnlyList<IMovable> BulletMovers => _bullets.GetItems();

        public void HideAll()
        {
            _bullets.ClearAll();
        }

        public void DestroyOutOfFieldBullets()
        {
            IReadOnlyList<Bullet> items = _bullets.GetItems();

            foreach (Bullet bullet in items)
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
            Vector3 velocity = _spawnPivot.up * BulletSpeed;

            var bullet = new Bullet(view, velocity);
            _bullets.Add(bullet);
        }

    }
}
