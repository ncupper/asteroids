using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Asteroids.Game
{
    public class AsteroidsPool
    {
        private readonly List<AsteroidView> _pool = new();

        public AsteroidsPool(AsteroidView sample, int capacity)
        {
            _pool.Add(sample);

            for (var i = 1; i < capacity; ++i)
            {
                var view = Object.Instantiate(sample.gameObject).GetComponent<AsteroidView>();
                _pool.Add(view);
            }

            HideAll();
        }

        public void HideAll()
        {
            foreach (AsteroidView view in _pool)
            {
                view.gameObject.SetActive(false);
            }
        }

        public AsteroidView Get()
        {
            foreach (AsteroidView view in _pool)
            {
                if (!view.gameObject.activeSelf)
                {
                    view.gameObject.SetActive(true);
                    return view;
                }
            }

            var newVview = Object.Instantiate(_pool.First().gameObject).GetComponent<AsteroidView>();
            _pool.Add(newVview);
            return newVview;
        }
    }
}
