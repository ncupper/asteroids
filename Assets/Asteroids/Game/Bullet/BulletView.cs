using System;

using UnityEngine;
namespace Asteroids.Game
{
    public class BulletView : MovableView
    {
        [field: SerializeField] public Collider2D Collider { get; private set; }
    }
}
