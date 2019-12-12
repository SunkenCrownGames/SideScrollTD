using UnityEngine;

namespace Spawners
{
    public class SpawnerIdentifier : MonoBehaviour
    {
        [SerializeField] private int m_id;
        
        public void SetId(int p_id)
        {
            m_id = p_id;
        }

        public int ID => m_id;
    }
}
