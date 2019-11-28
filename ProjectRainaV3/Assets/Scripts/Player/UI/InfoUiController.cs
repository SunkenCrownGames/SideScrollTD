using System;
using TMPro;
using UnityEngine;

namespace Player.UI
{
    public class InfoUiController : MonoBehaviour
    {
        [SerializeField] private bool m_startOff = false;
        
        [SerializeField] private TextMeshProUGUI m_name = null;
        [SerializeField] private TextMeshProUGUI m_description = null;
        [SerializeField] private TextMeshProUGUI m_hpStats = null;
        [SerializeField] private TextMeshProUGUI m_attackStat = null;
        [SerializeField] private TextMeshProUGUI m_speedStat = null;
        [SerializeField] private TextMeshProUGUI m_rangeStat = null;


        private void Awake()
        {
            BindInstance();
        }

        private void BindInstance()
        {
            if(Instance != null) Destroy(gameObject);

            Instance = this;
            
            if(m_startOff) gameObject.SetActive(false);
        }

        public void UpdateStats(InfoUiData p_data)
        {
            m_name.text = p_data.Name;
            m_description.text = p_data.Description;
            m_hpStats.text = p_data.HpStat.ToString();
            m_attackStat.text = p_data.DamageStat.ToString();
            m_speedStat.text = p_data.AttackSpeedStat.ToString();
            m_rangeStat.text = p_data.RangeStat.ToString();
        }

        public static InfoUiController Instance { get; private set; }
    }
}
