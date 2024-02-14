using UnityEngine;
namespace Asteroids.Game
{
    public class MovableView : MonoBehaviour, IMovableView
    {
        [field: SerializeField] public Collider2D Collider { get; private set; }

        private void Awake()
        {
            Self = transform;
        }

        public Transform Self { get; private set; }
    }
}
