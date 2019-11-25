using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace V2.Data
{
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "DataObjects/Enemy/Enemy Database", order = 2)]
    public class EnemyDatabase : ScriptableObject
    {
        [SerializeField]
        private List<EnemyData> m_enemyData = null;



        public List<EnemyData> Data
        {
            get { return m_enemyData; }
        }
    }
}