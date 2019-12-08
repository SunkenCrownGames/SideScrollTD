using UnityEngine;

namespace Level
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private Platform m_bottomNode;
        [SerializeField] private Platform m_topNode;

        public void UpdateNodes(Platform p_topNode, Platform p_bottomNode)
        {
            m_topNode = p_topNode;
            m_bottomNode = p_bottomNode;
        }
        
        public Platform BottomNode => m_bottomNode;

        public Platform TopNode => m_topNode;
    }
}
