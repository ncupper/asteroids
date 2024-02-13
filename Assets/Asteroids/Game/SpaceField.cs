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

        public bool IsOut(Vector3 position)
        {
            Vector3 viewportPos = _camera.WorldToViewportPoint(position);

            return viewportPos.x < 0
                || viewportPos.x > 1
                || viewportPos.y < 0
                || viewportPos.y > 1;
        }

        public Vector3 CorrectPosition(Vector3 position)
        {
            Vector3 viewportPos = _camera.WorldToViewportPoint(position);

            if (viewportPos.x < 0)
            {
                viewportPos.x += 1;
            }
            else if (viewportPos.x > 1)
            {
                viewportPos.x -= 1;
            }

            if (viewportPos.y < 0)
            {
                viewportPos.y += 1;
            }
            else if (viewportPos.y > 1)
            {
                viewportPos.y -= 1;
            }

            Vector3 newPosition = _camera.ViewportToWorldPoint(viewportPos);
            newPosition.z = position.z;
            return newPosition;
        }

        public Vector3 GetRandomPosition(int quadrant)
        {
            int x = quadrant % 4;
            int y = quadrant / 4;
            var randomViewportPos = new Vector3(Random.Range(0, 0.25f) + x * 0.25f, Random.Range(0, 0.25f) + y * 0.25f);

            Vector3 randomPosition = _camera.ViewportToWorldPoint(randomViewportPos);
            randomPosition.z = 0;
            return randomPosition;
        }
    }
}
