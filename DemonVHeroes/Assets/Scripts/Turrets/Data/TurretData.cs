using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Turrets
{
    [System.Serializable]
    public class TurretData : IComparable
    {
        [Title("Assets")]
        [AssetsOnly] [SerializeField] [BoxGroup("Turret")] private GameObject m_turretPrefab = null;
        
        [AssetsOnly] [SerializeField] [BoxGroup("Turret")] private SpriteState m_turretIcons = null;
        
        [Title("Data")]
        [SerializeField] [BoxGroup("Turret")] private int m_id = -1;
        [SerializeField] [BoxGroup("Turret")] private float m_cost = -1;
        [SerializeField] [BoxGroup("Turret")] private string m_turretName = "";
        [SerializeField] [BoxGroup("Turret")] private string m_turretDescription = "";
        [SerializeField] [BoxGroup("Turret")] private Stats m_turretStats = null;


        public TurretData(int p_id, float p_cost, string p_turretName, string p_turretDescription, Stats p_stats, GameObject p_turretPrefab, SpriteState p_turretIcons)
        {
            m_id = p_id;
            m_turretName = p_turretName;
            m_turretDescription = p_turretDescription;
            m_turretStats = p_stats;
            m_turretPrefab = p_turretPrefab;
            m_turretIcons = p_turretIcons;
            m_cost = p_cost;
        }

        public TurretData(TurretData p_otherData)
        {
            m_id = p_otherData.m_id;
            m_turretName = p_otherData.m_turretName;
            m_turretDescription = p_otherData.m_turretDescription;
            m_turretStats = p_otherData.m_turretStats;
            m_turretPrefab = p_otherData.m_turretPrefab;
            m_turretIcons = p_otherData.m_turretIcons;
            m_cost = p_otherData.m_cost;
        }

        #region Icomparable Functions
        
        public int CompareTo(object p_obj)
        {
            if (p_obj is int castedTaint)
            {
                if (m_id < castedTaint)
                {
                    return 1;
                }
                else if (castedTaint == m_id)
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
                var otherTurretData = p_obj as TurretData;

                if (otherTurretData != null && m_id < otherTurretData.m_id)
                {
                    return 1;
                }
                else if (otherTurretData != null && m_id == otherTurretData.m_id)
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
            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (p_obj)
            {
                case int castedTaint:
                    return castedTaint == m_id;
                case TurretData otherTurretData when m_id == otherTurretData.m_id:
                    return true;
                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        
        #endregion
        
        #region Accesors
        
        public GameObject TurretPrefab => m_turretPrefab;

        public SpriteState TurretIcons => m_turretIcons;

        public int Id => m_id;

        public float Cost => m_cost;

        public string TurretName => m_turretName;

        public string TurretDescription => m_turretDescription;

        public Stats TurretStats => m_turretStats;
        
        
        #endregion
    }
}
