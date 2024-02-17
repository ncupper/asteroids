using System.Collections.Generic;
namespace Asteroids.Game
{
    public class ItemsContainer<T> where T : IDestroyable
    {
        private readonly List<T> _items;
        private readonly List<T> _newItems;
        private readonly List<IDestroyable> _destroyedItems;

        public ItemsContainer()
        {
            _items = new List<T>();
            _newItems = new List<T>();
            _destroyedItems = new List<IDestroyable>();
        }

        public void Add(T item)
        {
            item.Destroyed += OnItemDestroyed;
            _newItems.Add(item);
        }

        public IReadOnlyList<T> GetItems()
        {
            ClearDestroyed();
            AddNew();
            return _items;
        }

        public void ClearAll()
        {
            AddNew();
            _items.ForEach(x => x.Destroyed -= OnItemDestroyed);
            _items.Clear();
            _destroyedItems.Clear();
        }

        private void ClearDestroyed()
        {
            foreach (T bullet in _destroyedItems)
            {
                _items.Remove(bullet);
            }
            _destroyedItems.Clear();
        }

        private void AddNew()
        {
            _items.AddRange(_newItems);
            _newItems.Clear();
        }

        private void OnItemDestroyed(IDestroyable item)
        {
            item.Destroyed -= OnItemDestroyed;
            _destroyedItems.Add(item);
        }

    }
}
