using System.Collections.Generic;
using UnityEngine;
using V2.Data;
using System.Text;
using AngieTools.DataStructures;
using AngieTools.Tools.DataStructure;

namespace Player.Turrets
{
    public class DynamicTurretDatabase : MonoBehaviour
    {
        [SerializeField] private TurretDatabase m_database = null;
        [SerializeField] private bool m_debug = false;

        private StringBuilder m_sb;
        private void Awake()
        {
            if (Instance == null) Destroy(gameObject);
            if (m_sb == null) m_sb = new StringBuilder();
            
            Instance = this;

            m_sb.AppendLine("Dynamic Turret Database Binded");
            
            
            if (m_database == null) return;
            
            CreateList();
            
            
            StatusCheck();
        }
        
        public void StatusCheck()
        {
            if (m_debug)
            {
                Debug.Log(m_sb.ToString());
            }
        }

        private void CreateList()
        {
            if (m_database == null) return;
            
            Data = new List<TurretData>(m_database.Data);
            Data = MergeSort.MergeSortStart<TurretData>(CustomList<TurretData>.ToCustomList(Data));

            m_sb.AppendFormat("Turret List Generated from Database - Count: {0}", Data.Count);
        }


        public DynamicTurretDatabase(TurretDatabase p_oldData)
        {
            Data = new List<TurretData>(p_oldData.Data);


            //Debug.Log("New Dynamic Turret Database Generated From Static Turret Database");
        }

        public DynamicTurretDatabase(List<TurretData> p_oldData)
        {
            Data = new List<TurretData>(p_oldData);

            //Debug.Log("New Dynamic Turret Database Generated Generated From Turret Data List");
        }

        public DynamicTurretDatabase()
        {
            Data = new List<TurretData>();

            //Debug.Log("Brand New Dynamic Turret Database Generated");
        }


        public void UpdateTurretData(List<TurretData> p_newData)
        {
            Data = p_newData;
        }
        
        public List<TurretData> Data { get; private set; }

        public static DynamicTurretDatabase Instance { get; private set; }
    }
}
