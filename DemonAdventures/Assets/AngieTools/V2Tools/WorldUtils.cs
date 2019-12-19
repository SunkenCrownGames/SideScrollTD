using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace AngieTools.V2Tools
{
    public static class WorldUtils
    {


        public static List<GameObject> GetAllObjectOnLayer(int p_layerId)
        {
            var allGameObjects = GameObject.FindObjectsOfType<GameObject>();

            return allGameObjects.Where(gameObject => gameObject.layer == p_layerId).ToList();
        }
        
        
        public static GameObject CreateWorldText(string p_text, Transform p_parent = null,
            Vector3 p_localPosition = default(Vector3), int p_fontSize = 45,  
            Color p_color = default(Color), TextAlignmentOptions p_alignment = TextAlignmentOptions.Center, int p_sortingOrder = 0)
        {
            var newObject = new GameObject("WorldText", typeof(TextMeshPro));
            var transform = newObject.transform;
            transform.SetParent(p_parent, false);
            transform.localPosition = p_localPosition;
            var tmpro = newObject.GetComponent<TextMeshPro>();
            tmpro.alignment = p_alignment;
            tmpro.text = p_text;
            tmpro.fontSize = p_fontSize;
            tmpro.color = p_color;
            tmpro.GetComponent<MeshRenderer>().sortingOrder = p_sortingOrder;

            return newObject;
        }
        

        #region  Viewport To World
        public static Vector3 ViewportToWorld(Vector2 p_position)
        {
            if (Camera.main != null) return Camera.main.ViewportToWorldPoint(p_position);

            return Vector3.zero;
        }
        
        public static Vector3 ViewportToWorld(float p_x, float p_y)
        {
            if (Camera.main == null) return Vector3.zero;
            
            var pos = Camera.main.ViewportToWorldPoint(new Vector3(p_x, p_y, Camera.main.transform.position.z * -1));
            return pos;

        }

        #endregion
        
        #region  Mouse Position In World
        
        public static Vector3 GetMousePositionWithZ()
        {
            if (Camera.main == null)
            {
                Debug.Log("No Camera Found");
                return Vector3.zero;
            }

            var pos =  new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1);
            return Camera.main.ScreenToWorldPoint(pos);
        }

        
        public static Vector3 GetMousePositionWithZ(Vector2 p_mousePosition)
        {
            if (UnityEngine.Camera.main == null)
            {
                Debug.LogWarning("No Camera Found");
                return Vector3.zero;
            }

            var pos =  new Vector3(p_mousePosition.x, p_mousePosition.y, 0);
            return Camera.main.ScreenToWorldPoint(pos);
        }
        
        public static Vector3 GetMousePositionWithZ(Vector2 p_mousePosition, Camera p_camera)
        {
            if (p_camera == null)
            {
                Debug.LogWarning("No Camera Found");
                return Vector3.zero;
            }

            var pos =  new Vector3(p_mousePosition.x, p_mousePosition.y, p_camera.transform.position.z);
            return p_camera.ScreenToWorldPoint(pos);
        }
        
        
        public static Vector3 GetMousePositionWithZ(Camera p_camera)
        {
            if (p_camera == null)
            {
                Debug.LogWarning("No Camera Found");
                return Vector3.zero;
            }

            Vector3 mousePosition = Input.mousePosition;

            var pos =  new Vector3(mousePosition.x, mousePosition.y, p_camera.transform.position.z);
            return p_camera.ScreenToWorldPoint(pos);
        }
        
        #endregion
    }
}