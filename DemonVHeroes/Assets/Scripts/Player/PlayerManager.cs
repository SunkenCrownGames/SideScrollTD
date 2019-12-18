using Player.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Title("Data")]
        [SerializeField] private float m_gold;

        // Start is called before the first frame update
        private void Start()
        {
            UpdateGold();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void UpdateGold()
        {
            GameManager.Instance.UiManager.SetupUi.UpdateGold(m_gold);
        }
        
        public void SetGold(float p_gold)
        {
            m_gold = p_gold;
            
            UpdateGold();
        }

        public void IncreaseGold(float p_gold)
        {
            m_gold += p_gold;
            
            UpdateGold();
        }
        public void DecreaseGold(float p_gold)
        {
            m_gold -= p_gold;

            if (m_gold < 0)
                m_gold = 0;
            
            UpdateGold();
        }

        public bool CheckGold(float p_cost)
        {
            return m_gold >= p_cost;
        }
    }
}
