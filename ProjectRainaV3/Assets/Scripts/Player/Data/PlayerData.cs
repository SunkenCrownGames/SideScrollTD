using UnityEngine;

namespace Player.Data
{
    [System.Serializable]
    public class PlayerData
    {
        [SerializeField] private float m_hp;

        [SerializeField] private float m_gold;
        
        public PlayerData()
        {
            m_hp = 0;
            m_gold = 0;
        }
        
        public PlayerData(float p_hp, float p_gold)
        {
            m_hp = p_hp;
            m_gold = p_gold;
        }

        public PlayerData(PlayerData p_data)
        {
            m_hp = p_data.m_hp;
            m_gold = p_data.m_gold;
        }
        
        public void SetHp(float p_val)
        {
            m_hp = p_val;
        }


        public void SetGold(float p_val)
        {
            m_gold = p_val;
        }
    }
}
