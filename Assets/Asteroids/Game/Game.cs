using UnityEngine;
namespace Asteroids.Game
{
    public class Game
    {
        private readonly Asteroids _asteroids;
        private readonly Bullets _bullets;
        private readonly SpaceField _field;

        public Game(SpaceField field, PlayerView playerView, AsteroidView asteroidView, BulletView bulletView)
        {
            _field = field;
            _asteroids = new Asteroids(_field, asteroidView);
            _bullets = new Bullets(_field, bulletView, playerView.BulletPivot);
        }

        public void StartRound(int round = 0)
        {
            _asteroids.HideAll();
            _asteroids.StartupSpawn(round + 6);
        }

        public void Update(float deltaTime)
        {
            _asteroids.Update(deltaTime, _bullets.ActiveBullets);
            _bullets.Update(deltaTime);
        }
    }
}
