using System;
using System.Collections.Generic;

using Asteroids.Models;

using UnityEngine;
namespace Asteroids.Game.Actors.Bullet
{
    public class BulletSpawner
    {
        private readonly PlayerModel _model;
        private readonly IField _field;
        private readonly Transform _spawnPivot;
        private readonly ViewsPool<BulletView> _viewsPool;
        private readonly Dictionary<BulletView, Bullet> _bullets;

        private float _spawnTimer;

        public BulletSpawner(PlayerModel model, IField field, BulletView bulletSample, Transform spawnPivot)
        {
            _model = model;
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
            if (_spawnTimer.CompareTo(_model.BulletFireDelaySeconds) >= 0)
            {
                _spawnTimer -= _model.BulletFireDelaySeconds;
                Spawn();
            }
        }

        private void Spawn()
        {
            BulletView view = _viewsPool.Get();
            view.Self.position = _spawnPivot.position;

            Vector3 velocity = _spawnPivot.up * _model.BulletSpeed;
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
