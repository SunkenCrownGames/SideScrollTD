using System.Text;
using UnityEngine;

namespace Weapons.Modifiers
{
    [System.Serializable]
    public class ExplosionData : Modifier
    {
        [SerializeField] private float m_explosionRadius;
        [SerializeField] private float m_explosionStrength;

        public float ExplosionRadius => m_explosionRadius;

        public float ExplosionStrength => m_explosionStrength;
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Explosion Radius: {m_explosionRadius}");
            sb.AppendLine($"Explosion Strength: {m_explosionStrength}");

            return sb.ToString();
        }
    }
}
