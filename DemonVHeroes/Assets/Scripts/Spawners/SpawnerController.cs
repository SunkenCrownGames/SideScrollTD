using System;
using Level;
using Sirenix.OdinInspector;
using Spawners.Data;
using UnityEngine;

namespace Spawners
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private SpawnerData m_data;
        [SerializeField] private Spawner m_spawnerSlotRef;

        private void Awake()
        {
            BindComponents();
        }

        // Start is called before the first frame update
        void Start()
        {
            SpawnerManager.Instance.SpawnerControllers.Add(this);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void BindComponents()
        {
            var sr = GetComponent<SpriteRenderer>();
            var childSr = sr.GetComponentInChildren<SpriteRenderer>();

            m_spriteRenderer = (sr != null) ? childSr : sr;   
        }

        public void InitializeData(SpawnerData p_data, Spawner p_spawner, Platform p_platform)
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

        public SpawnerData Data => m_data;

        public SpriteRenderer SpriteRenderer => m_spriteRenderer;

        public Spawner SpawnerSlotRef => m_spawnerSlotRef;
    }
}
