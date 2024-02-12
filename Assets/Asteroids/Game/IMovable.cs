using UnityEngine;
namespace Asteroids.Game
{
    public interface IMovable
    {
        public Vector3 Velocity { get; set; }
        void Move(float deltaTime);
    }
}
