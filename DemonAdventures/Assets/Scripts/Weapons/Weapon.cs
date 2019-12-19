using System;
using System.Net;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int m_currentAmmoCount;
        [SerializeField] private WeaponData m_data;


        private void Awake()
        {
            m_currentAmmoCount = m_data.MaxBulletCount;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public WeaponData Data => m_data;

        public bool CompareWeapons(Weapon p_weapon)
        {
            return m_data.Id == p_weapon.Data.Id;
        }

        public void ResetAmmoCount()
        {
            m_currentAmmoCount = m_data.MaxBulletCount;
        }

        public bool Fire()
        {
            m_currentAmmoCount--;
            return m_currentAmmoCount > 0;

        }
        
    }
}
