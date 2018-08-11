using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericCollections
{
    /// <summary>
    /// For working width sets
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.ISet{T}" />
    public class Set<T> : ISet<T>
    {
        #region Fields
        private BinarySearchTree.BinarySearchTree<T> set;
        #endregion
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class.
        /// </summary>
        public Set()
        {
            set = new BinarySearchTree.BinarySearchTree<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public Set(ICollection<T> collection) : base()
        {
            InputIEnumerableValidator(collection);

            set = new BinarySearchTree.BinarySearchTree<T>(collection);
        }
        #endregion
        #region puublic API        
        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public int Count { get { return set.Count; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds an element to the current set and returns a value to indicate if the element was successfully added.
        /// </summary>
        /// <param name="item">The element to add to the set.</param>
        /// <returns>
        ///   <see langword="true" /> if the element is added to the set; <see langword="false" /> if the element is already in the set.
        /// </returns>
        public bool Add(T item)
            => set.Add(item);

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
            => set = new BinarySearchTree.BinarySearchTree<T>();

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        ///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, <see langword="false" />.
        /// </returns>
        public bool Contains(T item)
            => set.Contains(item);

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="ArgumentOutOfRangeException">ArrayIndex must be more than 0</exception>
        /// <exception cref="ArgumentException">Array length must be more than set lenght</exception>
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

        /// <summary>
        /// Removes all elements in the specified collection from the current set.
        /// </summary>
        /// <param name="other">The collection of items to remove from the set.</param>
        public void ExceptWith(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            foreach (var item in other)
            {
                set.Remove(item);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in set.InOrder())
            {
                yield return item;
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
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

        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        ///   <see langword="true" /> if the current set is a proper subset of <paramref name="other" />; otherwise, <see langword="false" />.
        /// </returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            return IsSetOf(new Set<T>((ICollection<T>)other));
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        ///   <see langword="true" /> if the current set is a proper superset of <paramref name="other" />; otherwise, <see langword="false" />.
        /// </returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

             
            var otherSet = new Set<T>((ICollection<T>)other);

            return otherSet.IsSetOf(this);
        }

        /// <summary>
        /// Determines whether a set is a subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        ///   <see langword="true" /> if the current set is a subset of <paramref name="other" />; otherwise, <see langword="false" />.
        /// </returns>
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

        /// <summary>
        /// Determines whether the current set is a superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        ///   <see langword="true" /> if the current set is a superset of <paramref name="other" />; otherwise, <see langword="false" />.
        /// </returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            var otherSet = new BinarySearchTree.BinarySearchTree<T>(other);

            foreach (var item in otherSet.PreOrder())
            {
                if (!set.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        ///   <see langword="true" /> if the current set and <paramref name="other" /> share at least one common element; otherwise, <see langword="false" />.
        /// </returns>
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

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        ///   <see langword="true" /> if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(T item)
            => set.Remove(item);

        /// <summary>
        /// Determines whether the current set and the specified collection contain the same elements.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        ///   <see langword="true" /> if the current set is equal to <paramref name="other" />; otherwise, false.
        /// </returns>
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

        /// <summary>
        /// Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            foreach (var item in other)
            {
                if (!set.Remove(item))
                {
                    set.Add(item);
                } 
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            InputIEnumerableValidator(other);

            foreach (var item in other)
            {
                    set.Add(item);
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        void ICollection<T>.Add(T item)
            => Add(item);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Shows the set.
        /// </summary>
        /// <returns> Set </returns>
        public IEnumerable<T> ShowSet()
            => set.ShowTree();
        #endregion
        #region private methods
        private void InputIEnumerableValidator(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException($"Fill {nameof(other)}");
            }
        }

        private bool IsSetOf(Set<T> otherSet)
        {
            if (otherSet.Count == Count)
            {
                return false;
            }

            foreach (var item in set.InOrder())
            {
                if (!otherSet.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
