using UnityEngine;

namespace AngieTools.V2Tools
{
    public class VectorUtils : MonoBehaviour
    {
        public static readonly Vector3 DiagonalDownLeft = new Vector3(-1, -1, 0);
        public static readonly Vector3 DiagonalUpLeft = new Vector3(-1, 1, 0);
        
        public static readonly Vector3 DiagonalUpRight = new Vector3(1, 1, 0);
        public static readonly Vector3 DiagonalDownRight = new Vector3(1, -1, 0);
    }
}
