using UnityEngine;
namespace Asteroids.Game.Actors
{
    public interface IField
    {
        bool IsOut(Vector3 position);

        Vector3 CorrectPosition(Vector3 position);

        Vector3 GetRandomPositionForPerimeterArea();
    }
}
