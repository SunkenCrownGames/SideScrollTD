using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyPathControlller : MonoBehaviour
    {
        [SerializeField] protected bool m_debug;

        [ShowIfGroup("m_debug")] [BoxGroup("m_debug/Debug Data")] [SerializeField] private float m_debugMovementSpeed;
        [ShowIfGroup("m_debug")] [BoxGroup("m_debug/Debug Data")] [SerializeField] private Vector2 m_debugJumpStrength;
        [ShowIfGroup("m_debug")] [BoxGroup("m_debug/Debug Data")] [SerializeField] private Vector2 m_debugLeapStrength;
        
        [SerializeField] private PathDirection m_currentPathDirection;
        [SerializeField] protected Rigidbody2D m_rb;
        private EnemyController m_enemyController;

        private float m_movementSpeed = 0;
        private Vector2 m_jumpStrength = Vector2.zero;
        private Vector2 m_leapStrength = Vector2.zero;
        private bool m_transitioned;

        private void Awake()
        {
            BindComponents();
        }


        // Start is called before the first frame update
        private void Start()
        {
            InitializeData();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        #region  Collision
        
        private void OnTriggerEnter2D(Collider2D p_other)
        {
            if (p_other.CompareTag("EDZ") )
            {
                Debug.Log(p_other.name);
                Debug.Log("Entered Decision Zone");

                switch (m_currentPathDirection)
                {
                    case PathDirection.Top:
                        Jump();
                        break;
                    case PathDirection.Mid:
                        MidLogic();
                        break;
                    case PathDirection.Bot:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            else if (p_other.CompareTag($"EnemyDropZone") && (EnvManager.MidCollapsed && m_currentPathDirection == PathDirection.Bot))
            {
                StopMovement();
            }
            
            else if (p_other.CompareTag($"EnemyLandZone"))
            {
                Move();
            }
        }

        private void OnCollisionEnter2D(Collision2D p_other)
        {
            if (p_other.gameObject.CompareTag($"Ground") && m_transitioned)
            {
                Move();
                m_transitioned = false;
                Debug.Log("stopping movement");
            }
        }
        
                

        #endregion

        private void BindComponents()
        {
            if (m_rb == null)
                m_rb = GetComponent<Rigidbody2D>();
        }

        private void InitializeData()
        {
            m_movementSpeed = m_debug ? m_debugMovementSpeed : 0;
            m_leapStrength = m_debug ?  m_debugLeapStrength : Vector2.zero;
            m_jumpStrength = m_debug ? m_debugJumpStrength : Vector2.zero;
            
            Move();
        }
        
        
        private void Jump()
        {
            m_transitioned = true;
            m_rb.AddForce(m_jumpStrength);
        }

        private void MidLogic()
        {
            if(EnvManager.MidCollapsed)
            {
                m_transitioned = true;
                m_rb.AddForce(m_leapStrength);
            }
        }
        
        public void InitializePathing(EnemyController p_enemyController)
        {
            m_enemyController = p_enemyController;
        }


        public void Move()
        {
            m_rb.velocity = new Vector2(-m_movementSpeed, 0);
        }

        public void StopMovement()
        {
            m_rb.velocity = Vector2.zero;
        }
    }


    public enum PathDirection
    {
        Top,
        Mid,
        Bot
    }
}
