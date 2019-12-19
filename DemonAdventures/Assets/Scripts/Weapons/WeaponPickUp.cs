using UnityEngine;

namespace Weapons
{
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField] private GameObject m_weaponPrefab;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public GameObject WeaponPrefab => m_weaponPrefab;
    }
}
