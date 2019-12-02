using UnityEngine;

namespace AngieTools.Editor.Data.HierarchyData
{
    [System.Serializable]
    public class HierarchyObjectLink
    {

        public HierarchyObjectLink()
        {
            m_boxColor = new Color(0, 0, 0, 1);
            m_textColor = new Color(0, 0, 0, 1);
        }
        
        [SerializeField]
        private GameObject m_objectToColor = null;

        public GameObject ObjectToColor => m_objectToColor;

        public Color BoxColor => m_boxColor;

        public Color TextColor => m_textColor;

        [SerializeField] private Color m_boxColor;

        [SerializeField] private Color m_textColor;
    }
}
