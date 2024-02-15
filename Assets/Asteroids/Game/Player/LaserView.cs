using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Asteroids.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class LaserView : MonoBehaviour, ICollideable
    {
        private void Awake()
        {
            Collider = GetComponent<Collider2D>();
        }

        public bool IsAlive => gameObject.activeSelf;
        public Collider2D Collider { get; private set; }

        public void Collide()
        {
        }

        public bool IsTouchWith(ICollideable collideable)
        {
            return Collider.Distance(collideable.Collider).isOverlapped;
        }

        public ICollideable GetTouch(IReadOnlyCollection<ICollideable> collideables)
        {
            return collideables.FirstOrDefault(x => Collider.Distance(x.Collider).isOverlapped);
        }
    }
}
