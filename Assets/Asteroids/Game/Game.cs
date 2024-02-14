namespace Asteroids.Game
{
    public class Game
    {
        private readonly Asteroids _asteroids;
        private readonly Bullets _bullets;
        private readonly SpaceField _field;
        private Ufo _ufo;

        public Player Player { get; }

        public Game(SpaceField field, PlayerView playerView, AsteroidView asteroidView, BulletView bulletView, UfoView ufoView)
        {
            _field = field;
            Player = new Player(playerView, _field);
            _asteroids = new Asteroids(_field, asteroidView);
            _bullets = new Bullets(_field, bulletView, playerView.BulletPivot);
            _ufo = new Ufo(ufoView, _field, playerView.transform);
        }

        public void StartRound(int round = 0)
        {
            _asteroids.HideAll();
            _asteroids.StartupSpawn(round + 6);
        }

        public void Simulate(float deltaTime)
        {
            Player.Simulate(deltaTime);
            _bullets.Simulate(deltaTime);
            _asteroids.Simulate(deltaTime, _bullets.ActiveBullets);

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
