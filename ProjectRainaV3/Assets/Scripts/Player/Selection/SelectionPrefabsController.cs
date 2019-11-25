using System.Collections.Generic;
using System.Linq;
using Player.MergedTurret.Data;
using Player.UI;
using UnityEngine;

namespace Player.Selection
{
    public sealed class SelectionPrefabsController : MonoBehaviour
    {
        private SelectionData m_activeSelection;

        private void Awake()
        {
            BindComponents();
        }

        private void Start()
        {
            
        }

        private void BindComponents()
        {
            if(Instance != null) Destroy(gameObject);


            Instance = this;
            
            if (Data == null)
            {
                Data = new List<SelectionData>();
            }
            
        }

        /// <summary>
        /// Will Set the current draggable turret based on the currently merged one
        /// Will Create A new one if a reference to that specific turret has not been created yet
        /// </summary>
        public void SetDraggableTurret()
        {
            var curMergeData = ResultSlotController.Instance.MergedData;
            SelectionData foundData = null;

            foreach (var data in Data.Where(p_data => curMergeData.Equals(p_data.Data)))
            {
                foundData = data;
            }

            if (foundData != null) return;


            var newObject = Instantiate(curMergeData.Prefab, Vector3.zero, Quaternion.identity,
                transform);

            newObject.AddComponent<SelectionTurretController>();
            
            newObject.SetActive(false);
            
            
            Debug.Log("Added New Selectable Object To list");

            var newData = new SelectionData(newObject, curMergeData);
            
            Data.Add(newData);
            m_activeSelection = newData;

        }

        public static SelectionPrefabsController Instance { get; private set; }
        
        [field: SerializeField]
        public List<SelectionData> Data { get; private set; }
    }
}
