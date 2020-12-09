using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace advent.Collections
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal class Deque<T> : ICollection<T>
    {
        private readonly LinkedList<T> list;
        
        public Deque()
        {
            list = new LinkedList<T>();
        }

        /// <inheritdoc />
        public int Count => list.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        /// <inheritdoc />
        public void Add(T item)
        {
            list.AddLast(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                list.AddLast(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            list.Clear();
        }

        /// <inheritdoc />
        public bool Contains(T item) => list.Contains(item);

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(T item) => list.Remove(item);

        public void AddFront(T item)
        {
            list.AddFirst(item);
        }

        public void AddBack(T item)
        {
            list.AddLast(item);
        }

        public T PeekFront() => list.First();
        public T PeekBack() => list.Last();

        public T PopFront()
        {
            var result = PeekFront();
            list.RemoveFirst();

            return result;
        }
        public T PopBack()
        {
            var result = PeekBack();
            list.RemoveLast();

            return result;
        }
    }
}