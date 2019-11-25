using System;
using AngieTools.GameObjectTools;
using AngieTools.UI;
using Player.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player.Turrets.UI
{
    public class TurretSlotUiController : UIMonoBehaviour
    {
        [SerializeField] private bool m_lockState;
        
        private TurretData m_data = null;
        
        [SerializeField] private TextMeshProUGUI m_costLabel = null;
        [SerializeField] private Image m_turretIcon = null;


        private void UpdateSlot()
        {
            if (m_lockState) return;
            
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            m_costLabel.text = m_data.Cost.ToString();
            m_turretIcon.sprite = m_data.TurretIcon;
        }
        
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

        private void AddTurretToCrafting()
        {
            Debug.Log("Clicked");
            ResultSlotController.Instance.UpdateTurretData(m_data);
        }

        
        public void UpdateTurretInfo(TurretData p_info)
        {
            m_data = p_info;
            UpdateSlot();
        }

        public override void OnPointerEnter(PointerEventData p_eventData)
        {
            base.OnPointerEnter(p_eventData);
            
            if(m_lockState || m_data == null) return;
            
            ShowInfoScreen();
        }

        public override void OnPointerClick(PointerEventData p_eventData)
        {
            base.OnPointerClick(p_eventData);

            if (m_lockState || m_data == null) return;
            
            AddTurretToCrafting();
            
        }


        public override void OnPointerExit(PointerEventData p_eventData)
        {
            base.OnPointerExit(p_eventData);
            InfoUiController.Instance.gameObject.SetActive(false);
        }

        public void UpdateLockState(bool p_b, Sprite p_lockedSprite)
        {
            m_lockState = p_b;

            if (m_lockState)
            {
                m_turretIcon.sprite = p_lockedSprite;
            }
            else if(m_data != null)
            {
                m_turretIcon.sprite = m_data.TurretIcon;
            }
        }
    }
}
