using TMPro;
using UnityEngine;

namespace AngieTools.V2Tools
{
    public class CustomGrid
    {
        private int m_width;
        private int m_height;
        private float m_cellSize;
        private int[,] m_gridArray;
        private TextMeshPro[,] m_labels;
        private Vector3 m_originPosition;

        public CustomGrid(int p_width, int p_height, float p_cellSize, Vector3 p_originPosition = default(Vector3))
        {
            m_width = p_width;
            m_height = p_height;
            m_cellSize = p_cellSize;
            m_gridArray = new int[p_width, p_height];
            m_labels = new TextMeshPro[p_width, p_height];
            m_originPosition = p_originPosition;
            

            for (var x = 0; x < m_gridArray.GetLength(0); x++)
                for(var y = 0; y < m_gridArray.GetLength(1); y++)
                {
                    GameObject newObject = WorldUtils.CreateWorldText(m_gridArray[x,y].ToString(), null, GetWorldPosition(x,y) + new Vector3(m_cellSize, m_cellSize) * 0.5f, 10, Color.white, TextAlignmentOptions.Center);
                    m_labels[x, y] = newObject.GetComponent<TextMeshPro>();
                    
                    Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x + 1, y ), Color.white, 100f);
                }
                
            Debug.DrawLine(GetWorldPosition(0, m_height), GetWorldPosition(m_width, m_height ), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(m_width, 0), GetWorldPosition(m_width, m_height ), Color.white, 100f);
        }

        private Vector3 GetWorldPosition(int p_x, int p_y)
        {
            return new Vector3(p_x, p_y) * m_cellSize + m_originPosition;
        }

        private Vector2Int GetXy(Vector3 p_worldPosition)
        {
            var xyPosition = new Vector2Int
            {
                x = Mathf.FloorToInt((p_worldPosition.x - m_originPosition.x) / m_cellSize),
                y = Mathf.FloorToInt((p_worldPosition.y - m_originPosition.y) / m_cellSize)
            };

            return xyPosition;
        }

        public void SetValue(int p_x, int p_y, int p_targetValue)
        {
            if (p_x < 0 || p_y < 0 || p_x >= m_width || p_y >= m_height) return;
            
            m_gridArray[p_x, p_y] = p_targetValue;
            m_labels[p_x, p_y].text = p_targetValue.ToString();
        }

        public void SetValue(Vector3 p_worldPosition, int p_value)
        {
            Debug.Log(p_worldPosition.ToString());
            Vector2Int pos = GetXy(p_worldPosition);
            SetValue(pos.x, pos.y, p_value);
        }

        public int GetValue(int p_x, int p_y)
        {
            if (p_x < 0 || p_y < 0 || p_x >= m_width || p_y >= m_height) return -1;

            return m_gridArray[p_x, p_y];
        }

        public int GetValue(Vector3 p_worldPosition)
        {
            Vector2Int xy = GetXy(p_worldPosition);
            return GetValue(xy.x, xy.y);
        }
    }
}
