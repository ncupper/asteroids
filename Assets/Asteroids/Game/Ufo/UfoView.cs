using UnityEngine;
namespace Asteroids.Game
{
    public class UfoView : MovableView
    {
        [field: SerializeField] public Collider2D Collider { get; private set; }
    }
}
