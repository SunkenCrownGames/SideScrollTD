using System;
using System.Collections;
using System.Collections.Generic;
using AngieTools.Tools.DataStructure;
using UnityEngine;


namespace AngieTools.DataStructures
{
    public class MergeSort
    {
        /// <summary>
        /// Start The Merge Sword And Split The Unordered List Into the Left and Right List
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="p_unsorted"> The Unsorted List</param>
        /// <returns>The Sorted List</returns>
        public static CustomList<T> MergeSortStart<T>(CustomList<T> p_unsorted) where T : IComparable
        {
            if (p_unsorted.Count <= 1)
                return p_unsorted;

            CustomList<T> left = new CustomList<T>();
            CustomList<T> right = new CustomList<T>();

            int middle = p_unsorted.Count / 2;
            for (int i = 0; i < middle; i++)  //Dividing the unsorted list
            {
                left.Add(p_unsorted[i]);
            }
            for (int i = middle; i < p_unsorted.Count; i++)
            {
                right.Add(p_unsorted[i]);
            }

            left = MergeSortStart<T>(left);
            right = MergeSortStart<T>(right);

            return Merge<T>(left, right);
        }

        /// <summary>
        /// Merge Recursive Function That will Merge Both Left and Right Lsits
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="p_left">Left List</param>
        /// <param name="p_right">Right List</param>
        /// <returns> The sub merged list</returns>
        private static CustomList<T> Merge<T>(CustomList<T> p_left, CustomList<T> p_right) where T : IComparable
        {
            CustomList<T> result = new CustomList<T>();

            while (p_left.Count > 0 || p_right.Count > 0)
            {
                if (p_left.Count > 0 && p_right.Count > 0)
                {
                    if (p_left[0].CompareTo(p_right[0]) == 1)  //Comparing First two elements to see which is bigger
                    {
                        result.Add(p_left[0]);
                        p_left.Remove(p_left[0]);      //Rest of the list minus the first element
                    }
                    else
                    {
                        result.Add(p_right[0]);
                        p_right.Remove(p_right[0]);
                    }
                }
                else if (p_left.Count > 0)
                {
                    result.Add(p_left[0]);
                    p_left.Remove(p_left[0]);
                }
                else if (p_right.Count > 0)
                {
                    result.Add(p_right[0]);

                    p_right.Remove(p_right[0]);
                }
            }
            return result;
        }
    }
}
