using System.Collections.Generic;
using UnityEngine;

namespace AngieTools.V2Tools.Pathing.AStar
{
    public class PathFinder
    {
        private const int MoveStraightCost = 10;
        private const int MoveDiagonalCost = 14;
        
        private CustomGridObject<PathNode> m_grid;
        private List<PathNode> m_openedList;
        private List<PathNode> m_closedList;

        public PathFinder(int p_width, int p_height, float p_cellSize, Vector3 p_offset)
        {
            m_grid = new CustomGridObject<PathNode>(p_width, p_height, p_cellSize, p_offset, false, (
                CustomGridObject<PathNode> p_grid, int p_x, int p_y) => new PathNode(p_grid, new Vector2Int(p_x, p_y)));
        }

        public List<PathNode> FindPath(Vector2Int p_start, Vector2Int p_end)
        {
            var startNode = m_grid.GetValue(p_start.x, p_start.y);
            var endNode = m_grid.GetValue(p_end.x, p_end.y);
            
            m_openedList = new List<PathNode>() {startNode};
            m_closedList = new List<PathNode>();

            for (var x = 0; x < m_grid.Width; x++)
            {
                for (var y = 0; y < m_grid.Height; y++)
                {
                    PathNode pathNode = m_grid.GetValue(x, y);
                    pathNode.SetGCost(int.MaxValue);
                    pathNode.UpdateFCost();    
                    pathNode.SetCameFromNode(null);
                }
            }
            
            startNode.SetGCost(0);
            startNode.SetHCost(CalculateDistance(startNode, endNode));
            startNode.UpdateFCost();

            while (m_openedList.Count > 0)
            {
                var currentNode = GetLowestFCostNode(m_openedList);

                if (currentNode == endNode)
                {
                    return CalculatePath(endNode);
                }

                m_openedList.Remove(currentNode);
                m_closedList.Add(currentNode);

                foreach (var neighbourNode in GetNeighbourList(currentNode))
                {
                    if(m_closedList.Contains(neighbourNode)) continue;;


                    var tentativeGCost = currentNode.GCost + CalculateDistance(currentNode, neighbourNode);
                    if (tentativeGCost >= neighbourNode.GCost) continue;
                    
                    neighbourNode.SetCameFromNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(CalculateDistance(neighbourNode, endNode));
                    neighbourNode.UpdateFCost();

                    if (m_openedList.Contains(neighbourNode)) continue;
                    
                    m_openedList.Add(neighbourNode);
                }
            }
            
            //Out of nodes on the openList
            return null;
        }

        private List<PathNode> CalculatePath(PathNode p_endNode)
        {
            var path = new List<PathNode> {p_endNode};
            var currentNode = p_endNode;
            while (currentNode.CameFromNode != null)
            {
                path.Add(currentNode.CameFromNode);
                currentNode = currentNode.CameFromNode;
            }
            path.Reverse();

            return path;
        }

        private IEnumerable<PathNode> GetNeighbourList(PathNode p_currentNode)
        {
            var neighbourList = new List<PathNode>();
            
            if (p_currentNode.Position.x - 1 >= 0)
            {
                //LEFT
                neighbourList.Add(GetNode(p_currentNode.Position.x - 1, p_currentNode.Position.y ));
                //LEFT DOWN
                if(p_currentNode.Position.y  - 1 >= 0)  neighbourList.Add(GetNode(p_currentNode.Position.x - 1, p_currentNode.Position.y - 1 ));
                //LEFT UP
                if(p_currentNode.Position.y  + 1 < m_grid.Height)  neighbourList.Add(GetNode(p_currentNode.Position.x - 1, p_currentNode.Position.y + 1 ));
            }
            
            if (p_currentNode.Position.x + 1 < m_grid.Width)
            {
                //RIGHT
                neighbourList.Add(GetNode(p_currentNode.Position.x + 1, p_currentNode.Position.y ));
                //RIGHT DOWn
                if(p_currentNode.Position.y  - 1 >= 0)  neighbourList.Add(GetNode(p_currentNode.Position.x + 1, p_currentNode.Position.y - 1 ));
                //RIGHT UP
                if(p_currentNode.Position.y  + 1 < m_grid.Height)  neighbourList.Add(GetNode(p_currentNode.Position.x + 1, p_currentNode.Position.y + 1 ));
            }
            
            //DOWN
            if(p_currentNode.Position.y - 1 >= 0)  neighbourList.Add(GetNode(p_currentNode.Position.x, p_currentNode.Position.y - 1 ));
            //UP
            if(p_currentNode.Position.y + 1 < m_grid.Height)  neighbourList.Add(GetNode(p_currentNode.Position.x, p_currentNode.Position.y + 1 ));

            return neighbourList;
        }

        private PathNode GetNode(int p_x, int p_y)
        {
            return m_grid.GetValue(p_x, p_y);
        }

        private static int CalculateDistance(PathNode p_node1, PathNode p_node2)
        {
            var xDistance = Mathf.Abs(p_node1.Position.x - p_node2.Position.x);
            var yDistance = Mathf.Abs(p_node1.Position.y - p_node2.Position.y);
            var remaining = Mathf.Abs(xDistance - yDistance);

            return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
        }

        private static PathNode GetLowestFCostNode(IReadOnlyList<PathNode> p_nodeList)
        {
            var lowestFCostNode = p_nodeList[0];

            for (var i = 1; i < p_nodeList.Count; i++)
            {
                if (p_nodeList[i].FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = p_nodeList[i];
                }
            }

            return lowestFCostNode;
        }

        public CustomGridObject<PathNode> Grid => m_grid;
    }
}
