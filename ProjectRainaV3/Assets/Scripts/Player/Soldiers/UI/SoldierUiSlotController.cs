using AngieTools.UI;
using Player.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using V2.Data;

namespace Player.Soldiers.UI
{
    public class SoldierUiSlotController : UIMonoBehaviour
    {
        [SerializeField] private bool m_lockState;

        private SoldierData m_data = null;
        
        [SerializeField] private TextMeshProUGUI m_costLabel = null;
        [SerializeField] private Image m_soldierIcon = null;

        private void ShowInfoScreen()
        {
            InfoUiController.Instance.UpdateStats(new InfoUiData(p_name:m_data.Name, p_description:m_data.Description,
                m_data.StatsData.MaxHealth, p_damageStat:m_data.StatsData.Damage, 
                p_attackSpeedStat:m_data.StatsData.AttackSpeed, p_rangeStat:m_data.StatsData.AttackRange));
                        
            var pos = InfoUiController.Instance.gameObject.transform.position;
            pos.x = transform.position.x;
            
            // ReSharper disable once Unity.InefficientPropertyAccess
            InfoUiController.Instance.gameObject.transform.position = pos;
            InfoUiController.Instance.gameObject.SetActive(true);
        }

        public void UpdateSoldierInfo(SoldierData p_data)
        {
            if (m_lockState) return;
            
            m_data = p_data;
            
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            m_costLabel.text = p_data.Cost.ToString();
            m_soldierIcon.sprite = p_data.SoldierIcon;
        }
        
        
        public override void OnPointerEnter(PointerEventData p_eventData)
        {
            base.OnPointerEnter(p_eventData);
            
            if(m_lockState || m_data == null) return;

            ShowInfoScreen();
        }
        
        public override void OnPointerExit(PointerEventData p_eventData)
        {
            base.OnPointerExit(p_eventData);
            InfoUiController.Instance.gameObject.SetActive(false);
        }

        public override void OnPointerClick(PointerEventData p_eventData)
        {
            base.OnPointerClick(p_eventData);
            ResultSlotController.Instance.UpdateSoldierData(m_data);
        }
        


        public void UpdateLockState(bool p_b, Sprite p_soldierLockedIcon)
        {
            m_lockState = p_b;

            if (!p_b) return;
            
            m_soldierIcon.sprite = p_soldierLockedIcon;
        }
    }
}
