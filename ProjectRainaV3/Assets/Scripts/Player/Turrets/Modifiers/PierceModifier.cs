using Player.Soldiers.Data.Modifiers;
using UnityEngine;

namespace Player.Turrets.Modifiers
{
    public class PierceModifier : MonoBehaviour
    {
        [SerializeField] private float m_pierceCount = 0;
        [SerializeField] private PierceModifierData m_data = null;
        
        public void InitializePierce(PierceModifierData p_pierceModifierData)
        {
            m_data = new PierceModifierData(p_pierceModifierData);
        }
    }
}
