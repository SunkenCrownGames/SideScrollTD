using System;
using System.Collections.Generic;
using AngieTools;
using Level;
using Soldier;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class SpawnerManager : MonoBehaviour
    {

        [SerializeField] private Transform m_soldiersParent;
        [SerializeField] private List<SpawnerController> m_spawnerControllers;

        private static SpawnerManager _instance;

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
            if(_instance != null) Destroy(gameObject);

            _instance = this;
        }

        public void SpawnSoldiers()
        {
            var pos = 0;
            
            foreach (var controller in m_spawnerControllers)
            {
                if (controller == null) continue;

                bool direction = false;
                
                for (var i = 0; i < controller.Data.SoldierSpawnCount; i++)
                {
                    var state = (!direction) ? SoldierState.Left : SoldierState.Right;
                    if (m_soldiersParent == null) break;

                    var soldier = Instantiate(controller.Data.SoldierData.Prefab, controller.transform.position, Quaternion.identity,
                        m_soldiersParent);

                    var soldierController = soldier.GetComponent<SoldierController>();
                    soldierController.SetData(controller.Data.SoldierData, state);
                    var spriteRenderer = soldierController.GetComponent<SpriteRenderer>();
                    spriteRenderer.color = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1);
                    spriteRenderer.sortingOrder = controller.SpriteRenderer.sortingOrder + 1 + pos;
                    direction = !direction;
                    pos++;
                }
            }
        }

        public Platform GetDestination()
        {
            if (m_spawnerControllers.Count <= 0) return null;
            
            var randomSpawnerTarget = Random.Range(0, m_spawnerControllers.Count);
            var spawnerTarget = m_spawnerControllers[randomSpawnerTarget].SpawnerSlotRef.Platform;
            Debug.Log(spawnerTarget.name);

            return spawnerTarget;

        }
        
        

        public List<SpawnerController> SpawnerControllers => m_spawnerControllers;

        public static SpawnerManager Instance => _instance;
    }
}
