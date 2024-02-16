using System;
using System.Threading.Tasks;

using Asteroids.GUI;
using Asteroids.Inputs;

using UnityEditor;

using UnityEngine;
namespace Asteroids.Game
{
    public class Game : IDisposable
    {
        private const int StartRoundDelay = 900;

        private readonly UISwitcher _uiSwitcher;
        private readonly AsteroidSimulator _asteroids;
        private readonly BulletSimulator _bullets;
        private Ufo _ufo;
        private readonly Player _player;
        private readonly GameInput _gameInput;

        private readonly IObservableVariable<int> _scores;
        private readonly IObservableVariable<int> _round;
        private bool _isPaused;

        public Game(SpaceField field,
            PlayerView playerView, AsteroidView asteroidView, BulletView bulletView, UfoView ufoView,
            UISwitcher uiSwitcher)
        {
            _uiSwitcher = uiSwitcher;

            _gameInput = new GameInput();
            _gameInput.Enable();

            _scores = new ObservableVariable<int>();
            _round = new ObservableVariable<int>();

            _player = new Player(playerView, field);
            _asteroids = new AsteroidSimulator(field, asteroidView);
            _bullets = new BulletSimulator(field, bulletView, playerView.BulletPivot);
            _ufo = new Ufo(ufoView, field, playerView.transform);

            _uiSwitcher.Setup(_player.VelocityValue, _scores, _round);
            _uiSwitcher.StartClicked += StartGame;

            Pause();
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
                _player.Move(deltaTime);

                _bullets.Simulate(deltaTime, null);
                _asteroids.Simulate(deltaTime, _bullets.ActiveBullets);

                ICollideable hit = _player.GetTouch(_asteroids.ActiveAsteroids, 0);
                if (hit != null)
                {
                    ColliderDistance2D dist = _player.Collider.Distance(hit.Collider);
                    GameOver();
                }
                else if (_ufo.IsAlive)
                {
                    _ufo.Move(deltaTime);
                    ICollideable hitBullet = _ufo.GetTouch(_bullets.ActiveBullets, 0);
                    if (hitBullet != null)
                    {
                        hitBullet.Collide();
                        _ufo.Destroy();
                    }
                    else if (_player.IsTouchWith(_ufo))
                    {
                        GameOver();
                    }
                }
                else if (_asteroids.ActiveAsteroids.Count == 0)
                {
                    StartRound(_round.Value + 1);
                }
            }
        }

        private void GameOver()
        {
            _player.Destroy();
            Pause();
        }

        private void Pause()
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
            _round.Value = round;

            _player.Spawn();

            _bullets.HideAll();

            _asteroids.StartupSpawn(round + 5);

            _ufo.Spawn();

            _isPaused = true;
            //wait start round animation
            await Task.Delay(StartRoundDelay);
            _isPaused = false;
        }
    }
}
