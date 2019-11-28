using System.Collections.Generic;
using System.Text;
using AngieTools.DataStructures;
using AngieTools.Tools.DataStructure;
using Player.Soldiers.Data;
using Player.Turrets;
using UnityEngine;
using V2.Data;

namespace Player.Soldiers
{
    public class DynamicSoldierDatabase : MonoBehaviour
    {

        [SerializeField] private SoldierDatabase m_database = null;
        [SerializeField] private bool m_debug = false;
        
        private StringBuilder m_sb;
        
        
        private void Awake()
        {
            if (Instance == null) Destroy(gameObject);
            if (m_sb == null) m_sb = new StringBuilder();
            
            Instance = this;

            m_sb.AppendLine("Dynamic Soldier Database Binded");
            
            
            if (m_database == null) return;
            
            CreateList();
            
            
            StatusCheck();
        }
        
        private void CreateList()
        {
            if (m_database == null) return;
            
            Data = new List<SoldierData>(m_database.Data);
            Data = MergeSort.MergeSortStart<SoldierData>(CustomList<SoldierData>.ToCustomList(Data));

            m_sb.AppendFormat("Soldier List Generated from Database - Count: {0}", Data.Count);
        }
        
        public void StatusCheck()
        {
            if (m_debug)
            {
                Debug.Log(m_sb.ToString());
            }
        }

        public void UpdateSoldierData(List<SoldierData> p_newData)
        {
            Data = p_newData;
        }


        #region Constructors

        public DynamicSoldierDatabase(SoldierDatabase p_oldData)
        {
            Data = new List<SoldierData>(p_oldData.Data);


            //Debug.Log("New Dynamic Turret Database Generated From Static Soldier Database");
        }

        public DynamicSoldierDatabase(List<SoldierData> p_oldData)
        {
            Data = new List<SoldierData>(p_oldData);

            //Debug.Log("New Dynamic Turret Database Generated Generated From Soldier Data List");
        }

        public DynamicSoldierDatabase()
        {
            Data = new List<SoldierData>();

            //Debug.Log("Brand New Dynamic Soldier Database Generated");
        }
        
        
        #endregion
        
        public List<SoldierData> Data { get; private set; }
        
        public static DynamicSoldierDatabase Instance { get; private set; }
    }
}
