using Asteroids.Game.Actors;

using System;
using System.Threading.Tasks;

using Asteroids.GUI;
using Asteroids.Inputs;

using System.Collections.Generic;

using UnityEngine;
namespace Asteroids.Game
{
    public class Game : IDisposable
    {
        private const int StartRoundDelay = 900;

        private readonly ItemsContainer<Actor> _actors;

        private readonly SpaceField _field;
        private readonly UISwitcher _uiSwitcher;
        
        private AsteroidSimulator _asteroids;
        private BulletSimulator _bullets;
        private Ufo _ufo;
        
        private readonly Player _player;
        private readonly GameInput _gameInput;

        private readonly IObservableVariable<int> _scores;
        private readonly IObservableVariable<int> _round;
        private bool _isPaused;

        public Game(SpaceField field, PlayerView playerView, UISwitcher uiSwitcher)
        {
            _field = field;
            _uiSwitcher = uiSwitcher;

            _actors = new ItemsContainer<Actor>();

            _gameInput = new GameInput();
            _gameInput.Enable();

            _scores = new ObservableVariable<int>();
            _round = new ObservableVariable<int>();

            _player = new Player(playerView, field);
            _actors.Add(_player);

            _uiSwitcher.Setup(_player.VelocityValue, _scores, _round);
            _uiSwitcher.StartClicked += StartGame;
        }

        public Game SetupAsteroids(AsteroidView asteroidBigView, AsteroidView asteroidSmallView)
        {
            _asteroids = new AsteroidSimulator(_field, asteroidBigView, asteroidSmallView, _actors);
            return this;
        }

        public Game SetupBullets(BulletView bulletView, Transform pivot)
        {
            _bullets = new BulletSimulator(_field, bulletView, pivot, _actors);
            return this;
        }

        public Game SetupUfo(UfoView ufoView, Transform target)
        {
            _ufo = new Ufo(ufoView, _field, target);
            _actors.Add(_ufo);
            return this;
        }

        public void Dispose()
        {
            _uiSwitcher.StartClicked -= StartGame;
        }

        public void UpdateInput(float deltaTime)
        {
            _player.UpdateInput(_gameInput, deltaTime);
        }

        public void Simulate(float deltaTime)
        {
            if (!_isPaused)
            {
                IReadOnlyList<Actor> actors = _actors.GetItems();
                foreach (IMovable actor in actors)
                {
                    actor.Move(deltaTime);
                }
                
                _bullets.Simulate(deltaTime, null);
                _asteroids.Simulate(deltaTime, actors);
                
                ICollideable hit = _player.GetTouch(actors, 7);
                if (hit != null)
                {
                    GameOver();
                }
                else if (_ufo.IsAlive)
                {
                    ICollideable hitBullet = _ufo.GetTouch(actors, 6);
                    if (hitBullet != null)
                    {
                        hitBullet.Collide();
                        _ufo.Destroy();
                    }
                }
                /*else if (_asteroids.ActiveAsteroids.Count == 0)
                {
                    StartRound(_round.Value + 1);
                }*/
            }
        }

        private void GameOver()
        {
            _player.Destroy();
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
            _actors.ClearAll();

            _round.Value = round;

            _player.Spawn();

            _bullets.HideAll();
            _asteroids.HideAll();

            _asteroids.Spawn(round + 5);

            _ufo.Spawn();

            _isPaused = true;
            //wait start round animation
            await Task.Delay(StartRoundDelay);
            _isPaused = false;
        }
    }
}
