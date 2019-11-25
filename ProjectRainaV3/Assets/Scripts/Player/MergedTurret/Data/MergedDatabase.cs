using System.Collections.Generic;
using UnityEngine;
using V2.Data;

namespace Player.MergedTurret.Data
{
    [CreateAssetMenu(fileName = "MergedDatabase", menuName = "DataObjects/Merged Turret Database", order = 2)]
    public class MergedDatabase : ScriptableObject
    {
        [SerializeField]
        private List<MergedData> m_data = null;



        public List<MergedData> Data => m_data;
    }
}