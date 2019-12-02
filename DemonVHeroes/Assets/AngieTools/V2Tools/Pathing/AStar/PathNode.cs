using UnityEngine;

namespace AngieTools.V2Tools.Pathing.AStar
{
    public class PathNode
    {
        private bool m_selected;
        private Vector2Int m_position;

        public PathNode(CustomGridObject<PathNode> p_grid, Vector2Int p_position)
        {
            Grid = p_grid;
            m_position = p_position;
        }

        public void SetGCost(int p_val)
        {
            GCost = p_val;
        }
        
        public void SetHCost(int p_val)
        {
            HCost = p_val;
        }
        
        public void UpdateFCost()
        {
            FCost = GCost + HCost;
        }

        public void SetCameFromNode(PathNode p_node)
        {
            CameFromNode = p_node;
        }

        public CustomGridObject<PathNode> Grid { get; }

        public Vector2Int Position => m_position;

        public int GCost { get; private set; }

        public int HCost { get; private set; }

        public int FCost { get; private set; }

        public PathNode CameFromNode { get; private set; }


        public override string ToString()
        {
            if (m_selected) return m_selected.ToString();
            return $"{m_position.x.ToString()}";
        }

        public void SetSelected(bool p_b)
        {
            m_selected = p_b;
        }
    }
}
