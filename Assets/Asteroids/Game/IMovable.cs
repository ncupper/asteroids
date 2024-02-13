using UnityEngine;
namespace Asteroids.Game
{
    public interface IMovable
    {
        void Move(float deltaTime);
    }
}
