using Asteroids.Models;

using UnityEngine;
namespace Asteroids.Game.Actors.Bullet
{
    public class BulletSimulator
    {
        private readonly BulletSpawner _spawner;

        public BulletSimulator(PlayerModel model, IField field,
            BulletView bulletSample, Transform spawnPivot, ActiveActorsContainer container)
        {
            _spawner = new BulletSpawner(model, field, bulletSample, spawnPivot);
            _spawner.Spawned += container.Add;
        }

        public void HideAll()
        {
            _spawner.HideAll();
        }

        public void Simulate(float deltaTime)
        {
            _spawner.IncSpawnTimer(deltaTime);
        }

    }
}
