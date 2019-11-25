using System.Text;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class Stats
    {
        [SerializeField]
        protected float m_maxHealth;
        [SerializeField]
        protected float m_damage;
        [SerializeField]
        protected float m_attackSpeed;
        [SerializeField]
        protected float m_attackRange;
        [SerializeField]
        protected float m_bulletSpeed;

        public Stats(float p_maxHealth, float p_damage, float p_attackSpeed, float p_attackRange, float p_bulletSpeed)
        {
            m_maxHealth = p_maxHealth;
            m_damage = p_damage;
            m_attackSpeed = p_attackSpeed;
            m_attackRange = p_attackRange;
            m_bulletSpeed = p_bulletSpeed;
            m_maxHealth = p_maxHealth;
        }

        private Stats()
        {
            
        }

        public static Stats operator +(Stats p_stats1, Stats p_stats2)
        {
            var newStats = new Stats
            {
                m_damage =  p_stats1.m_damage + p_stats2.m_damage,
                m_attackSpeed = p_stats1.m_attackSpeed + p_stats2.m_attackSpeed,
                m_attackRange = p_stats1.m_attackRange + p_stats2.m_attackRange,
                m_bulletSpeed = p_stats1.m_bulletSpeed + p_stats2.m_bulletSpeed,
                m_maxHealth =  p_stats1.m_maxHealth + p_stats2.m_maxHealth
            };
            
            return newStats;
        }

        public float MaxHealth => m_maxHealth;

        public float Damage => m_damage;

        public float AttackSpeed => m_attackSpeed;

        public float AttackRange => m_attackRange;

        public float BulletSpeed => m_bulletSpeed;
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Max Health: " + m_maxHealth);
            sb.AppendLine("Damage: " + m_damage);
            sb.AppendLine("Attack Speed: " +  m_attackSpeed);
            sb.AppendLine("Attack Range: " + m_attackRange);
            sb.AppendLine("Bullet Speed: " + m_bulletSpeed);


            return sb.ToString();
        }
    }
}