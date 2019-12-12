using TMPro;
using UnityEngine;

namespace Player.UI.PlayerInfo
{
    public class PlayerInfoComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_val;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ToggleInfo()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }


        public void UpdateComponent(string p_val)
        {
            gameObject.SetActive(true);
            m_val.text = p_val;
        }
    }
}
