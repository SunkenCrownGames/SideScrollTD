using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable BaseObjectGetHashCodeCallInGetHashCode

namespace Player.Turrets
{
    [System.Serializable]
    public class TurretData : IComparable
    {
        [Header("Bullet Data")]
        [SerializeField]
        protected GameObject m_bulletPrefabs;
        [SerializeField]
        protected Vector3 m_bulletSpawnOffset = Vector3.zero;
        
        [SerializeField]
        protected Sprite m_turretIcon;

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

        public TurretData(TurretData p_data)
        {
            m_bulletPrefabs = p_data.m_bulletPrefabs;
            m_turretIcon = p_data.m_turretIcon; ;
            
            m_id = p_data.m_id;
            m_name = p_data.m_name;
            m_description = p_data.m_description;

            m_stats = p_data.m_stats;
        }

        public int CompareTo(object p_obj)
        {
            if (p_obj is int castedTint)
            {
                if(m_id < castedTint)
                {
                    return 1;
                }
                else if(castedTint == m_id)
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
                TurretData otherTurretData = p_obj as TurretData;

                if(otherTurretData != null && m_id < otherTurretData.m_id)
                {
                    return 1;
                }
                else if(otherTurretData != null && m_id == otherTurretData.m_id)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }

        public override bool Equals(object p_obj)
        {
            switch (p_obj)
            {
                case int castedTint when castedTint == m_id:
                    return true;
                case int castedTint:
                    return false;
                case TurretData otherTurretData when m_id == otherTurretData.m_id:
                    return true;
                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public Vector3 SpawnOffset => m_bulletSpawnOffset;

        #region Object Data

        public Sprite TurretIcon => m_turretIcon;

        public GameObject BulletPrefabs => m_bulletPrefabs;

        #endregion

        #region Entity Data

        public int Id => m_id;

        public string Name => m_name;

        public string Description => m_description;

        public float Cost => m_cost;

        #endregion

        public Stats StatsData => m_stats;
    }
}
