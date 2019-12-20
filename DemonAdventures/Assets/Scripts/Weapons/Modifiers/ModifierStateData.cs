using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Modifiers
{
    [System.Serializable]
    public class ModifierStateData
    {
        [SerializeField] private List<ModifierStateStatus> m_modifierStateStatuses;

        public List<ModifierStateStatus> ModifierStateStatuses => m_modifierStateStatuses;
    }
}
