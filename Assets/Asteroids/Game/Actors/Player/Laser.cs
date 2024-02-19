using Asteroids.Game.Actors;
using Asteroids.Inputs;
namespace Asteroids.Game
{
    public class Laser : Actor
    {
        private const float LaserActiveTimer = 1.0f;
        private const float LaserChargeTimer = 10.0f;
        private const int MaxLaserCharges = 3;

        private float _activeTimer;

        public Laser(MovableView view) : base(view)
        {
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
                if (_activeTimer.CompareTo(LaserActiveTimer) >= 0)
                {
                    Destroy();
                }
            }
            else if (gameInput.Gameplay.LaserFire.WasPerformedThisFrame() && ChargesCount.Value > 0)
            {
                ChargesCount.Value -= 1;
                if (ChargeTimer.Value.Equals(0))
                {
                    ChargeTimer.Value = LaserChargeTimer;
                }
                _activeTimer = 0;
                Spawn();
                container.Add(this);
            }
        }

        private void UpdateRestoreTimer(float deltaTime)
        {
            if (!ChargeTimer.Value.Equals(0))
            {
                ChargeTimer.Value -= deltaTime;
                if (ChargeTimer.Value.CompareTo(0) <= 0)
                {
                    ChargesCount.Value += 1;
                    if (MaxLaserCharges > ChargesCount.Value)
                    {
                        ChargeTimer.Value = LaserChargeTimer;
                    }
                    else
                    {
                        ChargeTimer.Value = 0;
                    }
                }
            }
        }
    }
}
