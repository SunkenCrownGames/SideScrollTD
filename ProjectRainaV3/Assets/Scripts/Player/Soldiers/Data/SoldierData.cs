using System;
using System.Collections.Generic;
using Player.Soldiers.Data.Modifiers;
using Sirenix.OdinInspector;
using UnityEngine;
using PierceModifier = Player.Turrets.Modifiers.PierceModifier;

namespace Player.Soldiers.Data
{
    [System.Serializable]
    public class SoldierData : IComparable
    {
        
        #region Inspector Functions
        
        [Button("Entity Data", ButtonSizes.Medium), GUIColor(255, 60, 0)]
        public void ToggleEntityData()
        {
            m_entityDataToggle = !m_entityDataToggle;
        }
        
        [Button("Object Data", ButtonSizes.Medium), GUIColor(0,145,255)]
        public void ToggleObjectData()
        {
            m_objectDataToggle = !m_objectDataToggle;
        }

        [Button("Modifier Data", ButtonSizes.Medium), GUIColor(1f, 0f, 1f)]
        public void ToggleModifierData()
        { 
            m_modifierDataToggle = !m_modifierDataToggle;
        }

        #endregion

        #region Inspector Values
        
        [ShowIfGroup("m_entityDataToggle")]
        [BoxGroup("m_entityDataToggle/Entity Data")] [SerializeField] protected int m_id;
        [BoxGroup("m_entityDataToggle/Entity Data")] [SerializeField] protected string m_name;
        [BoxGroup("m_entityDataToggle/Entity Data")] [SerializeField] protected string m_description;
        [BoxGroup("m_entityDataToggle/Entity Data")] [SerializeField] protected float m_cost;
        [BoxGroup("m_entityDataToggle/Entity Data")] [SerializeField] protected Stats m_stats;

        [ShowIfGroup("m_objectDataToggle")] 
        [BoxGroup("m_objectDataToggle/Object Data")] [SerializeField] protected GameObject m_bullet; 
        [BoxGroup("m_objectDataToggle/Object Data")] [SerializeField] protected Sprite m_soldierIcon; 
        [BoxGroup("m_objectDataToggle/Object Data")] [SerializeField] protected Sprite m_soldierSelectedIcon;

        [ShowIfGroup("m_modifierDataToggle")] 
        [BoxGroup("m_modifierDataToggle/Modifier Data") ][SerializeField] private List<Modifier> m_activeModifiers;
        [BoxGroup("m_modifierDataToggle/Modifier Data")] [SerializeField] private DamageOverDistanceModifierData m_damageOverDistanceModifierData;
        [BoxGroup("m_modifierDataToggle/Modifier Data")] [SerializeField] private PierceModifierData m_pierceModifierData;

        [HideInInspector]
        public bool m_objectDataToggle;
        [HideInInspector]
        public bool m_entityDataToggle;
        [HideInInspector]
        public bool m_modifierDataToggle;
        #endregion

        #region  Icomparable Overrides
        
        public int CompareTo(object p_obj)
        {
            if (p_obj is int castedTint)
            {
                if (m_id < castedTint)
                {
                    return 1;
                }
                else if (castedTint == m_id)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }

            }
            else
            {
                var otherTurretData = p_obj as SoldierData;

                if (otherTurretData == null || m_id >= otherTurretData.m_id)
                {
                    return otherTurretData != null && m_id == otherTurretData.m_id ? 0 : -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public override bool Equals(object p_obj)
        {
            if (p_obj?.GetType() == typeof(int))
            {
                var castedTint = (int)p_obj;

                return castedTint == m_id;
            }
            else
            {
                return p_obj is SoldierData otherTurretData && m_id == otherTurretData.m_id;
            }
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }
        #endregion
        
        #region Object Data

        public Sprite SoldierIcon => m_soldierIcon;
        public Sprite SelectedSoldierIcon => m_soldierSelectedIcon;
        
        #endregion

        #region Entity Data

        public int Id => m_id;

        public string Name => m_name;

        public string Description => m_description;

        public float Cost => m_cost;

        #endregion
        
        public SoldierData(SoldierData p_data)
        {
            m_soldierIcon = p_data.m_soldierIcon; ;

            m_id = p_data.m_id;
            m_name = p_data.m_name;
            m_description = p_data.m_description;
            m_stats = p_data.StatsData;
            m_bullet = p_data.m_bullet;
            m_activeModifiers = p_data.m_activeModifiers;
            m_damageOverDistanceModifierData = p_data.m_damageOverDistanceModifierData;
            m_pierceModifierData = p_data.m_pierceModifierData;
        }

        public GameObject Bullet => m_bullet;

        public List<Modifier> ActiveModifiers => m_activeModifiers;

        public DamageOverDistanceModifierData DamageOverDistanceModifierData => m_damageOverDistanceModifierData;

        public PierceModifierData PierceModifierData => m_pierceModifierData;

        public Stats StatsData => m_stats;
    }
}