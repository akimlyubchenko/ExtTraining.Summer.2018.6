using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GenericCollections.Tests
{
    [TestFixture]
    class SetTests
    {
        [Test]
        public void Set_Add()
        {
            Set<int> actual = new Set<int>(new int[] { 5 });
            int[] arr = new int[] { 3, 1, 7, 9 };
            for (int i = 0; i < arr.Length; i++)
            {
                actual.Add(arr[i]);
            }

            IEnumerable<int> expected = new int[] { 1, 3, 5, 7, 9 };

            CollectionAssert.AreEqual(expected, actual.ShowSet());
        }

        [Test]
        public void Set_Clear()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7 });

            IEnumerable<int> expected = new int[] { };
            actual.Clear();

            CollectionAssert.AreEqual(expected, actual.ShowSet());
        }

        [Test]
        public void Set_Contains()
        {
            Set<int> actual = new Set<int>(new int[] { 5 });
            int[] arr = new int[] { 3, 1, 7, 9 };
            for (int i = 0; i < arr.Length; i++)
            {
                actual.Add(arr[i]);
            }

            int expected = 9;

            Assert.True(actual.Contains(expected));
        }

        [Test]
        public void Set_CopyTo()
        {
            int arrind = 5;
            Set<int> actual = new Set<int>(new int[] { 5, 3, 1, 7, 9 });

            int[] actualArr = new int[12];
            actual.CopyTo(actualArr, arrind);

            CollectionAssert.AreEqual(new int[] { 0, 0, 0, 0, 0, 1, 3, 5, 7, 9, 0, 0 }, actualArr);
        }

        [Test]
        public void Set_GetEnumerator()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7, 8, 2, 9 });

            IEnumerable<int> other = new int[] { 2, 5, 6, 7, 8, 9 };

            CollectionAssert.AreEqual(other, actual.ShowSet());
        }

        [Test]
        public void Set_IntersectWith()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7, 8, 2, 9 });

            IEnumerable<int> other = new int[] { 2, 12, 6, 4, 0, 9 };
            actual.IntersectWith(other);

            CollectionAssert.AreEqual(new int[] { 2, 6, 9 }, actual.ShowSet());
        }

        [Test]
        public void Set_IsProperSubsetOf_True()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7 });

            IEnumerable<int> other = new int[] { 12, 5, 6, 7, 3, 4, 14 };

            Assert.True(actual.IsProperSubsetOf(other));
        }

        [Test]
        public void Set_IsProperSubsetOf_False()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7, 123 });

            IEnumerable<int> other = new int[] { 3, 5, 6, 7, 3, 4, 5, 3 };

            Assert.False(actual.IsProperSubsetOf(other));
        }

        [Test]
        public void Set_IsProperSupersetOf_True()
        {
            Set<int> actual = new Set<int>(new int[] { 12, 5, 6, 7, 3, 4, 13 });

            IEnumerable<int> other = new int[] { 5, 6, 7 };

            Assert.True(actual.IsProperSupersetOf(other));
        }

        [Test]
        public void Set_IsProperSupersetOf_False()
        {
            Set<int> actual = new Set<int>(new int[] { 12, 5, 6, 7, 3, 4, 13 });

            IEnumerable<int> other = new int[] { 5, 6, 7, 55 };

            Assert.False(actual.IsProperSubsetOf(other));
        }

        [Test]
        public void Set_IsSubsetOf_True()
        {
            Set<int> actual = new Set<int>(new int[] { 12, 5, 6, 7, 3, 4, 13 });

            IEnumerable<int> other = new int[] { 5, 6, 7 };

            Assert.True(actual.IsSubsetOf(other));
        }

        [Test]
        public void Set_IsSubsetOf_True_Equals()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7 });

            IEnumerable<int> other = new int[] { 5, 6, 7 };

            Assert.True(actual.IsSubsetOf(other));
        }

        [Test]
        public void Set_IsSubsetOf_False()
        {
            Set<int> actual = new Set<int>(new int[] { 12, 5, 6, 7, 3, 4, 13 });

            IEnumerable<int> other = new int[] { 5, 6, 7, 55 };

            Assert.False(actual.IsSubsetOf(other));
        }

        [Test]
        public void Set_IsSupersetOf_True()
        {
            Set<int> actual = new Set<int>(new int[] { 12, 5, 6, 7, 3, 4, 13 });

            IEnumerable<int> other = new int[] { 5, 6, 7 };

            Assert.True(actual.IsSupersetOf(other));
        }

        [Test]
        public void Set_IsSupersetOf_True_Equals()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7 });

            IEnumerable<int> other = new int[] { 5, 6, 7 };

            Assert.True(actual.IsSupersetOf(other));
        }

        [Test]
        public void Set_IsSupersetOf_False()
        {
            Set<int> actual = new Set<int>(new int[] { 12, 5, 6, 7, 3, 4, 13 });

            IEnumerable<int> other = new int[] { 5, 6, 7, 55 };

            Assert.False(actual.IsSupersetOf(other));
        }


        [Test]
        public void Set_OverLaps_True()
        {
            Set<int> actual = new Set<int>(new int[] { 12, 5, 6, 7, 3, 4, 13 });

            IEnumerable<int> other = new int[] { 1, 2, 67, 78, 13 };

            Assert.True(actual.Overlaps(other));
        }

        [Test]
        public void Set_OverLaps_Fasle()
        {
            Set<int> actual = new Set<int>(new int[] { 12, 5, 6, 7, 3, 4, 13 });

            IEnumerable<int> other = new int[] { 1, 2, 67, 78 };

            Assert.False(actual.IsSupersetOf(other));
        }

        [Test]
        public void Set_Remove()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7, 8, 2, 9 });
            actual.Remove(8);
            actual.Remove(9);
            actual.Remove(2);

            CollectionAssert.AreEqual(new int[] { 5, 6, 7 }, actual.ShowSet());
        }

        [Test]
        public void Set_SetEquals()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7, 8, 2, 9 });

            IEnumerable<int> other = new Set<int>(new int[] { 5, 6, 7, 8, 2, 9 });

            CollectionAssert.AreEqual(other, actual.ShowSet());
        }

        [Test]
        public void Set_SymmetricExceptWith()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7, 8, 2, 9 });

            IEnumerable<int> other = new Set<int>(new int[] { 5, 6, 14, 13 });
            actual.SymmetricExceptWith(other);

            CollectionAssert.AreEqual(new int[] { 2, 7, 8, 9, 13, 14 }, actual.ShowSet());
        }

        [Test]
        public void Set_UnionWith()
        {
            Set<int> actual = new Set<int>(new int[] { 5, 6, 7, 8, 2, 9 });

            IEnumerable<int> other = new Set<int>(new int[] { 5, 6, 14, 13 });
            actual.UnionWith(other);

            CollectionAssert.AreEqual(new int[] { 2, 5, 6, 7, 8, 9, 13, 14 }, actual.ShowSet());
        }
    }
}
