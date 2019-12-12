using System;
using System.Collections.Generic;
using System.Linq;
using Spawners.Data;
using Spawners.UI;
using UnityEngine;

namespace Spawners
{
    public class GhostSpawner : MonoBehaviour
    {
        private static GhostSpawner _instance;
        
        private GameObject m_activeSprite;
        
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


        public void EnableSprite(Vector3 p_position)
        {
            var data = SpawnerUiController.Instance.SelectedSlot.Data;
            
            var spriteToEnable = data.VisualType == SpawnerData.SpawnerVisualType.Sprite
                ? data.SpawnerSpritePrefab
                : data.SpawnerSpinePrefab;
            
            if (m_activeSprite != null)
            {
                var identifier = m_activeSprite.GetComponent<SpawnerIdentifier>().ID;
                var newIdentifier = data.ID;

                if (identifier == newIdentifier)
                {
                    transform.position = p_position;
                    m_activeSprite.SetActive(true);
                    return;
                }
                
                Destroy(m_activeSprite);
            }

            m_activeSprite = Instantiate(spriteToEnable, Vector3.zero, Quaternion.identity, transform);
            m_activeSprite.GetComponent<SpawnerIdentifier>().SetId(data.ID);

            transform.position = p_position;
            m_activeSprite.transform.localPosition = Vector3.zero;
        }
        
        public void DisableSprite()
        {
            m_activeSprite.SetActive(false);
        }

        private void BindInstance()
        { 
            if (_instance != null) Destroy(gameObject);
            
            _instance = this;
        }
        

        public static GhostSpawner Instance => _instance;
    }
}
