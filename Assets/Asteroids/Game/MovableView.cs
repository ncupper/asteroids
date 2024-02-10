using UnityEngine;
namespace Asteroids.Game
{
    public class MovableView : MonoBehaviour, IMovable
    {
        private void Awake()
        {
            Self = transform;
        }

        public Transform Self { get; private set; }

        public void MoveTo(Vector3 to)
        {
            Self.position = to;
        }
    }
}
