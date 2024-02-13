using UnityEngine;
namespace Asteroids.Game
{
    public class Bullet : IMovable
    {
        private readonly IMovableView _view;
        private readonly SpaceField _field;
        private readonly Vector3 _velocity;

        public Bullet(IMovableView view, SpaceField field, Vector3 velocity)
        {
            _view = view;
            _field = field;
            _velocity = velocity;
        }

        public void Move(float deltaTime)
        {
            Vector3 pos = _view.Self.position + deltaTime * _velocity;
            _view.Self.position = pos;
            if (_field.IsOut(pos))
            {
                _view.Self.gameObject.SetActive(false);
            }
        }
    }
}
