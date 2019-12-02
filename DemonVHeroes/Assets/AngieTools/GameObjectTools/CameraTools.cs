using UnityEngine;

namespace AngieTools.GameObjectTools
{
    public class CameraTools : MonoBehaviour
    {
        public static Vector3 GetWorldPositionOnPlane(UnityEngine.Camera p_cam, Vector3 p_screenPosition, float z) {
            if (p_cam == null) return Vector3.zero;
            
            var ray = p_cam.ScreenPointToRay(p_screenPosition);
            var xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
            xy.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }
    }
}
