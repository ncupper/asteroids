using System.Collections.Generic;

using Asteroids.Game.Actors;
namespace Asteroids.Game
{
    public class ActiveActorsContainer
    {
        private readonly List<Actor> _items;
        private readonly List<Actor> _newItems;
        private readonly List<IDestroyable> _destroyedItems;

        public ActiveActorsContainer()
        {
            _items = new List<Actor>();
            _newItems = new List<Actor>();
            _destroyedItems = new List<IDestroyable>();
        }

        public void Add(Actor item)
        {
            item.Destroyed += OnItemDestroyed;
            _newItems.Add(item);
        }

        public IReadOnlyList<Actor> GetItems()
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
            foreach (IDestroyable item in _destroyedItems)
            {
                _items.Remove((Actor)item);
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
