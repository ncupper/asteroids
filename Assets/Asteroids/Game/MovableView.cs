using UnityEngine;
namespace Asteroids.Game
{
    public class MovableView : MonoBehaviour, IMovableView
    {
        private void Awake()
        {
            Self = transform;
        }

        public Transform Self { get; private set; }
    }
}
