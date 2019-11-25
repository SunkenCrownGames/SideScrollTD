using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngieTools.DataStructures;
using Player;
using UnityEngine.Serialization;

namespace V2.Data
{
    [System.Serializable]
    public class SoldierData : IComparable
    {
        [Header("Object Data")]
        [SerializeField]
        protected Sprite m_soldierIcon;

        [Header("Modifier Data")]
        [SerializeField]
        protected Sprite m_modifierIcon;


        [Header("Entity Data")]
        [SerializeField]
        protected int m_id;
        [SerializeField]
        protected string m_name;
        [SerializeField]
        protected string m_description;
        [SerializeField]
        protected float m_cost;
        [SerializeField]
        protected Stats m_stats;
        
        [SerializeField]
        protected GameObject m_bullet;
        

        public SoldierData(SoldierData p_data)
        {
            m_soldierIcon = p_data.m_soldierIcon; ;

            m_id = p_data.m_id;
            m_name = p_data.m_name;
            m_description = p_data.m_description;
            m_stats = p_data.StatsData;
            m_bullet = p_data.m_bullet;
        }

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

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Int32))
            {
                int castedToint = (int)obj;

                if (castedToint == m_id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return obj is SoldierData otherTurretData && m_id == otherTurretData.m_id;
            }
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        // ADD MODIFIER LOGIC

        #region Object Data

        public Sprite SoldierIcon => m_soldierIcon;

        public Sprite ModifierIcon => m_modifierIcon;

        #endregion

        #region Entity Data

        public int Id => m_id;

        public string Name => m_name;

        public string Description => m_description;

        public float Cost => m_cost;

        #endregion


        public GameObject Bullet => m_bullet;

        public Stats StatsData => m_stats;
    }
}