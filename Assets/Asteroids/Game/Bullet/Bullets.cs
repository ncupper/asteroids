using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class Bullets
    {
        private const float BulletSpeed = 20.0f;
        private const float BulletSpawnDelaySeconds = 0.2f;

        private readonly ViewsPool<BulletView> _viewsPool;
        private readonly ItemsContainer<Bullet> _bullets;
        private readonly SpaceField _field;
        private readonly Transform _spawnPivot;

        private float _spawnTimer;

        public Bullets(SpaceField field, BulletView bulletSample, Transform spawnPivot)
        {
            _field = field;
            _spawnPivot = spawnPivot;
            _viewsPool = new ViewsPool<BulletView>(bulletSample, 10);
            _bullets = new ItemsContainer<Bullet>();
        }

        public IReadOnlyList<ICollideable> ActiveBullets => _bullets.GetItems();

        public void HideAll()
        {
            _bullets.ClearAll();
        }

        public void Update(float deltaTime)
        {
            IReadOnlyList<Bullet> items = _bullets.GetItems();

            foreach (Bullet bullet in items)
            {
                bullet.Move(deltaTime);
                if (_field.IsOut(bullet.Positon))
                {
                    bullet.Destroy();
                }
            }

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

            var bullet = new Bullet(view, _field, velocity);
            _bullets.Add(bullet);
        }

    }
}
