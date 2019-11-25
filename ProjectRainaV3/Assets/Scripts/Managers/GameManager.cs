using System;
using System.Collections.Generic;
using System.Linq;
using AngieTools.DataStructures;
using AngieTools.Tools.DataStructure;
using Player;
using Player.Soldiers;
using Player.Turrets;
using UnityEngine;
using V2.Data;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header(("Tutorial Data"))]
        [SerializeField] private bool m_tutorialMode = false;
        [Space] [SerializeField] private int m_availableTurretSlots = 0;
        [SerializeField] private int m_availableSoldierSlots = 0;
        [Space] [SerializeField] private List<int> m_turretIds = null;
        [Space] [SerializeField] private List<int> m_soldierIds = null;


        [Header("Debug Data")] 
        [SerializeField] private bool m_debugMode;
        [SerializeField] private int m_availableTurretTracker;
        [SerializeField] private int m_availableSoldiersTracker;
        
        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            TutorialLock();
        }

        private void Update()
        {
            DebugData();   
        }

        private void DebugData()
        {
            if (!m_debugMode) return;

            if (m_availableSoldierSlots != m_availableSoldiersTracker)
            {
                m_availableSoldiersTracker = m_availableSoldierSlots;
                CraftingMenu.Instance.UpdateSoldiersTutorialLock(m_availableSoldierSlots);
            }

            if (m_availableTurretSlots != m_availableTurretTracker)
            {
                m_availableTurretTracker = m_availableTurretSlots;
                CraftingMenu.Instance.UpdateTurretsTutorialLock(m_availableTurretSlots);
            }
        }
        

        private void TutorialLock()
        {
            if (!m_tutorialMode) return;
            
            CraftingMenu.Instance.UpdateTurretsTutorialLock(m_availableTurretSlots);
            CraftingMenu.Instance.UpdateSoldiersTutorialLock(m_availableSoldierSlots);

            var turretDates = m_turretIds.Select(p_id => BinarySearch.StartBinarySearchWithID<TurretData>(CustomList<TurretData>.ToCustomList(DynamicTurretDatabase.Instance.Data), p_id)).ToList();
            
            var soldierDates = m_soldierIds.Select(p_id => BinarySearch.StartBinarySearchWithID<SoldierData>(CustomList<SoldierData>.ToCustomList(DynamicSoldierDatabase.Instance.Data), p_id)).ToList();

            CraftingMenu.Instance.UpdateTurrets(turretDates);
            CraftingMenu.Instance.UpdateSoldiers(soldierDates);
        }

        public int AvailableTurretSlots
        {
            get => m_availableTurretSlots;
            set
            {
                m_availableTurretSlots = value;
                CraftingMenu.Instance.UpdateTurretsTutorialLock(m_availableTurretSlots);
                
            }
        }

        public int AvailableSoldierSlots
        {
            get => m_availableSoldierSlots;
            set
            {
                m_availableSoldierSlots = value;
                CraftingMenu.Instance.UpdateSoldiersTutorialLock(m_availableSoldierSlots);
            }
        }
    }
}
