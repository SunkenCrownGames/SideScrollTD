using System;
using System.Collections.Generic;
using Player.Soldiers.UI;
using Player.Turrets;
using Player.Turrets.Data;
using Player.Turrets.UI;
using UnityEngine;
using V2.Data;

namespace Player
{
    public class CraftingMenu : MonoBehaviour
    {
        private static CraftingMenu _instance;
        
        [Header("Locked Sprites")]
        [SerializeField] private Sprite m_turretLockedIcon = null;
        [SerializeField] private Sprite m_soldierLockedIcon = null;
        
        [Header("Slot Lists")]
        [SerializeField] private List<TurretSlotUiController> m_turretSlots = null;
        [SerializeField] private List<SoldierUiSlotController> m_soldierSlots = null;

        private void Awake()
        {
            BindInstance();
        }

        private void BindInstance()
        {
            if(_instance != null) Destroy(gameObject);

            _instance = this;
        }

        public void UpdateTurrets(List<TurretData> p_data)
        {
            for (var i = 0; i < m_turretSlots.Count; i++)
            {
                if (i < p_data.Count)
                {
                    m_turretSlots[i].UpdateTurretInfo(p_data[i]);
                }
            }
        }

        public void UpdateSoldiers(List<SoldierData> p_data)
        {
            for (var i = 0; i < m_soldierSlots.Count; i++)
            {
                if (i < p_data.Count)
                {
                    m_soldierSlots[i].UpdateSoldierInfo(p_data[i]);
                }
            }
        }

        public void UpdateTurretsTutorialLock(int p_lockCount)
        {
            for (var i = 0; i < m_turretSlots.Count; i++)
            {
                m_turretSlots[i].UpdateLockState(i >= p_lockCount, m_turretLockedIcon);
            }
        }

        public void UpdateSoldiersTutorialLock(int p_lockCount)
        {
            for (var i = 0; i < m_soldierSlots.Count; i++)
            {
                m_soldierSlots[i].UpdateLockState(i >= p_lockCount, m_soldierLockedIcon);
            }
        }

        public static CraftingMenu Instance => _instance;
    }
}
