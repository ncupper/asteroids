namespace Asteroids.Game.Actors.Asteroid
{
    public class AsteroidSimulator
    {
        private const int Pieces = 5;

        private readonly AsteroidSpawner _bigSpawner;
        private readonly AsteroidSpawner _smallSpawner;

        public AsteroidSimulator(IField field,
                                 AsteroidView asteroidBigSample, AsteroidView asteroidSmallSample,
                                 ActiveActorsContainer container)
        {
            _bigSpawner = new AsteroidSpawner(field, asteroidBigSample);
            _bigSpawner.Spawned += container.Add;
            _bigSpawner.Destroyed += OnBigAsteroidDestroyed;

            _smallSpawner = new AsteroidSpawner(field, asteroidSmallSample);
            _smallSpawner.Spawned += container.Add;
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

        private void OnBigAsteroidDestroyed(Asteroid asteroid)
        {
            _smallSpawner.Spawn(asteroid.Positon, Pieces);
        }
    }
}
