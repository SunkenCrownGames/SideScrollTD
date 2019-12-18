using System;
using Level;
using Sirenix.OdinInspector;
using Soldier.Data;
using UnityEngine;

namespace Soldier
{
    public class SoldierController : MonoBehaviour
    {
        [Title("Component Data")]
        [SerializeField] private Rigidbody2D m_rb;
        [SerializeField] private MeshRenderer m_textRenderer;
        [SerializeField] private SpriteRenderer m_spriteRenderer;

        [Title("Game Data")] 
        [SerializeField] private SoldierData m_soldierData;
        [SerializeField] private float m_direction;
        [SerializeField] private float m_offset;
        
        [Title("Scene Data")]
        [SerializeField] private Platform m_platform;

        [Title("Debug Data")] 
        [SerializeField] private SoldierState m_state = SoldierState.None;
        
        // Start is called before the first frame update
        void Start()
        {
            m_textRenderer.sortingOrder = m_spriteRenderer.sortingOrder + 1;
        }

        // Update is called once per frame
        void Update()
        {
            if(m_state != SoldierState.Attacking)
                PatrolLogic();
            
            if(m_platform != null)
                UpdateState();

        }

        private void UpdateState()
        {
            if (m_platform.EnemiesOnPlatform.Count > 0)
            {
                m_state = SoldierState.Attacking;
            }
            else
            {
                m_state = SoldierState.None;
            }
        }

        private void PatrolLogic()
        {
            if (m_platform != null)
            {
                if (transform.position.x - m_offset < m_platform.PlatformWidth.StartValue && m_state != SoldierState.Right)
                {
                    m_state = SoldierState.Right;
                }
                else if (transform.position.x + m_offset > m_platform.PlatformWidth.EndValue && m_state != SoldierState.Left)
                {
                    m_state = SoldierState.Left;
                }
            }
            
            switch (m_state)
            {
                case SoldierState.Left:
                {
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (m_rb.velocity.x != -1)
                    {
                        m_rb.velocity = new Vector2(-1 * m_soldierData.Stats.MovementSpeed, m_rb.velocity.y);
                    }

                    break;
                }
                case SoldierState.Right:
                {
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (m_rb.velocity.x != 1)
                    {
                        m_rb.velocity = new Vector2(1 * m_soldierData.Stats.MovementSpeed, m_rb.velocity.y);
                    }

                    break;
                }
                case SoldierState.None:
                    break;
                case SoldierState.Attacking:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AttackLogic()
        {
            
        }
        
        private void OnCollisionEnter2D(Collision2D p_other)
        {
            GroundCollision(p_other);
        }

        private void GroundCollision(Collision2D p_other)
        {
            var ground = p_other.collider.gameObject;
            
            if (ground.CompareTag($"Ground") || ground.CompareTag($"Platform"))
            {
                var platform = ground.GetComponent<Platform>();
                if(transform.position.y > ground.transform.position.y + platform.SpriteRenderer.bounds.extents.y)
                    m_platform = ground.GetComponent<Platform>();
            }
        }

        public void SetData(SoldierData p_data, SoldierState p_state)
        {
            m_soldierData = p_data;
            m_state = p_state;
        }

        public Rigidbody2D Rb => m_rb;

        public SpriteRenderer SpriteRenderer => m_spriteRenderer;

        public SoldierData SoldierData => m_soldierData;

        public float Direction => m_direction;

        public float Offset => m_offset;

        public Platform Platform => m_platform;

        public SoldierState State => m_state;
    }
    
    public enum SoldierState
    {
        Left,
        Right,
        None,
        Attacking
    }
}
