using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnerManager : MonoBehaviour
    {

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
        
        

        public List<SpawnerController> SpawnerControllers => m_spawnerControllers;

        public static SpawnerManager Instance => _instance;
    }
}
