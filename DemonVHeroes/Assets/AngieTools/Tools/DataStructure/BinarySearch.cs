using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AngieTools.Tools.DataStructure;


namespace AngieTools.DataStructures
{
    public class BinarySearch
    {
        public static T StartBinarySearch<T>(CustomList<T> p_sortedList, T p_item) where T : IComparable
        {
            return FindItem<T>(p_sortedList, p_item);
        }

        public static T StartBinarySearchWithID<T>(CustomList<T> p_sortedList, int p_ID) where T : IComparable
        {
            return FindItem<T>(p_sortedList, p_ID);
        }

        private static T FindItem<T>(CustomList<T> p_sortedList, T p_item) where T : IComparable
        {
            CustomList<T> sortedList = new CustomList<T>(p_sortedList);
            int midPoint = sortedList.Count / 2;

            if (sortedList.Count > 1)
            {
                int comparisonResult = sortedList[midPoint].CompareTo(p_item);

                if (sortedList[midPoint].Equals(p_item))
                {
                    return sortedList[midPoint];
                }

                if (comparisonResult == -1)
                {
                    sortedList.SplitAndResize(0, midPoint);
                    return FindItem<T>(sortedList, p_item);
                }
                else
                {
                    sortedList.SplitAndResize(midPoint, sortedList.Count);
                    return FindItem<T>(sortedList, p_item);
                }
            }

            else
            {
                if (sortedList[0].Equals(p_item))
                    return sortedList[0];

                else
                {
                    Debug.Log("Item Could not be found");
                    return default(T);
                }
            }
        }

        private static T FindItem<T>(CustomList<T> p_sortedList, int p_ID) where T : IComparable
        {

            CustomList<T> sortedList = new CustomList<T>(p_sortedList);
            int midPoint = sortedList.Count / 2;
            IComparable newIDObject = p_ID;

            if (sortedList.Count > 1)
            {
                int comparisonResult = sortedList[midPoint].CompareTo(newIDObject);

                if (sortedList[midPoint].Equals(newIDObject))
                {
                    return sortedList[midPoint];
                }

                if (comparisonResult == -1)
                {
                    sortedList.SplitAndResize(0, midPoint);
                    return FindItem<T>(sortedList, p_ID);
                }
                else
                {
                    sortedList.SplitAndResize(midPoint, sortedList.Count);
                    return FindItem<T>(sortedList, p_ID);
                }
            }
            else
            {
                if (sortedList[0].Equals(newIDObject))
                    return sortedList[0];

                else
                {
                    Debug.Log("Item Could not be found");
                    return default(T);
                }
            }
        }
    }
}