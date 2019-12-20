using Sirenix.OdinInspector;
using UnityEngine;
using Weapons.Modifiers;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WepData", menuName = "Weapons/Weapon Data", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [Title("Identification Data")] 
        [SerializeField] private int m_iD;
        [SerializeField] private string m_name;
        [SerializeField] private string m_description;

        [Title("Modifiers")] 
        [SerializeField] private SplitData m_splitData;
        [SerializeField] private BounceData m_bounceData;
        [SerializeField] private ExplosionData m_explosionData;
        [SerializeField] private CastMultiplierData m_castMultiplierData;
        

        [Title("Bullet Data")] 
        [SerializeField] private int m_maxBulletCount;
        [SerializeField] private GameObject m_bulletPrefab;
        
        
        
        public int Id => m_iD;

        public string Name => m_name;

        public string Description => m_description;
        
        public int MaxBulletCount => m_maxBulletCount;

        public int ID => m_iD;

        public SplitData SplitData => m_splitData;

        public BounceData BounceData => m_bounceData;

        public ExplosionData ExplosionData => m_explosionData;

        public CastMultiplierData CastMultiplierData => m_castMultiplierData;

        public GameObject BulletPrefab => m_bulletPrefab;
    }
}
