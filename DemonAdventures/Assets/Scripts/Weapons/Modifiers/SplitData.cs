using System.Text;
using UnityEngine;

namespace Weapons.Modifiers
{
    [System.Serializable]
    public class SplitData : Modifier
    {
        [SerializeField] private int m_splitCount;
        [SerializeField] private float m_angle;

        public int SplitCount => m_splitCount;

        public float Angle => m_angle;
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Split Count: {m_splitCount}");
            sb.AppendLine($"Split Angle: {m_angle}");

            return sb.ToString();
        }
    }
}
