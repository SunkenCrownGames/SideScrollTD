using Player.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Title("Data")]
        [SerializeField] private float m_gold;
        
        [Title("Required Components")]
        [SerializeField] [Required] private SetUpUi m_setUpUi;
        
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
            m_setUpUi.UpdateGold(m_gold);
        }

        private void SetGold(float p_gold)
        {
            m_gold = p_gold;
        }

        private void IncreaseGold(float p_gold)
        {
            m_gold += p_gold;
        }
        private void DecreaseGold(float p_gold)
        {
            m_gold -= p_gold;
        }
    }
}
