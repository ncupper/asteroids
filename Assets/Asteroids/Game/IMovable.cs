using UnityEngine;
namespace Asteroids.Game
{
    public interface IMovable
    {
        Transform Self { get; }

        void MoveTo(Vector3 to);
    }
}
