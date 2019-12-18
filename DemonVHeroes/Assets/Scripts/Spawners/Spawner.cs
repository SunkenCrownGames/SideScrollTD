using System;
using AngieTools.V2Tools.Pathing.Dijkstra;
using Level;
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
        [SerializeField] private Platform m_platform;

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
                
                if (GameManager.Instance.Player.CheckGold(data.Cost))
                {
                    GameManager.Instance.Player.DecreaseGold(data.Cost);
                    SpawnPlatform(data);
                }
            }
        }

        private void SpawnPlatform(SpawnerData p_data)
        {
            var sprite = p_data.VisualType == SpawnerData.SpawnerVisualType.Sprite
                ? p_data.SpawnerSpritePrefab
                : p_data.SpawnerSpinePrefab;

            var spawnedSpawner = Instantiate(sprite, transform.position, Quaternion.identity,
                PathingManager.Instance.SpawnersParent);
                
            spawnedSpawner.GetComponent<SpawnerIdentifier>().SetId(p_data.ID);
            var controller = spawnedSpawner.AddComponent<SpawnerController>();
            controller.InitializeData(p_data, this, m_platform);
            m_controller = controller;
            GhostSpawner.Instance.DisableSprite();
            DisableSpawnerVisual();
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

        public void UpdatePlatform(Platform p_platform)
        {
            m_platform = p_platform;
        }

        [Button("Reset Slot")]
        public void ResetSlot()
        {
            m_controller = null;
            
            if(!SpawnerUiController.Instance.OnUi)
                UpdateSpawnerVisual();
        }
        
        public bool Instant => m_instant;

        public Platform Platform => m_platform;

        public SpawnerController Controller => m_controller;
    }
}
