using Asteroids.Game.Actors;

using System;
using System.Threading.Tasks;

using Asteroids.GUI;
using Asteroids.Inputs;

using System.Collections.Generic;

using Asteroids.Game.Actors.Asteroid;
using Asteroids.Game.Actors.Bullet;
using Asteroids.Models;

using UnityEngine;
namespace Asteroids.Game
{
    public class GameController : IDisposable
    {
        private readonly ActiveActorsContainer _activeActors;

        private readonly GameConfigData _config;
        private readonly IField _field;
        private readonly UISwitcher _uiSwitcher;

        private AsteroidSimulator _asteroids;
        private BulletSimulator _bullets;
        private Ufo _ufo;

        private readonly Player _player;
        private readonly GameInput _gameInput;

        private readonly int _playerLayer;
        private int _obstacleLayer;

        private readonly IObservableVariable<int> _round;
        private readonly IObservableVariable<int> _scores;
        private bool _isPaused;

        public GameController(GameConfigData config, IField field, PlayerView playerView, UISwitcher uiSwitcher)
        {
            _config = config;
            _field = field;
            _uiSwitcher = uiSwitcher;

            _activeActors = new ActiveActorsContainer();

            _gameInput = new GameInput();
            _gameInput.Enable();

            _round = new ObservableVariable<int>();
            _scores = new ObservableVariable<int>();

            _player = new Player(_config.Player, playerView, field);
            _player.Destroyed += OnPlayerDestroyed;
            _playerLayer = _player.Layer;

            _uiSwitcher.Setup(
                _player.PositionValue, _player.RotationValue, _player.VelocityValue,
                _player.LaserChargesCount, _player.LaserChargeTimer,
                _round, _scores);
            _uiSwitcher.StartClicked += StartGame;
        }

        public GameController SetupAsteroids(AsteroidView asteroidBigView, AsteroidView asteroidSmallView)
        {
            _obstacleLayer = asteroidBigView.gameObject.layer;
            _asteroids = new AsteroidSimulator(_config.Asteroid, _field, asteroidBigView, asteroidSmallView, _activeActors);
            return this;
        }

        public GameController SetupBullets(BulletView bulletView, Transform pivot)
        {
            _bullets = new BulletSimulator(_config.Player, _field, bulletView, pivot, _activeActors);
            return this;
        }

        public GameController SetupUfo(UfoView ufoView, Transform target)
        {
            _ufo = new Ufo(_config.Ufo, ufoView, _field, target);
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
                        _scores.Value += 1;
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
            _isPaused = true;

            _bullets.HideAll();
            _asteroids.HideAll();
            _ufo.Destroy();

            _uiSwitcher.SwitchTo(UIMode.GameOver);
            _gameInput.Gameplay.Disable();
            _gameInput.UI.Enable();
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

            _scores.Value = 0;

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

            _asteroids.Spawn(_config.FirstRoundAsteroidsCount + round - 1);

            _ufo.Spawn();
            _activeActors.Add(_ufo);

            //wait start round animation
            _isPaused = true;
            await Task.Delay(Mathf.RoundToInt(_config.StartRoundDelaySeconds * 1000));
            _isPaused = false;
        }
    }
}
