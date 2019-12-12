using System;
using AngieTools.V2Tools.Pathing.Dijkstra;
using Player.UI;
using Sirenix.OdinInspector;
using Spawners.Data;
using Spawners.UI;
using UnityEngine;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private bool m_instant;

        [SerializeField] private SpawnerController m_controller;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var data = SpawnerUiController.Instance.SelectedSlot.Data;
                var sprite = data.VisualType == SpawnerData.SpawnerVisualType.Sprite
                    ? data.SpawnerSpritePrefab
                    : data.SpawnerSpinePrefab;

                var spawnedSpawner = Instantiate(sprite, transform.position, Quaternion.identity,
                    PathingManager.Instance.SpawnersParent);
                
                spawnedSpawner.GetComponent<SpawnerIdentifier>().SetId(data.ID);
                var controller = spawnedSpawner.AddComponent<SpawnerController>();
                controller.InitializeData(data, this);
                m_controller = controller;
                GhostSpawner.Instance.DisableSprite();
                DisableSpawnerVisual();
            }
        }

        private void OnMouseEnter()
        {
            GhostSpawner.Instance.EnableSprite(transform.position); 
        }

        private void OnMouseExit()
        {
            GhostSpawner.Instance.DisableSprite();
        }


        public void UpdateSpawnerVisual()
        {
            if (m_instant && m_controller == null)
            {
                gameObject.SetActive(true);
            }
        }

        public void DisableSpawnerVisual()
        {
            if (m_instant)
            {
                gameObject.SetActive(false);
            }
        }

        [Button("Reset Slot")]
        public void ResetSlot()
        {
            m_controller = null;
            
            if(!SpawnerUiController.Instance.OnUi)
                UpdateSpawnerVisual();
        }
        
        public bool Instant => m_instant;

        public SpawnerController Controller => m_controller;
    }
}
