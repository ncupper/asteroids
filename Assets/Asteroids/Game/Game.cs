using Asteroids.Game.Actors;

using System;
using System.Threading.Tasks;

using Asteroids.GUI;
using Asteroids.Inputs;

using System.Collections.Generic;

using Asteroids.Game.Actors.Asteroid;
using Asteroids.Game.Actors.Bullet;

using UnityEngine;
namespace Asteroids.Game
{
    public class Game : IDisposable
    {
        private const int StartRoundDelay = 900;

        private readonly ActiveActorsContainer _activeActors;

        private readonly IField _field;
        private readonly UISwitcher _uiSwitcher;

        private AsteroidSimulator _asteroids;
        private BulletSimulator _bullets;
        private Ufo _ufo;

        private readonly Player _player;
        private readonly GameInput _gameInput;

        private readonly int _playerLayer;
        private int _obstacleLayer;

        private readonly IObservableVariable<int> _scores;
        private readonly IObservableVariable<int> _round;
        private bool _isPaused;

        public Game(IField field, PlayerView playerView, UISwitcher uiSwitcher)
        {
            _field = field;
            _uiSwitcher = uiSwitcher;

            _activeActors = new ActiveActorsContainer();

            _gameInput = new GameInput();
            _gameInput.Enable();

            _scores = new ObservableVariable<int>();
            _round = new ObservableVariable<int>();

            _player = new Player(playerView, field);
            _player.Destroyed += OnPlayerDestroyed;
            _playerLayer = _player.Layer;

            _uiSwitcher.Setup(_player.VelocityValue, _scores, _round);
            _uiSwitcher.StartClicked += StartGame;
        }

        public Game SetupAsteroids(AsteroidView asteroidBigView, AsteroidView asteroidSmallView)
        {
            _obstacleLayer = asteroidBigView.gameObject.layer;
            _asteroids = new AsteroidSimulator(_field, asteroidBigView, asteroidSmallView, _activeActors);
            return this;
        }

        public Game SetupBullets(BulletView bulletView, Transform pivot)
        {
            _bullets = new BulletSimulator(_field, bulletView, pivot, _activeActors);
            return this;
        }

        public Game SetupUfo(UfoView ufoView, Transform target)
        {
            _ufo = new Ufo(ufoView, _field, target);
            _obstacleLayer = _ufo.Layer;
            return this;
        }

        public void Dispose()
        {
            _uiSwitcher.StartClicked -= StartGame;
        }

        public void UpdateInput(float deltaTime)
        {
            _player.UpdateInput(_gameInput, deltaTime, _activeActors);
        }

        public void Simulate(float deltaTime)
        {
            if (!_isPaused)
            {
                IReadOnlyList<Actor> actors = _activeActors.GetItems();
                foreach (IMovable actor in actors)
                {
                    actor.Move(deltaTime);
                }

                _bullets.Simulate(deltaTime);

                var haveObstacles = false;
                foreach (ICollideable actor in actors)
                {
                    int contrLayer = actor.Layer == _playerLayer
                        ? _obstacleLayer
                        : _playerLayer;

                    ICollideable hit = actor.GetTouch(actors, contrLayer);
                    if (hit != null)
                    {
                        actor.Collide();
                        hit.Collide();
                    }
                    haveObstacles = haveObstacles || actor.Layer == _obstacleLayer;
                }

                if (_player.IsAlive && !haveObstacles)
                {
                    StartRound(_round.Value + 1);
                }
            }
        }

        private void OnPlayerDestroyed(IDestroyable _)
        {
            Pause();
        }

        public void Pause()
        {
            _isPaused = true;

            _player.Destroy();
            _bullets.HideAll();
            _asteroids.HideAll();
            _ufo.Destroy();

            _uiSwitcher.SwitchTo(UIMode.StartGame);
            _gameInput.Gameplay.Disable();
            _gameInput.UI.Enable();
        }

        private void StartGame()
        {
            _uiSwitcher.SwitchTo(UIMode.MainGame);
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();

            StartRound(1);
        }

        private async void StartRound(int round)
        {
            _activeActors.ClearAll();

            _round.Value = round;

            _player.Spawn();
            _activeActors.Add(_player);

            _bullets.HideAll();
            _asteroids.HideAll();

            _asteroids.Spawn(round + 5);

            _ufo.Spawn();
            _activeActors.Add(_ufo);

            //wait start round animation
            _isPaused = true;
            await Task.Delay(StartRoundDelay);
            _isPaused = false;
        }
    }
}
