using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] protected float m_health;
    [SerializeField] protected float m_attackSpeed;
    [SerializeField] protected float m_attackDamage;
    [SerializeField] protected float m_range;
    [SerializeField] protected float m_movementSpeed;
    public Stats(float p_health, float p_attackSpeed, float p_attackDamage)
    {
        m_health = p_health;
        m_attackSpeed = p_attackSpeed;
        m_attackDamage = p_attackDamage;
    }

    public float Health => m_health;

    public float AttackSpeed => m_attackSpeed;


    public float Range => m_range;

    public float AttackDamage => m_attackDamage;

    public float MovementSpeed => m_movementSpeed;
}
