using System;
using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game.Actors.Bullet
{
    public class BulletSpawner
    {
        private const float BulletSpeed = 30.0f;
        private const float BulletSpawnDelaySeconds = 0.15f;

        private readonly ViewsPool<BulletView> _viewsPool;
        private readonly Dictionary<BulletView, Bullet> _bullets;
        private readonly IField _field;
        private readonly Transform _spawnPivot;

        private float _spawnTimer;

        public BulletSpawner(IField field, BulletView bulletSample, Transform spawnPivot)
        {
            _field = field;
            _spawnPivot = spawnPivot;
            _viewsPool = new ViewsPool<BulletView>(bulletSample, 10);
            _bullets = new Dictionary<BulletView, Bullet>();
        }

        public event Action<Actor> Spawned;

        public void HideAll()
        {
            _viewsPool.HideAll();
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
            if (!_bullets.TryGetValue(view, out Bullet bullet))
            {
                bullet = new Bullet(view, _field);
                _bullets.Add(view, bullet);
            }

            bullet.Velocity = velocity;
            bullet.Spawn();
            Spawned?.Invoke(bullet);
        }

    }
}
