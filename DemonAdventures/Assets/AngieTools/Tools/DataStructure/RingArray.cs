using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngieTools.DataStructures;
using System.Text;

namespace AngieTools.DataStructures
{
    public class RingArray<T>
    {
        //Array Of Data
        private T[] m_array = null;

        private bool m_fill = false;

        //Current Insert position of the ring array
        private int m_pos = 0;

        /// <summary>
        /// Ring Array Constructor
        /// </summary>
        /// <param name="p_count"> The size of the ring array</param>
        /// <param name="p_fill"> Weather to find holes and fill them</param>
        public RingArray(int p_count, bool p_fill = false)
        {
            m_array = new T[p_count];


            m_pos = 0;
            m_fill = p_fill;
        }
        /// <summary>
        /// Will Resize The Ring To p_size
        /// </summary>
        /// <param name="p_size">The new size of the ring array</param>
        public void ResizeRing(int p_size)
        {
            T[] newArray = new T[p_size];

            for (int i = 0; i < newArray.Length; i++)
            {
                if (i < m_array.Length)
                {
                    newArray[i] = m_array[i];
                }
                else
                {
                    break;
                }
            }

            m_array = newArray;
        }

        /// <summary>
        /// Will return what spaces in the array are occupied
        /// </summary>
        /// <returns></returns>
        public int OccupiedLength()
        {
            int count = 0;

            for (int i = 0; i < m_array.Length; i++)
            {
                if (m_array[i] != null)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Will Get The T At The Given Position
        /// </summary>
        /// <param name="p_pos"> Position To Find</param>
        /// <returns>The Entire T Object</returns>
        public T GetAt(int p_pos)
        {
            return m_array[p_pos];
        }

        /// <summary>
        /// Will Add a p_object T at p_position
        /// </summary>
        /// <param name="p_pos">The position to add at</param>
        /// <param name="p_object">The object to add</param>
        public void AddAt(int p_pos, T p_object)
        {
            if (p_pos < m_array.Length && p_pos >= 0)
            {
                m_array[p_pos] = p_object;
            }
            else
            {
                Debug.Log("Invalid Position");
            }
        }

        /// <summary>
        /// Will remove an item from the list at a given position
        /// </summary>
        /// <param name="p_pos">Item to delete from array</param>
        public void RemoveAt(int p_pos)
        {
            if (p_pos < m_array.Length && p_pos >= 0)
            {
                m_array[p_pos] = default(T);
            }
            else
            {
                Debug.Log("Invalid Position");
            }
        }

        /// <summary>
        /// Will Add p_object T to the RingArray
        /// Using m_pos as the insert position
        /// </summary>
        /// <param name="p_object">The object to add</param>
        public void Add(T p_object)
        {

            if (m_fill)
            {
                for (int i = 0; i < m_array.Length; i++)
                {
                    if (m_array[i] == null)
                    {
                        m_array[i] = p_object;
                        return;
                    }
                }
            }

            m_array[m_pos] = p_object;
            m_pos++;

            if (m_pos >= m_array.Length)
            {
                m_pos = 0;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < m_array.Length; i++)
            {
                if (m_array[i] != null)
                {
                    sb.AppendLine(m_array[i].ToString());
                }
                else
                {
                    sb.AppendLine("Element is null");
                }
            }

            return sb.ToString();
        }

        public int Length
        {
            get { return m_array.Length; }
        }
    }
}