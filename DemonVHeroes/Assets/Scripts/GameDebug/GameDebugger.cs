using System.Collections.Generic;
using System.Net.Configuration;
using AngieTools.V2Tools;
using Player;
using Sirenix.OdinInspector;
using Turrets;
using UnityEngine;
using AngieTools.V2Tools;

namespace GameDebug
{
    public class GameDebugger : MonoBehaviour
    {
        [Title(" Inventory Debug")] 
        [SceneObjectsOnly] [SerializeField] private Inventory m_inventory;
        [SerializeField] private List<int> m_debugInventoryList;
        [ShowInInspector] [SerializeField] [ReadOnly] private List<int> m_inventoryIdList;
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "m_turretName")]
        [ShowInInspector] [SerializeField] [ReadOnly] private List<TurretData> m_inventoryDataList;
        

        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = false, ButtonHeight = 20, Name = "Update Inventory")]
        public void UpdateInventoryDebug()
        {
            m_inventory.UpdateTurrets(m_debugInventoryList);
            
            m_inventoryIdList = m_inventory.TurretIdsList;
            m_inventoryDataList = m_inventory.CompiledData;
        }

        private CustomGrid m_testGrid;

        // Start is called before the first frame update
        private void Start()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
            
        }
    }
}
