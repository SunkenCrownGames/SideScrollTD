using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;
using Weapons.Modifiers;

namespace Weapons
{
    public class BulletDriver : MonoBehaviour
    {
        protected Modifier[] m_modifiers = null;
        protected ModifierStateData m_modifierStateData;
        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void SetModifiers(params Modifier[] p_activeModifiers)
        {
            var filteredModifiers = p_activeModifiers.Where(p_modifier => p_modifier.Status).ToArray();
            
            if (m_modifiers == null)
                m_modifiers = new Modifier[filteredModifiers.Length];

            Array.Copy(filteredModifiers, m_modifiers, filteredModifiers.Length);
            var sb = new StringBuilder();
            
            foreach (var modifier in m_modifiers)
            {
                sb.AppendLine(modifier.ToString());
            }
            
            Debug.Log(sb.ToString());
        }
    }
}
