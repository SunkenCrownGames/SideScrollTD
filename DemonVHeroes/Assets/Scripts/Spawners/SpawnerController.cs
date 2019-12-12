using System;
using Sirenix.OdinInspector;
using Spawners.Data;
using UnityEngine;

namespace Spawners
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField] private SpawnerData m_data;
        [SerializeField] private Spawner m_spawnerSlotRef;

        

        // Start is called before the first frame update
        void Start()
        {
            SpawnerManager.Instance.SpawnerControllers.Add(this);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void InitializeData(SpawnerData p_data, Spawner p_spawner)
        {
            m_data = p_data;
            m_spawnerSlotRef = p_spawner;
        }

        [Button("Destroy Spawner")]
        private void DestroySpawner()
        {
            m_spawnerSlotRef.ResetSlot();
            Debug.Log("Resetting Slot");
            SpawnerManager.Instance.SpawnerControllers.Remove(this);
            Destroy(gameObject);
        }
    }
}
