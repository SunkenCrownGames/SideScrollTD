using Player.UI.PlayerInfo;
using Sirenix.OdinInspector;
using Spawners.UI;
using UnityEngine;

namespace Player.UI
{
    public class SetUpUi : MonoBehaviour
    {

        [SerializeField] [Required] private SpawnerUiController m_uiController;
        [SerializeField] [Required] private PlayerInfoUi m_infoUi;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateCost(float p_value)
        {
            m_infoUi.Cost.UpdateComponent(p_value.ToString());
        }

        public void UpdateGold(float p_val)
        {
            m_infoUi.Gold.UpdateComponent(p_val.ToString());
        }
    }
}
