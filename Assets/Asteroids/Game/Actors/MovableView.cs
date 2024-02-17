using UnityEngine;
namespace Asteroids.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class MovableView : MonoBehaviour
    {
        private void Awake()
        {
            Self = transform;
            Collider = GetComponent<Collider2D>();
        }

        public Transform Self { get; private set; }
        public Collider2D Collider { get; private set; }
    }
}
