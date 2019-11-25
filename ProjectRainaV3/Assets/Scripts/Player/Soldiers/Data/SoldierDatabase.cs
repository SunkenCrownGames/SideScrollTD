using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V2.Data
{
    [CreateAssetMenu(fileName = "SoldierDatabase", menuName = "DataObjects/Soldier/Soldier Database", order = 2)]
    public class SoldierDatabase : ScriptableObject
    {
        [SerializeField]
        private List<SoldierData> m_data = null;



        public List<SoldierData> Data => m_data;
    }
}