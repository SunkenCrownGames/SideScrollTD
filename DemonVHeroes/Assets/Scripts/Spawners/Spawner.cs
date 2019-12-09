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

        public bool Instant => m_instant;

        public SpawnerController Controller => m_controller;
    }
}
