using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AngieTools.Editor.Data.HierarchyData
{
    [CreateAssetMenu(fileName = "HierarchyItemDatabase", menuName = "DataObjects/Hierarchy/Hierarchy Item Database", order = 2)]
    public class HierarchyDatabase : ScriptableObject
    {

        public HierarchyDatabase()
        {
            m_data = new List<HierarchyObjectLink>();
        }
        
        [FormerlySerializedAs("m_hierarchyData")] [SerializeField]
        private List<HierarchyObjectLink> m_data = null;

        public List<HierarchyObjectLink> Data => m_data;
    }
}