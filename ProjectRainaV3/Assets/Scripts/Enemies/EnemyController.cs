using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private bool m_debug;
        [SerializeField] private bool m_debugStateData;
        
        [ShowIfGroup("m_debug")] [BoxGroup("m_debug/Debug Data")] [SerializeField] private float m_debugRange;
        
        [ShowIfGroup("m_debugStateData")] [BoxGroup("m_debugStateData/Debug Data")] [SerializeField] private bool m_inRange;
        [ShowIfGroup("m_debugStateData")] [BoxGroup("m_debugStateData/Debug Data")] [SerializeField] private bool m_grounded;
        [ShowIfGroup("m_debugStateData")] [BoxGroup("m_debugStateData/Debug Data")] [SerializeField] private EnemyState m_enemyState;
        
        [SerializeField] private EnemyPathControlller m_enemyPathControlller;


        private float m_range = 0;
        private static int _mask = -1;
        
        // Start is called before the first frame update
        void Start()
        {
            InitializeEnemy();
        }

        private void Update()
        {
            DrawDebugRange();
            FindTurretInRange();
        }


        #region Colliison

                private void OnCollisionEnter2D(Collision2D p_other)
                {
                    if (p_other.gameObject.CompareTag($"Ground"))
                    {
                        m_grounded = true;
                    }
                }

                private void OnCollisionExit(Collision p_other)
                {
                    if (p_other.gameObject.CompareTag($"Ground"))
                    {
                        m_grounded = false;
                    }
                }

                #endregion

        private void InitializeEnemy()
        {
            if (m_enemyPathControlller == null)
            {
                Debug.LogError("Pathing Script Not Attached Destroying this enemy");
                Destroy(gameObject);
            }
            else
            {
                m_enemyPathControlller.InitializePathing(this);
            }
            
            InitializeTurret();
        }

        private void InitializeTurret()
        {
            if(_mask == -1)
                _mask = LayerMask.GetMask($"Turrets");
            
            m_range = m_debug ? m_debugRange : 0;
        }

        private void DrawDebugRange()
        {
            if (!m_debug) return;
            
            Debug.DrawRay(transform.position, Vector2.left * m_range, Color.blue);
        }

        private void FindTurretInRange()
        {

            if (!m_grounded) return;
            
            var hits = Physics2D.RaycastAll(transform.position, Vector2.left, m_range, _mask);

            if (hits.Length == 0) return;
            
            Debug.Log("Found Turret");
            m_enemyPathControlller.StopMovement();
        }
    }
}
