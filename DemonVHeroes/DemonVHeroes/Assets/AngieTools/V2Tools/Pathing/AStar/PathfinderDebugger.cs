using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AngieTools.V2Tools.Pathing.AStar
{
    public class PathfinderDebugger : MonoBehaviour
    {
        [SerializeField] private Vector3 m_offset;
        private PathFinder m_pathFinder;
        
        [Button("Toggle Grid")]
        public void ToggleGrid()
        {
            m_pathFinder.Grid.ToggleGrid();
        }
        
        // Start is called before the first frame update
        void Start()
        {
            m_pathFinder= new PathFinder(5,5, 2, m_offset );
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2Int pos = m_pathFinder.Grid.GetXy(WorldUtils.GetMousePositionWithZ());
                List<PathNode> path = m_pathFinder.FindPath(Vector2Int.zero, pos);

                if (path == null) return;
                
                for (var i = 0; i < path.Count - 1; i++)
                {
                    path[i].SetSelected(true);
                    m_pathFinder.Grid.RefreshValue(path[i].Position.x, path[i].Position.y);
                }
            }
        }
    }
}
