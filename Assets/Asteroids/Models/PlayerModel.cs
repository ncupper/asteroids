using System;
namespace Asteroids.Models
{
    [Serializable]
    public class PlayerModel
    {
        public float Braking;
        public float Acceleration;
        public float MaxVelocity;

        public float BulletSpeed;
        public float BulletFireDelaySeconds;

        public int MaxLaserCharges;
        public float LaserActiveDurationSeconds;
        public float LaserChargeDurationSeconds;
    }
}
