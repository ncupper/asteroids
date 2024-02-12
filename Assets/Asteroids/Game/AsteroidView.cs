using UnityEngine;
namespace Asteroids.Game
{
    public class AsteroidView : MovableView
    {
        public int Size
        {
            get => Mathf.RoundToInt(Self.localScale.x * 100);
            set => Self.localScale = new Vector3(value / 100.0f, value / 100.0f, 1);
        }
    }
}
