using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Weapons;

namespace Player
{
    public class WeaponPickUpController : MonoBehaviour
    {
        [SerializeField] [Required] private WeaponSlots m_weaponSlots;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider p_other)
        {
            if (p_other.CompareTag($"Weapon"))
            {
                var pickup = p_other.GetComponent<WeaponPickUp>();
                m_weaponSlots.AddWeapon(pickup.WeaponPrefab);
                Destroy(p_other.gameObject);
            }
        }
    }
}
