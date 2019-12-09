using Sirenix.OdinInspector;
using Soldier.Data;
using UnityEngine;
// ReSharper disable ConvertToAutoProperty

namespace Spawners.Data
{
    [CreateAssetMenu(fileName = "SpawnerData", menuName = "DataObjects/Spawner/Spawner Data", order = 2)]
    public class SpawnerData : ScriptableObject
    {
        [Title("Description Data")]
        [SerializeField] private string m_spawnerName;
        [SerializeField] private string m_spawnerDescription;
        
        [Title("Asset Data")]
        [AssetsOnly] [SerializeField] private GameObject m_spawnerPrefab;
        [AssetsOnly] [SerializeField] private SoldierData m_soldierData;
        [AssetsOnly] [SerializeField] private Sprite m_soldierIcon;
        
        [Title("Game Data")]
        [SerializeField] private int m_soldierSpawnCount = 0;

        public SoldierData SoldierData => m_soldierData;

        public Sprite SoldierIcon => m_soldierIcon;

        public int SoldierSpawnCount => m_soldierSpawnCount;
    }
}
