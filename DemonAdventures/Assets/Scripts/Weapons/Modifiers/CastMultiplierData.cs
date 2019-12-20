using System.Text;
using UnityEngine;

namespace Weapons.Modifiers
{
    [System.Serializable]
    public class CastMultiplierData : Modifier
    {
        [SerializeField] private int m_castCount;
        [SerializeField] private float m_castDelay;

        public int CastCount => m_castCount;
        public float CastDelay => m_castDelay;
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Cast Count Count: {m_castCount}");
            sb.AppendLine($"Cast Delay: {m_castDelay}");

            return sb.ToString();
        }
    }
}
