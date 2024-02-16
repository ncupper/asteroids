using UnityEngine;
namespace Asteroids.Game
{
    public interface IMovable
    {
        Vector3 Positon { get; }
        void Move(float deltaTime);
    }
}
