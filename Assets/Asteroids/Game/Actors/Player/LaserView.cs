using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Asteroids.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class LaserView : MovableView
    {
        public bool IsAlive => gameObject.activeSelf;
        public int Layer => gameObject.layer;

        public void Collide()
        {
        }

        public bool IsTouchWith(ICollideable collideable)
        {
            return Collider.Distance(collideable.Collider).isOverlapped;
        }

        public ICollideable GetTouch(IReadOnlyCollection<ICollideable> collideables, int layer)
        {
            return collideables.FirstOrDefault(x => Collider.Distance(x.Collider).isOverlapped);
        }
    }
}
