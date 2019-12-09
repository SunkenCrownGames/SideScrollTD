using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Soldier.Data
{
    [CreateAssetMenu(fileName = "SoldierData", menuName = "DataObjects/Soldiers/Soldier Data", order = 2)]
    public class SoldierData : ScriptableObject
    {
        [Title("Description Data")]
        [SerializeField] private string m_name;
        [SerializeField] private string m_description;

        [Title("Asset Data")]
        [SerializeField] [AssetsOnly] private GameObject m_prefab;
        
        [Title("Game Data")]
        [SerializeField] private int m_id;
        [SerializeField] private Stats m_stats;


        public SoldierData(SoldierData p_data)
        {
            m_prefab = p_data.m_prefab;
            m_id = p_data.m_id;
            m_stats = p_data.m_stats;
            m_name = p_data.m_name;
            m_description = p_data.m_description;
        }
        
        public SoldierData(GameObject p_prefab, int p_id, Stats p_stats, string p_name, string p_description)
        {
            m_prefab = p_prefab;
            m_id = p_id;
            m_stats = p_stats;
            m_description = p_description;
            m_name = p_name;
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
                var otherTurretData = p_obj as SoldierData;

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
                case SoldierData otherTurretData when m_id == otherTurretData.m_id:
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
        

        public GameObject Prefab => m_prefab;

        public int Id => m_id;

        public Stats Stats => m_stats;

        public string Name => m_name;

        public string Description => m_description;
    }
}
