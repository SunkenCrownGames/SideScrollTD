using System.Collections;
using System.Collections.Generic;
using Player.Soldiers.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace V2.Data
{
    [CreateAssetMenu(fileName = "SoldierDatabase", menuName = "DataObjects/Soldier/Soldier Database", order = 2)]
    public class SoldierDatabase : ScriptableObject
    {
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "m_name")]
        [SerializeField] private List<SoldierData> m_data = null;



        public List<SoldierData> Data => m_data;
    }
}