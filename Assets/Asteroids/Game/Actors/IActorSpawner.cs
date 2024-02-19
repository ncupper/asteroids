using System;
namespace Asteroids.Game.Actors
{
    public interface IActorSpawner
    {
        event Action<Actor> Spawned;
    }
}
