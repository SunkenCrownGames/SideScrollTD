using UnityEditor.IMGUI.Controls;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats : Stats
    {
        
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        [SerializeField] private float m_movementSpeed;

        public EnemyStats(float p_movementSpeed, float p_health, float p_attackSpeed, float p_attackDamage) : base(p_health, p_attackSpeed, p_attackDamage)
        {
            m_movementSpeed = p_movementSpeed;
            m_health = p_health;
            m_attackSpeed = p_attackSpeed;
            m_attackDamage = p_attackDamage;
        }

        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once ConvertToAutoProperty
        public float MovementSpeed => m_movementSpeed;

        public float Health1 => m_health;

        public float AttackSpeed1 => m_attackSpeed;

        public float AttackDamage1 => m_attackDamage;
    }
}
