using Asteroids.Game.Actors;
using Asteroids.Inputs;
using Asteroids.Models;
namespace Asteroids.Game
{
    public class Laser : Actor
    {
        private readonly PlayerModel _model;

        private float _activeTimer;

        public Laser(PlayerModel model, MovableView view) : base(view)
        {
            _model = model;
            ChargeTimer = new ObservableVariable<float>();
            ChargesCount = new ObservableVariable<int>();
        }

        public override void Move(float deltaTime)
        {
        }

        public ObservableVariable<float> ChargeTimer { get; }
        public ObservableVariable<int> ChargesCount { get; }

        public void UpdateInput(GameInput gameInput, float deltaTime, ActiveActorsContainer container)
        {
            UpdateRestoreTimer(deltaTime);

            if (IsAlive)
            {
                _activeTimer += deltaTime;
                if (_activeTimer.CompareTo(_model.LaserActiveDurationSeconds) >= 0)
                {
                    Destroy();
                }
            }
            else if (gameInput.Gameplay.LaserFire.WasPerformedThisFrame() && ChargesCount.Value > 0)
            {
                ChargesCount.Value -= 1;
                if (ChargeTimer.Value.Equals(0))
                {
                    ChargeTimer.Value = _model.LaserChargeDurationSeconds;
                }
                _activeTimer = 0;
                Spawn();
                container.Add(this);
            }
        }

        private void UpdateRestoreTimer(float deltaTime)
        {
            if (ChargesCount.Value < _model.MaxLaserCharges && !ChargeTimer.Value.Equals(0))
            {
                ChargeTimer.Value -= deltaTime;
                if (ChargeTimer.Value.CompareTo(0) <= 0)
                {
                    ChargesCount.Value += 1;
                    ChargeTimer.Value = ChargesCount.Value < _model.MaxLaserCharges
                        ? _model.LaserChargeDurationSeconds
                        : 0;
                }
            }
        }
    }
}
