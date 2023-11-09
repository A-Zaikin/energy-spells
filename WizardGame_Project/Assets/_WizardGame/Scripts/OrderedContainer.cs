using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WizardGame.Utility
{
    public class OrderedContainer<T> : IReadOnlyList<T> where T : class
    {
        private T[] array;

        public OrderedContainer(int capacity)
        {
            this.capacity = capacity;
            array = new T[capacity];
        }

        private Action<OrderedContainer<T>> observe;
        public event Action<OrderedContainer<T>> Observe
        {
            add
            {
                observe += value;
                value?.Invoke(this);
            }

            remove => observe -= value;
        }

        private int capacity;
        public int Capacity
        {
            get => capacity;
            set
            {
                capacity = value;

                var newArray = new T[capacity];
                for (int i = 0, count = array.Length; i < count && i < capacity; i++)
                    newArray[i] = array[i];

                observe?.Invoke(this);
            }
        }

        public int ItemCount => array.Count(item => item != null);

        public int Count => Capacity;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return array.GetEnumerator();
        }

        public bool CanAdd => ItemCount < Capacity;

        public bool Add(T item)
        {
            if (!CanAdd)
                return false;

            for (int i = 0, count = Capacity; i < count; i++)
            {
                if (array[i] == null)
                {
                    array[i] = item;
                    observe?.Invoke(this);
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            for (int i = 0, count = Capacity; i < count; i++)
                array[i] = null;

            observe?.Invoke(this);
        }

        public bool Contains(T item)
        {
            return array.Contains(item);
        }

        public bool Remove(T item)
        {
            for (int i = 0, count = Capacity; i < count; i++)
            {
                if (array[i] == item)
                {
                    array[i] = null;
                    observe?.Invoke(this);
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf(array, item);
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= Capacity || array[index] == null)
                return false;

            array[index] = null;
            observe?.Invoke(this);
            return true;
        }

        public bool EmptyAt(int index)
        {
            if (index < 0 || index >= Capacity)
                return false;

            return array[index] == null;
        }

        public T this[int index]
        {
            get => array[index];
            set
            {
                array[index] = value;
                observe?.Invoke(this);
            }
        }
    }
}