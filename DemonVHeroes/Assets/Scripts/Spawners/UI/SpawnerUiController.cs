using System.Collections.Generic;
using AngieTools.UI;
using AngieTools.V2Tools.Pathing.Dijkstra;
using Sirenix.OdinInspector;
using Spawners.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Spawners.UI
{
    public class SpawnerUiController : UIMonoBehaviour
    {
        [SerializeField] private SpawnerSlot m_selectedSlot;
        
        [SerializeField] private List<SpawnerSlot> m_slots;
        [SerializeField] private SpawnerDatabase m_database;
        
        [SerializeField] private GameObject m_selectedObject;
        
        private void Awake()
        {
            BindInstance();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        

        private void BindInstance()
        {
            if(Instance != null) Destroy(gameObject);

            Instance = this;
            gameObject.SetActive(false);
        }

        [Button("Update Slots")]
        public void UpdateSelection()
        {
            for (var i = 0; i < m_slots.Count; i++)
            {
                if (i < m_database.Data.Count)
                {
                    m_slots[i].UpdateSlot(m_database.Data[i]);
                }
                else
                {
                    m_slots[i].gameObject.SetActive(false);
                }
            }
        }

        [Button("Toggle Spawner UI")]
        public void ToggleMenu()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        public override void OnPointerEnter(PointerEventData p_eventData)
        {
            base.OnPointerEnter(p_eventData);
            PathingManager.Instance.ActivePlatforms.ForEach(p_platform => p_platform.DisableSpawners());
            m_onUi = true;
            
        }

        public override void OnPointerExit(PointerEventData p_eventData)
        {
            base.OnPointerExit(p_eventData);
            PathingManager.Instance.ActivePlatforms.ForEach(p_platform => p_platform.UpdateSpawners());
            m_onUi = false;
        }


        public void ToggleController(bool p_toggle)
        {
            gameObject.SetActive(p_toggle);
        }

        public void UpdateSelectedSlot(SpawnerSlot p_slot)
        {
            m_selectedSlot = p_slot;
            m_selectedObject.transform.position = m_selectedSlot.transform.position;
            m_selectedObject.gameObject.SetActive(true);
        }

        public static SpawnerUiController Instance { get; private set; }
    }
}
