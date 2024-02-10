using UnityEngine;
namespace Asteroids.Game
{
    public class SpaceField
    {
        private readonly Camera _camera;

        public SpaceField(Camera camera)
        {
            _camera = camera;
        }

        public Vector3 CorrectPosition(Vector3 position)
        {
            Vector3 viewportPos = _camera.WorldToViewportPoint(position);

            if (viewportPos.x < 0) viewportPos.x += 1;
            else if (viewportPos.x > 1) viewportPos.x -= 1;

            if (viewportPos.y < 0) viewportPos.y += 1;
            else if (viewportPos.y > 1) viewportPos.y -= 1;

            return _camera.ViewportToWorldPoint(viewportPos);
        }
    }
}
