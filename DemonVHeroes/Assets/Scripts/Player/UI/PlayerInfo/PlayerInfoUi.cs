using UnityEngine;

namespace Player.UI.PlayerInfo
{
    public class PlayerInfoUi : MonoBehaviour
    {
        [SerializeField] private PlayerInfoComponent m_gold;

        [SerializeField] private PlayerInfoComponent m_cost;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        public PlayerInfoComponent Gold => m_gold;

        public PlayerInfoComponent Cost => m_cost;
    }
}
