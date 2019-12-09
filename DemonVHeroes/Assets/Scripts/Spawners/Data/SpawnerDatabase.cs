using System.Collections.Generic;
using UnityEngine;

namespace Spawners.Data
{
    [CreateAssetMenu(fileName = "SpawnerDatabase", menuName = "DataObjects/Spawner/Spawner Database", order = 1)]
    public class SpawnerDatabase : ScriptableObject
    {
        [SerializeField] private List<SpawnerData> m_data;


        public List<SpawnerData> Data => m_data;
    }
}
