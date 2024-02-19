using System.Collections.Generic;

using Asteroids.Game.Actors;

using UnityEngine;
namespace Asteroids.Game
{
    public class BulletSimulator : ISimulator
    {
        private readonly BulletSpawner _spawner;

        public BulletSimulator(IField field, BulletView bulletSample, Transform spawnPivot, ActiveActorsContainer container)
        {
            _spawner = new BulletSpawner(field, bulletSample, spawnPivot);
            _spawner.Spawned += container.Add;
        }

        public void HideAll()
        {
            _spawner.HideAll();
        }

        public void Simulate(float deltaTime, IReadOnlyList<ICollideable> collideables)
        {
            _spawner.IncSpawnTimer(deltaTime);
        }

    }
}
