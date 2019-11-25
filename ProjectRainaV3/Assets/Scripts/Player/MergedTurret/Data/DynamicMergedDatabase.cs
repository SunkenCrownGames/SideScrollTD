using System.Collections.Generic;
using System.Text;
using Player.Soldiers;
using UnityEngine;
using V2.Data;

namespace Player.MergedTurret.Data
{
    public class DynamicMergedDatabase : MonoBehaviour
    {

        [SerializeField] private MergedDatabase m_database = null;
        [SerializeField] private bool m_debug = false;
        
        private StringBuilder m_sb;
        
        
        private void Awake()
        {
            if (Instance == null) Destroy(gameObject);
            if (m_sb == null) m_sb = new StringBuilder();
            
            Instance = this;

            m_sb.AppendLine("Dynamic Merged Database Binded");
            
            
            if (m_database == null) return;
            
            CreateList();
            
            
            StatusCheck();
        }
        
        private void CreateList()
        {
            if (m_database == null) return;
            
            Data = new List<MergedData>(m_database.Data);
        
            m_sb.AppendFormat("Merged List Generated from Database - Count: {0}", Data.Count);
        }
        
        public void StatusCheck()
        {
            if (m_debug)
            {
                Debug.Log(m_sb.ToString());
            }
        }

        public void UpdateSoldierData(List<MergedData> p_newData)
        {
            Data = p_newData;
        }


        #region Constructors

        public DynamicMergedDatabase(MergedDatabase p_oldData)
        {
            Data = new List<MergedData>(p_oldData.Data);


            //Debug.Log("New Dynamic Turret Database Generated From Static Soldier Database");
        }

        public DynamicMergedDatabase(List<MergedData> p_oldData)
        {
            Data = new List<MergedData>(p_oldData);

            //Debug.Log("New Dynamic Turret Database Generated Generated From Soldier Data List");
        }

        public DynamicMergedDatabase()
        {
            Data = new List<MergedData>();

            //Debug.Log("Brand New Dynamic Soldier Database Generated");
        }
        
        
        #endregion
        
        public List<MergedData> Data { get; private set; }
        
        public static DynamicMergedDatabase Instance { get; private set; }
    }
}
