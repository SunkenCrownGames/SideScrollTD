using System;
using Sirenix.OdinInspector;
using TMPro;
using Turrets;
using UnityEngine;

namespace Player
{
    public class TurretInfoUi : MonoBehaviour
    {

        private static TurretInfoUi _instance;
        
        [Title("UI Components")] 
        [SceneObjectsOnly] [SerializeField] private TextMeshProUGUI m_turretName;
        [SceneObjectsOnly] [SerializeField] private TextMeshProUGUI m_turretDescription;
        [Title("Stat Components")]
        [SceneObjectsOnly] [SerializeField] private StatUi m_damage;
        [SceneObjectsOnly] [SerializeField] private StatUi m_speed;
        [SceneObjectsOnly] [SerializeField] private StatUi m_range;


        private void Awake()
        {
            BindInstance();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void BindInstance()
        {
            if(_instance != null) Destroy(gameObject);

            _instance = this;
            gameObject.SetActive(false);
        }

        public void UpdateTurretInfo(TurretData p_data)
        {
            m_turretName.text = p_data.TurretName;
            m_turretDescription.text = p_data.TurretDescription;
            m_damage.UpdateStat($"Attack Damage: { p_data?.TurretStats?.AttackDamage.ToString() } ");
            m_speed.UpdateStat($"Attack Speed: { p_data?.TurretStats?.AttackSpeed.ToString() } ");
            m_range.UpdateStat($"Attack Range: { p_data?.TurretStats?.Range.ToString() } ");
        }

        public static void UpdateTurret(TurretData p_data, float p_xValue)
        {
            if (_instance == null) return;

            var transform1 = _instance.transform;
            var position = transform1.position;
            position.x = p_xValue;
            transform1.position = position;
            _instance.UpdateTurretInfo(p_data);
            _instance.gameObject.SetActive(true);
        }

        public static void DisableTurret()
        {
            if (_instance == null) return;
            
            _instance.gameObject.SetActive(false);
        }
        
    }
}
