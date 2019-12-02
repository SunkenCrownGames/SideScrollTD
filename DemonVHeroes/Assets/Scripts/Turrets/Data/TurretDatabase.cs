using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Turrets
{
    [CreateAssetMenu(fileName = "TurretDatabase", menuName = "DataObjects/Turret/Turret Database", order = 2)]
    public class TurretDatabase : ScriptableObject
    {
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "m_turretName")]
        [SerializeField] private List<TurretData> m_turretData;


        public void UpdateTurretData(List<TurretData> p_turretDataList)
        {
            m_turretData = p_turretDataList;
        }
        
        public List<TurretData> TurretData => m_turretData;
    }
}
