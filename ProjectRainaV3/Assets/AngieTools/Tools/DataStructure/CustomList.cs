using System.Collections.Generic;
using System.Text;

namespace AngieTools.Tools.DataStructure
{
    public class CustomList<T> : List<T>
    {

        private bool m_sorted;

        public CustomList()
        {

        }


        public CustomList(List<T> p_originalList)
        {
            foreach(T item in p_originalList)
            {
                Add(item);
            }

        }

        public CustomList(CustomList<T> p_originalList)
        {
            foreach (T item in p_originalList)
            {
                Add(item);
            }

        }

        /// <summary>
        /// Will split the list based on p_start and p_end and will reisze the list
        /// </summary>
        /// <param name="p_start">Where to start adding the data</param>
        /// <param name="p_end">where to stop adding the data</param>
        public void SplitAndResize(int p_start, int p_end)
        {
            if (p_start >= 0 && p_end <= Count && p_start <= p_end)
            {
                CustomList<T> tempList = new CustomList<T>(this);
                Clear();

                for (int i = p_start; i < p_end; i++)
                {
                    Add(tempList[i]);
                }
            }
        }

        public static CustomList<T> ToCustomList(List<T> p_originalList)
        {
            return new CustomList<T>(p_originalList);
        }


        public override string ToString()
        {
            CustomList<T> items = this;

            StringBuilder sb = new StringBuilder();

            foreach (T item in items)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
