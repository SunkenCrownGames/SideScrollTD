using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Sirenix.OdinInspector;
using UnityEngine;
using Weapons.Modifiers;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [Title("Weapon Data")]
        [SerializeField] protected int m_currentAmmoCount;
        [SerializeField] protected WeaponData m_data;

        [Title("Mods Data")]
        [SerializeField] protected ModifierStateData m_modifierStateData;


        [Title("Debug Data")] 
        [ShowInInspector] [ReadOnly] private static Transform m_bulletParent;
        
        private void Awake()
        {
            m_currentAmmoCount = m_data.MaxBulletCount;
            
            if (m_bulletParent == null)
                m_bulletParent = GameObject.FindGameObjectWithTag("Bullets").transform;
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

        public virtual bool Fire()
        {
            m_currentAmmoCount--;
            var bulletObject = Instantiate(m_data.BulletPrefab, transform.position, Quaternion.identity,
                m_bulletParent);

            var bulletDriver = bulletObject.GetComponent<BulletDriver>();
            bulletDriver.SetModifiers(m_data.BounceData, m_data.ExplosionData, m_data.SplitData, m_data.CastMultiplierData);
            
            return m_currentAmmoCount > 0;

        }

        [Button("Test Set Upgrade")]
        public void SetUpgrade(ModifierType p_type, bool p_status)
        {
            switch (p_type)
            {
                case ModifierType.MultipleCast:
                    m_data.CastMultiplierData.SetStatus(p_status);
                    break;
                case ModifierType.Split:
                    m_data.SplitData.SetStatus(p_status);
                    break;
                case ModifierType.Explosion:
                    m_data.ExplosionData.SetStatus(p_status);
                    break;
                case ModifierType.Bounce:
                    m_data.BounceData.SetStatus(p_status);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(p_type), p_type, null);
            }
        }

    }
}
