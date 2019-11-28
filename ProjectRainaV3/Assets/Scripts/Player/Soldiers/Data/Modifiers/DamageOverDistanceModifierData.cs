using UnityEngine;

namespace Player.Soldiers.Data.Modifiers
{
    [System.Serializable]
    public class DamageOverDistanceModifierData
    {
        [SerializeField] private float m_damageMultiplier;
        [SerializeField] private float m_maxDistance;
        [SerializeField] private float m_maxDamage;

        public DamageOverDistanceModifierData(DamageOverDistanceModifierData p_data)
        {
            m_damageMultiplier = p_data.m_damageMultiplier;
            m_maxDamage = p_data.m_maxDamage;
            m_maxDistance = p_data.m_maxDistance;
        }
    }
}
