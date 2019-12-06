using System;
using System.Collections.Generic;
using AngieTools.DataStructures;
using AngieTools.Tools.DataStructure;
using Sirenix.OdinInspector;
using Turrets;
using UnityEngine;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        [Title("Data Assets")]
        [AssetsOnly] [SerializeField] private TurretDatabase m_turretDatabase;
        
        [Title("Scene Assets")]
        [SceneObjectsOnly] [SerializeField] private InventoryUi m_inventoryUi;
        
        [Title("Data")]
        [SerializeField] private List<int> m_turrets;

        private List<TurretData> m_compiledData;

        [Button("Force Update UI")]
        public void ForceUpdateList()
        {
            if (m_inventoryUi != null && m_compiledData != null)
            {
                CompileTurrets();
                m_inventoryUi.UpdateSlots(m_compiledData);
            }
        }

        private void Awake()
        {
            CompileTurrets();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_inventoryUi.UpdateSlots(m_compiledData);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateTurrets(IEnumerable<int> p_turretIds)
        {
            m_turrets = new List<int>(p_turretIds);
            CompileTurrets();
            m_inventoryUi.UpdateSlots(m_compiledData);
        }
        

        private void CompileTurrets()
        {
            m_turretDatabase.UpdateTurretData(MergeSort.MergeSortStart(CustomList<TurretData>.ToCustomList(m_turretDatabase.TurretData)));
            
            if(m_compiledData == null)
                m_compiledData = new List<TurretData>();
            else
                m_compiledData.Clear();

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var t in m_turrets)
            {
                var newData = BinarySearch.StartBinarySearchWithID<TurretData>(CustomList<TurretData>.ToCustomList(m_turretDatabase.TurretData), t);
                if (newData == null) continue;
                
                m_compiledData.Add(newData);
            }
        }

        public List<int> TurretIdsList => m_turrets;

        public List<TurretData> CompiledData => m_compiledData;
    }
}
