using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WepData", menuName = "Weapons/Weapon Data", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [Title("Identification Data")] 
        [SerializeField] private int m_iD;
        [SerializeField] private string m_name;
        [SerializeField] private string m_description;


        [Title("Bullet Data")] 
        [SerializeField] private int m_maxBulletCount;
        [SerializeField] private GameObject m_bulletPrefab;
        
        
        
        public int Id => m_iD;

        public string Name => m_name;

        public string Description => m_description;
        
        public int MaxBulletCount => m_maxBulletCount;

        public GameObject BulletPrefab => m_bulletPrefab;
    }
}
