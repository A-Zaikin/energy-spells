using System;
using System.Collections;
using System.Collections.Generic;

namespace WizardGame.Utility
{
    public class ObservableList<T> : IList<T>, IReadOnlyList<T>
    {
        private readonly IList<T> list = new List<T>();

        private Action<ObservableList<T>> observe;
        public event Action<ObservableList<T>> Observe
        {
            add
            {
                observe += value;
                value?.Invoke(this);
            }

            remove => observe -= value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }

        public void Add(T item)
        {
            list.Add(item);
            observe?.Invoke(this);
        }

        public void Clear()
        {
            list.Clear();
            observe?.Invoke(this);
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var removed = list.Remove(item);
            observe?.Invoke(this);
            return removed;
        }

        public int Count => list.Count;

        public bool IsReadOnly => list.IsReadOnly;

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
            observe?.Invoke(this);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
            observe?.Invoke(this);
        }

        public T this[int index]
        {
            get => list[index];
            set
            {
                list[index] = value;
                observe?.Invoke(this);
            }
        }
    }
}