using System.Collections;
using System.Collections.Generic;
using Player.Turrets;
using UnityEngine;

namespace V2.Data
{
    [CreateAssetMenu(fileName = "TurretDatabase", menuName = "DataObjects/Turret/Turret Database", order = 2)]
    public class TurretDatabase : ScriptableObject
    {
        [SerializeField]
        private List<TurretData> m_turretData = null;



        public List<TurretData> Data
        {
            get { return m_turretData; }
        }
    }
}
