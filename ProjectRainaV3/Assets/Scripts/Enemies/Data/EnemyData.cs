using System;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

namespace Enemies
{ 
    [System.Serializable]
    public class EnemyData : IComparable
    {
        [Header("Object Data")]
        [SerializeField]
        protected GameObject m_enemy;
        [SerializeField]
        protected Sprite m_enemyIcon;

        [Header("Entity Data")]
        [SerializeField]
        protected int m_id;
        [SerializeField]
        protected string m_name;
        [SerializeField]
        protected string m_description;
        [FormerlySerializedAs("m_cost")] [SerializeField]
        protected float m_gold;

        [Header("Stats")]
        [SerializeField]
        private float m_attackRange;
        [SerializeField]
        private float m_speed;
        [SerializeField]
        private float m_maxHealth;
        [SerializeField]
        private float m_damage;
        [SerializeField]
        private float m_attackSpeed;


        public EnemyData(EnemyData p_data)
        {
            m_enemy = p_data.m_enemy;
            m_enemyIcon = p_data.m_enemyIcon; ;

            m_id = p_data.m_id;
            m_name = p_data.m_name;
            m_description = p_data.m_description;
            m_attackRange = p_data.m_attackRange;
            m_speed = p_data.m_speed;
            m_gold = p_data.m_gold;
            m_maxHealth = p_data.m_maxHealth;
            m_damage = p_data.m_damage;
            m_attackSpeed = p_data.m_attackSpeed;
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
                var otherTurretData = p_obj as EnemyData;

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


        public override bool Equals(object obj)
        {
            if (obj is int castedTint)
            {
                return castedTint == m_id;
            }
            else
            {
                return obj is EnemyData otherTurretData && m_id == otherTurretData.m_id;
            }
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        #region Object Data

        public GameObject Enemy
        {
            get { return m_enemy; }
        }

        public Sprite EnemyIcon
        {
            get { return m_enemyIcon; }
        }

        #endregion

        #region Entity Data

        public int Id => m_id;

        public string Name => m_name;

        public string Description => m_description;

        public float Gold => m_gold;

        #endregion

        #region Stat Data

        public float Range => m_attackRange;

        public float Damage => m_damage;

        public float Speed => m_speed;

        public float AttackSpeed => m_attackSpeed;
        
        public float MaxHealth => m_maxHealth;

        #endregion
    }
}
