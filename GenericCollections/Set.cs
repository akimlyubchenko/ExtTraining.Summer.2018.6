using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericCollections
{
    class Set<T> : ISet<T>
    {
        private BinarySearchTree.BinarySearchTree<T> set;

        public Set()
        {
            set = new BinarySearchTree.BinarySearchTree<T>();
        }

        public Set(ICollection<T> collection) : base()
        {
            InputIEnumerableValidator(collection);

            set = new BinarySearchTree.BinarySearchTree<T>(collection);
        }
        public int Count { get { return set.Count; } }

        public bool IsReadOnly => false;

        public bool Add(T item)
            => set.Add(item);

        public void Clear()
            => set = new BinarySearchTree.BinarySearchTree<T>();

        public bool Contains(T item)
            => set.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            InputIEnumerableValidator(array);

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("ArrayIndex must be more than 0");
            }

            if (array.Length < Count)
            {
                throw new ArgumentException("Array length must be more than set lenght");
            }

            IEnumerable<T> SetArray = set.InOrder();
            int i = 0;

            foreach (var item in SetArray)
            {
                array[arrayIndex + i] = item;
                i++;
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            foreach (var item in other)
            {
                set.Remove(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in set.InOrder())
            {
                yield return item;
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            var otherSet = new BinarySearchTree.BinarySearchTree<T>(other);
            var tempSet = new BinarySearchTree.BinarySearchTree<T>(set.PreOrder());
            set = new BinarySearchTree.BinarySearchTree<T>();

            foreach (var item in otherSet.PreOrder())
            {
                if (tempSet.Contains(item))
                {
                    set.Add(item);
                }
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            int counter = 0;
            foreach (var item in other)
            {
                counter++;
                if (!set.Contains(item))
                {
                    return false;
                }
            }

            if (counter == Count)
            {
                return false;
            }

            return true;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            var otherSet = new BinarySearchTree.BinarySearchTree<T>(other);
            if (otherSet.Count == set.Count)
            {
                return false;
            }

            foreach (var item in set.PreOrder())
            {
                if (!otherSet.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            foreach (var item in other)
            {
                if (!set.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            var otherSet = new BinarySearchTree.BinarySearchTree<T>(other);

            foreach (var item in set.PreOrder())
            {
                if (!otherSet.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            foreach (var item in other)
            {
                if (set.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(T item)
            => set.Remove(item);

        public bool SetEquals(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            int counter = 0;
            foreach (var item in other)
            {
                if (!set.Contains(item) || counter > set.Count)
                {
                    return false;
                }
            }

            if (counter != set.Count)
            {
                return false;
            }

            return false;
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            foreach (var item in other)
            {
                set.Remove(item);
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            foreach (var item in other)
            {
                if (!set.Contains(item))
                {
                    set.Add(item);
                }
            }
        }

        void ICollection<T>.Add(T item)
            => Add(item);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private void InputIEnumerableValidator(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException($"Fill {nameof(other)}");
            }
        }
    }
}
