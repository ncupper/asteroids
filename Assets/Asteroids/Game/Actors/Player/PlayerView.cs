using UnityEngine;

namespace Asteroids.Game
{
    public class PlayerView : MovableView
    {
        [field: SerializeField] public Transform BulletPivot { get; private set; }
        [field: SerializeField] public LaserView Laser { get; private set; }

    }
}
