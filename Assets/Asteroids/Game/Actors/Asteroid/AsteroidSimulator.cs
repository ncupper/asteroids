using Asteroids.Game.Actors;

using System.Collections.Generic;

namespace Asteroids.Game
{
    public class AsteroidSimulator : ISimulator
    {
        private const int PlayerLayer = 6;
        private const int Pieces = 5;

        private readonly AsteroidSpawner _bigSpawner;
        private readonly AsteroidSpawner _smallSpawner;

        public AsteroidSimulator(SpaceField field,
                                 AsteroidView asteroidBigSample, AsteroidView asteroidSmallSample,
                                 ItemsContainer<Actor> container)
        {
            _bigSpawner = new AsteroidSpawner(field, asteroidBigSample, container);
            _smallSpawner = new AsteroidSpawner(field, asteroidSmallSample, container);
        }

        public void HideAll()
        {
            _bigSpawner.HideAll();
            _smallSpawner.HideAll();
        }

        public void Spawn(int count)
        {
            _bigSpawner.Spawn(count);
        }

        public void Simulate(float deltaTime, IReadOnlyList<ICollideable> collideables)
        {
            IReadOnlyCollection<Asteroid> asteroids = _bigSpawner.Asteroids;
            foreach (Asteroid asteroid in asteroids)
            {
                ICollideable hit = asteroid.GetTouch(collideables, PlayerLayer);
                if (hit != null)
                {
                    hit.Collide();
                    asteroid.Destroy();
                    _smallSpawner.Spawn(asteroid.Positon, Pieces);
                }
            }

            asteroids = _smallSpawner.Asteroids;
            foreach (Asteroid asteroid in asteroids)
            {
                ICollideable hit = asteroid.GetTouch(collideables, PlayerLayer);
                if (hit != null)
                {
                    hit.Collide();
                    asteroid.Destroy();
                }
            }
        }

    }
}
