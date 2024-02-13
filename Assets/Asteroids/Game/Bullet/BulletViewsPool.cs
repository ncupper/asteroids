namespace Asteroids.Game
{
    public class BulletViewsPool : ViewsPool<BulletView>
    {
        public BulletViewsPool(BulletView sample, int capacity) : base(sample, capacity) { }
    }
}
