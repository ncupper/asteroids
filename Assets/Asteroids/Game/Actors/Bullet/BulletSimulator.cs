using Asteroids.Game.Actors;

using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class BulletSimulator : ISimulator
    {
        private readonly BulletSpawner _spawner;

        public BulletSimulator(SpaceField field, BulletView bulletSample, Transform spawnPivot, ItemsContainer<Actor> container)
        {
            _spawner = new BulletSpawner(field, bulletSample, spawnPivot, container);
        }

        public void HideAll()
        {
            _spawner.HideAll();
        }

        public void Simulate(float deltaTime, IReadOnlyList<ICollideable> collideables)
        {
            _spawner.DestroyOutOfFieldBullets();
            _spawner.IncSpawnTimer(deltaTime);
        }

    }
}
