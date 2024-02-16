using System.Collections.Generic;
namespace Asteroids.Game
{
    public interface ISimulator
    {
        void Simulate(float deltaTime, IReadOnlyList<ICollideable> collideables);
    }
}
