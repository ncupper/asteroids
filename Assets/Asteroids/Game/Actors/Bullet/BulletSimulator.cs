using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class BulletSimulator : ISimulator
    {
        private readonly BulletSpawner _spawner;

        public BulletSimulator(SpaceField field, BulletView bulletSample, Transform spawnPivot)
        {
            _spawner = new BulletSpawner(field, bulletSample, spawnPivot);
        }

        public IReadOnlyList<ICollideable> ActiveBullets => _spawner.BulletColliders;

        public void HideAll()
        {
            _spawner.HideAll();
        }

        public void Simulate(float deltaTime, IReadOnlyList<ICollideable> collideables)
        {
            IReadOnlyList<IMovable> items = _spawner.BulletMovers;

            foreach (IMovable bullet in items)
            {
                bullet.Move(deltaTime);
            }

            _spawner.DestroyOutOfFieldBullets();

            _spawner.IncSpawnTimer(deltaTime);
        }

    }
}
