using Asteroids.Inputs;

using System;

using UnityEngine;

namespace Asteroids.Game
{
    public class PlayerView : MovableView
    {
        [field: SerializeField] public Transform BulletPivot { get; private set; }


    }
}
