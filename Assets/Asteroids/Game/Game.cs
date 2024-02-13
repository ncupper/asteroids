namespace Asteroids.Game
{
    public class Game
    {
        private readonly Asteroids _asteroids;
        private readonly Bullets _bullets;
        private readonly SpaceField _field;
        private Ufo _ufo;

        public Game(SpaceField field, PlayerView playerView, AsteroidView asteroidView, BulletView bulletView, UfoView ufoView)
        {
            _field = field;
            _asteroids = new Asteroids(_field, asteroidView);
            _bullets = new Bullets(_field, bulletView, playerView.BulletPivot);
            _ufo = new Ufo(ufoView, _field, playerView.transform);
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

            if (_ufo != null)
            {
                _ufo.Move(deltaTime);
                if (_ufo.IsAnyTouch(_bullets.ActiveBullets))
                {
                    _ufo.Destroy();
                    _ufo = null;
                }
            }
        }
    }
}
