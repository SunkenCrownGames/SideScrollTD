using System;
using TMPro;
using UnityEngine;

namespace AngieTools.V2Tools
{
    public class CustomGridObject<T>
    {
        private readonly int m_width;
        private readonly int m_height;
        private readonly float m_cellSize;
        private readonly GameObject m_nodeParent;
        private readonly TextMeshPro[,] m_nodes;
        private readonly Vector3 m_originPosition;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private T[,] m_gridData;


        public CustomGridObject(int p_width, int p_height, float p_cellSize, Vector3 p_originPosition,bool p_debug, Func<CustomGridObject<T>, int, int, T> p_createGridObject)
        {
            m_width = p_width;
            m_height = p_height;
            m_cellSize = p_cellSize;
            m_gridData = new T[m_width, m_height];
            m_nodes = new TextMeshPro[m_width, m_height];
            m_originPosition = p_originPosition;

            m_nodeParent = new GameObject("Grid");
            
            for (var x = 0; x < m_gridData.GetLength(0); x++)
            {
                for (var y = 0; y < m_gridData.GetLength(1); y++)
                {
                    m_gridData[x,y] = p_createGridObject(this, x, y);
                    
                        var newObject = WorldUtils.CreateWorldText(m_gridData[x, y].ToString(), m_nodeParent.transform,
                            GetWorldPosition(x, y) + new Vector3(m_cellSize, m_cellSize) * 0.5f, 5, Color.white,
                            TextAlignmentOptions.Center, 0);
                        m_nodes[x, y] = newObject.GetComponent<TextMeshPro>();
                        
                        if (!p_debug) continue;
                    
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }

            if(!p_debug) return;
            Debug.DrawLine(GetWorldPosition(0, m_height), GetWorldPosition(m_width, m_height ), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(m_width, 0), GetWorldPosition(m_width, m_height ), Color.white, 100f);
        }
        
        private Vector3 GetWorldPosition(int p_x, int p_y)
        {
            return new Vector3(p_x, p_y) * m_cellSize + m_originPosition;
        }
        
        public Vector2Int GetXy(Vector3 p_worldPosition)
        {
            var xyPosition = new Vector2Int
            {
                x = Mathf.FloorToInt((p_worldPosition.x - m_originPosition.x) / m_cellSize),
                y = Mathf.FloorToInt((p_worldPosition.y - m_originPosition.y) / m_cellSize)
            };

            return xyPosition;
        }
        public void SetValue(int p_x, int p_y, T p_targetValue)
        {
            if (p_x < 0 || p_y < 0 || p_x >= m_width || p_y >= m_height) return;
            
            m_gridData[p_x, p_y] = p_targetValue;
            m_nodes[p_x, p_y].text = p_targetValue.ToString();
        }

        public void RefreshValue(int p_x, int p_y)
        {
            m_nodes[p_x, p_y].text = m_gridData[p_x, p_y].ToString();
        }
        
        public void SetValue(Vector3 p_worldPosition, T p_value)
        {
            Debug.Log(p_worldPosition.ToString());
            var pos = GetXy(p_worldPosition);
            SetValue(pos.x, pos.y, p_value);
        }

        public T GetValue(int p_x, int p_y)
        {
            if (p_x < 0 || p_y < 0 || p_x >= m_width || p_y >= m_height) return default(T);

            return m_gridData[p_x, p_y];
        }

        public T GetValue(Vector3 p_worldPosition)
        {
            var xy = GetXy(p_worldPosition);
            return GetValue(xy.x, xy.y);
        }

        public void ToggleGrid()
        {
            m_nodeParent.SetActive(!m_nodeParent.activeInHierarchy);
        }


        public int Width => m_width;

        public int Height => m_height;
    }
}
