using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Weapons;

public class WeaponSlots : MonoBehaviour
{
    [Title("Sltos")]
    [SerializeField] private WeaponSlot[] m_slots;
    
    [Title("Active Weapons")]
    [SerializeField] private WeaponSlot m_activeMainWeapon;
    [SerializeField] private WeaponSlot m_activeOffhandWeapon;
    
    private Queue<WeaponSlot> m_weaponQueue;


    private void Awake()
    {
        InitializeData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FiringLogic();
    }

    private void InitializeData()
    {
        m_weaponQueue = new Queue<WeaponSlot>();
    }

    private void FiringLogic()
    {
        if (Input.GetMouseButtonDown(0) && m_activeMainWeapon != null)
        {
            
            if (!m_activeMainWeapon.gameObject.activeInHierarchy)
            {
                m_activeMainWeapon.gameObject.SetActive(true);
                m_activeOffhandWeapon.gameObject.SetActive(false);
            }
            
            if (m_activeMainWeapon.FireWeaponInSlot()) return;
            
            m_activeMainWeapon = ResetAndQueue(m_activeMainWeapon);
        }
        else if(Input.GetMouseButtonDown(1) && m_activeOffhandWeapon != null)
        {
            
            if (!m_activeOffhandWeapon.gameObject.activeInHierarchy)
            {
                m_activeOffhandWeapon.gameObject.SetActive(true);
                m_activeMainWeapon.gameObject.SetActive(false);
            }
            
            if (m_activeOffhandWeapon.FireWeaponInSlot()) return;

            m_activeOffhandWeapon = ResetAndQueue(m_activeOffhandWeapon);
        }
    }

    private WeaponSlot ResetAndQueue(WeaponSlot p_slot)
    {
        
        p_slot.Weapon.ResetAmmoCount();
        p_slot.gameObject.SetActive(false);
        m_weaponQueue.Enqueue(p_slot);
        p_slot = null;
        p_slot = m_weaponQueue.Dequeue();
        p_slot.gameObject.SetActive(true);

        return p_slot;
    }

    public void AddWeapon(GameObject p_weapon)
    {
        GameObject newWeapon;
        
        foreach (var weapon in m_slots)
        {
            if (weapon.Weapon == null)
            {
                newWeapon = Instantiate(p_weapon, Vector3.zero, Quaternion.identity,
                    weapon.transform);
                
                //we set the newly created weapon to its empty slot
                weapon.SetWeapon(newWeapon);
                newWeapon.transform.localPosition = Vector3.zero;

                //if we dont have any active weapon
                if (m_activeMainWeapon == null)
                {
                    m_activeMainWeapon = weapon;
                    m_activeMainWeapon.gameObject.SetActive(true);
                }
                else
                {
                    //if the offhand is currenly null
                    if (m_activeOffhandWeapon == null)
                    {
                        m_activeOffhandWeapon = weapon;
                    }
                    //if an offhand exists then add the next weapon to the queue
                    else
                    {
                        m_weaponQueue.Enqueue(weapon);
                    }
                }

                return;
            }
            else
            {
                //if the weapon si already in the queue then we just reset its ammo
                if (p_weapon.GetComponent<Weapon>().CompareWeapons(weapon.Weapon))
                {
                    Debug.LogWarning("Gun Already in Queue");
                    weapon.Weapon.ResetAmmoCount();
                    
                    return;
                }
            }
        }
        
        
        //if we dont have an active gun at this point there is an error
        if (m_activeMainWeapon == null)
        {
            Debug.LogError("At This Point It Shouldnt Be Null");
            return;
        }
        
        //create a new weapon and set it to the current weapon
        
        newWeapon = Instantiate(p_weapon, Vector3.zero, Quaternion.identity,
            m_activeMainWeapon.transform);
                
        newWeapon.transform.localPosition = Vector3.zero;
                
        m_activeMainWeapon.SetWeapon(newWeapon);
    }
}