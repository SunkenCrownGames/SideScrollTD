using UnityEngine;

namespace Player.Soldiers.Data.Modifiers
{
    [System.Serializable]
    public class PierceModifierData
    {


        public PierceModifierData(PierceModifierData p_data)
        {
            m_maxPierceCount = p_data.m_maxPierceCount;
        }
        
        public PierceModifierData()
        {
            
        }

        public PierceModifierData(int p_maxPierceCount)
        {
            m_maxPierceCount = p_maxPierceCount;    
        }

        [SerializeField] private int m_maxPierceCount;
    }
}
