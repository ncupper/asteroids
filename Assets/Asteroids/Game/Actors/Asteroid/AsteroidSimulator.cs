using Asteroids.Models;
namespace Asteroids.Game.Actors.Asteroid
{
    public class AsteroidSimulator
    {
        private readonly AsteroidModel _model;
        private readonly AsteroidSpawner _bigSpawner;
        private readonly AsteroidSpawner _smallSpawner;

        public AsteroidSimulator(AsteroidModel model, IField field,
                                 AsteroidView asteroidBigSample, AsteroidView asteroidSmallSample,
                                 ActiveActorsContainer container)
        {
            _model = model;

            _bigSpawner = new AsteroidSpawner(_model, field, asteroidBigSample);
            _bigSpawner.Spawned += container.Add;
            _bigSpawner.Destroyed += OnBigAsteroidDestroyed;

            _smallSpawner = new AsteroidSpawner(_model, field, asteroidSmallSample);
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
            _smallSpawner.Spawn(asteroid.Positon, _model.CrushPieces);
        }
    }
}
