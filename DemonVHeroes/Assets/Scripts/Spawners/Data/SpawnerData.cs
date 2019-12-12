using Sirenix.OdinInspector;
using Soldier.Data;
using UnityEngine;
// ReSharper disable ConvertToAutoProperty

namespace Spawners.Data
{
    [CreateAssetMenu(fileName = "SpawnerData", menuName = "DataObjects/Spawner/Spawner Data", order = 2)]
    public class SpawnerData : ScriptableObject
    {
        [BoxGroup("Identification Data")] [SerializeField] private int m_iD;
        [BoxGroup("Identification Data")] [SerializeField] private string m_spawnerName;
        [BoxGroup("Identification Data")] [SerializeField] private string m_spawnerDescription;
        
        [BoxGroup("Visual Prefabs")] [AssetsOnly] [SerializeField] private GameObject m_spawnerSpinePrefab;
        [BoxGroup("Visual Prefabs")] [AssetsOnly] [SerializeField] private GameObject m_spawnerSpritePrefab;
        [BoxGroup("Visual Prefabs")] [AssetsOnly] [SerializeField] private Sprite m_soldierIcon;
        
        [BoxGroup("Data")] [AssetsOnly] [SerializeField] private SpawnerVisualType m_spriteType;
        [BoxGroup("Data")] [AssetsOnly] [SerializeField] private SoldierData m_soldierData;
        [BoxGroup("Data")] [SerializeField] private int m_soldierSpawnCount = 0;
        [BoxGroup("Data")] [SerializeField] private int m_cost = 0;

        public SoldierData SoldierData => m_soldierData;

        public Sprite SoldierIcon => m_soldierIcon;

        public string SpawnerName => m_spawnerName;

        public string SpawnerDescription => m_spawnerDescription;

        public GameObject SpawnerSpritePrefab => m_spawnerSpritePrefab;
        public GameObject SpawnerSpinePrefab => m_spawnerSpinePrefab;

        public int Cost => m_cost;

        public int ID => m_iD;

        public SpawnerVisualType VisualType => m_spriteType;
        
        public int SoldierSpawnCount => m_soldierSpawnCount;

        public enum SpawnerVisualType
        {
            Spine,
            Sprite
        }
    }
}
