using System.Collections.Generic;
using System.Text;
using AngieTools.DataStructures;
using AngieTools.Tools.DataStructure;
using Player.Turrets;
using UnityEngine;
using V2.Data;

namespace Enemies.Data
{
    public class DynamicEnemyDatabase : MonoBehaviour
    {
        [SerializeField] private EnemyDatabase m_database = null;
        [SerializeField] private bool m_debug = false;

        private StringBuilder m_sb;
        private void Awake()
        {
            if (Instance == null) Destroy(gameObject);
            if (m_sb == null) m_sb = new StringBuilder();
            
            Instance = this;

            m_sb.AppendLine("Dynamic Enemy Database Binded");
            
            
            if (m_database == null) return;
            
            CreateList();
            
            
            StatusCheck();
        }

        private void CreateList()
        {
            if (m_database == null) return;
            
            Data = new List<EnemyData>(m_database.Data);
            Data = MergeSort.MergeSortStart<EnemyData>(CustomList<EnemyData>.ToCustomList(Data));

            m_sb.AppendFormat("Enemy List Generated from Database - Count: {0}", Data.Count);
        }

        #region Constructors
        
        public DynamicEnemyDatabase(EnemyDatabase p_oldData)
        {
            Data = new List<EnemyData>(p_oldData.Data);


            //Debug.Log("New Dynamic Turret Database Generated From Static Enemy Database");
        }

        public DynamicEnemyDatabase(List<EnemyData> p_oldData)
        {
            Data = new List<EnemyData>(p_oldData);

            //Debug.Log("New Dynamic Turret Database Generated Generated From Enemy Data List");
        }

        public DynamicEnemyDatabase()
        {
            Data = new List<EnemyData>();

            //Debug.Log("Brand New Dynamic Enemy Database Generated");
        }
        #endregion

        public void StatusCheck()
        {
            if (m_debug)
            {
                Debug.Log(m_sb.ToString());
            }
        }

        public void UpdateEnemyData(List<EnemyData> p_newData)
        {
            Data = p_newData;
        }

        public List<EnemyData> Data { get; private set; }


        public static DynamicEnemyDatabase Instance { get; private set; }
    }
}
