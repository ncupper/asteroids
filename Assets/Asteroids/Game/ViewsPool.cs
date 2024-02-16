using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Asteroids.Game
{
    public class ViewsPool<T> where T : Component
    {
        private readonly List<T> _pool = new();

        public ViewsPool(T sample, int capacity)
        {
            _pool.Add(sample);

            for (var i = 1; i < capacity; ++i)
            {
                var view = Object.Instantiate(sample.gameObject).GetComponent<T>();
                _pool.Add(view);
            }

            HideAll();
        }

        public void HideAll()
        {
            foreach (T view in _pool)
            {
                view.gameObject.SetActive(false);
            }
        }

        public T Get()
        {
            foreach (T view in _pool)
            {
                if (!view.gameObject.activeSelf)
                {
                    return view;
                }
            }

            var newView = Object.Instantiate(_pool.First().gameObject).GetComponent<T>();
            _pool.Add(newView);
            return newView;
        }
    }
}
