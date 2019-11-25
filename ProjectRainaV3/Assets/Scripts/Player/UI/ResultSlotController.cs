using System;
using System.Collections.Generic;
using System.Linq;
using AngieTools.UI;
using Player.MergedTurret.Data;
using Player.Selection;
using Player.Turrets;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using V2.Data;

namespace Player.UI
{
    public class ResultSlotController : UIMonoBehaviour
    {
        private Stats m_stats = null;
        private TurretData m_turretData;
        private SoldierData m_soldierData;
        
        [SerializeField] private TextMeshProUGUI m_costLabel = null;
        [SerializeField] private TextMeshProUGUI m_turretName = null;
        [SerializeField] private Image m_turretImage = null;
        [SerializeField] private Image m_soldierImage = null;
        [SerializeField] private Image m_mergedImage = null;

        private void Awake()
        {
            BindInstance();
        }

        private void BindInstance()
        {
            if (Instance != null) Destroy(gameObject);

            Instance = this;
        }

        private void UpdateSlot()
        {
            
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            m_costLabel.text = (m_turretData.Cost + m_soldierData.Cost).ToString();
            UpdateMergedData();

        }

        private void UpdateMergedData()
        {
            var filterteTurretList =
                DynamicMergedDatabase.Instance.Data.Where(p_data => p_data.TurretId == m_turretData.Id).ToList();
            
            var filteredSoldierList = filterteTurretList.Where(p_data => p_data.SoldierId == m_soldierData.Id);

            var soldierList = filteredSoldierList.ToList();
            
            if (!soldierList.Any()) return;

            m_stats = m_turretData.StatsData + m_soldierData.StatsData;
            MergedData = soldierList[0];
            m_mergedImage.sprite = MergedData.MergedSprite;
            SelectionPrefabsController.Instance.SetDraggableTurret();
        }
        
        private void ShowInfoScreen()
        {
            InfoUiController.Instance.UpdateStats(new InfoUiData(p_name:MergedData.Name, p_description:MergedData.Description,
                m_stats.MaxHealth, p_damageStat:m_stats.Damage, 
                p_attackSpeedStat:m_stats.AttackSpeed, p_rangeStat:m_stats.AttackRange));
            
            var pos = InfoUiController.Instance.gameObject.transform.position;
            pos.x = transform.position.x;
            // ReSharper disable once Unity.InefficientPropertyAccess
            InfoUiController.Instance.gameObject.transform.position = pos;
            InfoUiController.Instance.gameObject.SetActive(true);
        }

        public void UpdateTurretData(TurretData p_data)
        {
            m_turretData = p_data;
            m_turretImage.sprite = p_data.TurretIcon;

            if (m_soldierData == null) return;
            
            UpdateSlot();
        }

        public void UpdateSoldierData(SoldierData p_data)
        {
            m_soldierData = p_data;
            m_soldierImage.sprite = p_data.SoldierIcon; 
            
            if (m_turretData == null) return;
            
            UpdateSlot();
        }

        public override void OnPointerEnter(PointerEventData p_eventData)
        {
            base.OnPointerEnter(p_eventData);

            if (MergedData == null) return;
            
            ShowInfoScreen();
        }

        public override void OnPointerExit(PointerEventData p_eventData)
        {
            base.OnPointerExit(p_eventData);
            
            InfoUiController.Instance.gameObject.SetActive(false);
        }

        public static ResultSlotController Instance { get; private set; }

        public MergedData MergedData { get; private set; }
    }
}
