using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


namespace AngieTools.DataStructures
{
    public class ArrayTools
    {
        public static T[] ReverseArray<T>(T[] p_initialArray)
        {
            T[] reverseArray = new T[p_initialArray.Length];

            for (int i = p_initialArray.Length - 1; i >= 0; i--)
            {
                reverseArray[i] = p_initialArray[i];
            }

            return reverseArray;
        }

        /// <summary>
        /// Will reverse the array make sure p_startID is Smaller Than p_endID
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="p_initialArray"> The initial array to swap</param>
        /// <param name="p_startID">The starting element from the array could be end Or start of the actual array</param>
        /// <param name="p_endID">The ending element from the array could be end Or start of the actual array</param>
        /// <returns>Returns The Swapped Array</returns>
        public static T[] ReverseArray<T>(T[] p_initialArray, int p_startID, int p_endID)
        {
            if (p_startID < p_endID)
            {

                T[] reverseArray = new T[p_initialArray.Length];

                int count = 0;

                for (int i = p_endID; i >= p_startID; i--)
                {
                    reverseArray[p_startID + count] = p_initialArray[i];
                    count++;
                }

                return reverseArray;
            }
            else
            {
                Debug.Log("Could not Swap");
                return p_initialArray;
            }
        }

        public static string ArrayToString<T>(T[] p_array)
        {
            StringBuilder sb = new StringBuilder();

            foreach(T ele in p_array)
            {
                sb.AppendLine(ele.ToString());
            }

            return sb.ToString();
        }
    }
}
