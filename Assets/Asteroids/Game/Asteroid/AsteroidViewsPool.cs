namespace Asteroids.Game
{
    public class AsteroidViewsPool : ViewsPool<AsteroidView>
    {
        public AsteroidViewsPool(AsteroidView sample, int capacity) : base(sample, capacity) { }
    }
}
