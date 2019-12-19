using UnityEngine;

namespace Weapons
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private Weapon m_weapon;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public bool FireWeaponInSlot()
        {
            return Weapon.Fire();
        }


        public void SetWeapon(GameObject p_weapon)
        {
            if (m_weapon != null)
            {
                Destroy(m_weapon.gameObject);
            }
            
            m_weapon = p_weapon.GetComponent<Weapon>();
        }

        public Weapon Weapon => m_weapon;
    }
}
