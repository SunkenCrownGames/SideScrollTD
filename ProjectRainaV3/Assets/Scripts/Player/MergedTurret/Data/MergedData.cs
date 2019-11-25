using UnityEngine;

namespace Player.MergedTurret.Data
{
    [System.Serializable]
    public class MergedData
    {
        [SerializeField] private GameObject m_prefab = null;
        [SerializeField] private Sprite m_mergedSprite = null;
        [SerializeField] private string m_name = "";
        [SerializeField] private string m_description = "";
        [SerializeField] private int m_turretId = 0;
        [SerializeField] private int m_soldierId = 0;

        public GameObject Prefab => m_prefab;

        public Sprite MergedSprite => m_mergedSprite;

        public string Name => m_name;

        public string Description => m_description;

        public int TurretId => m_turretId;

        public int SoldierId => m_soldierId;
        
    }
}
