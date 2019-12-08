using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Level
{
    [System.Serializable]
    public class PlatformPath
    {
        public PlatformPath(Platform p_node, Platform p_parentNode, PlatformPath p_parentPlatformPathNode, float p_distanceToDestination)
        {
            m_node = p_node;
            m_parentNode = p_parentNode;
            m_distanceToDestination = p_distanceToDestination;
            m_parentPlatformPathNode = p_parentPlatformPathNode;
        }

        [SerializeField] private Platform m_node;
        [SerializeField] private Platform m_parentNode;
        [SerializeField] private PlatformPath m_parentPlatformPathNode;
        [SerializeField] private float m_distanceToDestination;

        public Platform Node => m_node;

        public Platform ParentNode => m_parentNode;

        public PlatformPath ParentPlatformPathNode => m_parentPlatformPathNode;

        public float DistanceToDestination => m_distanceToDestination;
        
        

        public void UpdateDistance(float p_distance)
        {
            m_distanceToDestination = p_distance;
        }

        public void UpdateParent(Platform p_node)
        {
            m_parentNode = p_node;
        }

        public void UpdateParentNode(PlatformPath p_parentPlatformPathNode)
        {
            m_parentPlatformPathNode = p_parentPlatformPathNode;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string parentNode = (m_parentNode == null) ? "null" :  m_parentNode.name;
            
            sb.AppendLine("Node: " + m_node.name);
            sb.AppendLine("Node Parent: "  + parentNode);
            sb.AppendLine("Node Distance: " + m_distanceToDestination);
            
            return sb.ToString();
        }
    }
}
