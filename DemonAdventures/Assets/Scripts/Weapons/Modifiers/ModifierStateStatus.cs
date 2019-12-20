using UnityEngine;

namespace Weapons.Modifiers
{
    [System.Serializable]
    public class ModifierStateStatus
    {
        [SerializeField] private bool m_status;
        [SerializeField] private ModifierType m_type;


        public bool Status => m_status;

        public ModifierType Type => m_type;
    }
}
