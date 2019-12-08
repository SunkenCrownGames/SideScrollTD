using System;
using System.Collections.Generic;
using System.Linq;
using AngieTools;
using AngieTools.V2Tools.Pathing.Dijkstra;
using Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy
{
    public class EnemyTest : MonoBehaviour
    {
        [SerializeField] private EnemyState m_state;

        [Title("Components")] 
        [SerializeField] private Rigidbody2D m_rb;
        [SerializeField] private BoxCollider2D m_bc;

        [Title("Data")] [SerializeField] private float m_movementSpeed;
        [SerializeField] private int m_layerId = 0;
        [SerializeField] private float m_enteredLadderOffset;
        
        [Title("Debug Platforms")]
        [SerializeField] private Platform m_currentPlatform;
        [SerializeField] private Platform m_nextPlatform;
        [SerializeField] private Platform m_destinationPlatform;
        [SerializeField] private Stack<Platform> m_path;
        
        [Title("Debug State")]
        [SerializeField] private bool m_useLadder;

        [SerializeField] private Ladder m_ladder = null;
        [SerializeField] private Direction m_ladderDirection =  Direction.TOP;

        private int m_defaultLayer = 0;
        
        private void Awake()
        {
            m_defaultLayer = gameObject.layer;
        }

        private void Start()
        {
            m_destinationPlatform = PathingManager.RandomPlatform();
        }

        private void FixedUpdate()
        {
            MovementLogic();
        }

        private void OnCollisionEnter2D(Collision2D p_other)
        {
            var collidedCollider = p_other.collider;

            if (m_currentPlatform != null) return;
            
            if (collidedCollider.CompareTag("Ground") || collidedCollider.CompareTag("Platform"))
            {
                m_currentPlatform = collidedCollider.GetComponent<Platform>();
            }

            if (m_path == null || m_path.Count == 0)
                m_path = PathingManager.GetPath(m_currentPlatform, m_destinationPlatform);
            
            UpdateNextNode();
        }

        private void UpdateNextNode()
        {
            if (m_currentPlatform != m_destinationPlatform)
            {
                if (m_path.Count > 0)
                {
                    m_nextPlatform = m_path.Pop();

                    if (m_nextPlatform == m_currentPlatform)
                        m_nextPlatform = m_path.Pop();

                    var position = transform.position;
                    var nextPlatformPosition = m_nextPlatform.transform.position;

                    var ladders = (nextPlatformPosition.y > position.y)
                        ? m_nextPlatform.GetComponentsInChildren<Ladder>()
                        : m_currentPlatform.GetComponentsInChildren<Ladder>();

                    m_ladderDirection = (position.y < nextPlatformPosition.y) ? Direction.TOP : Direction.BOTTOM;

                    var ladder = ladders.Where(p_ladder =>
                        p_ladder.BottomNode.Equals(m_nextPlatform) && m_ladderDirection == Direction.BOTTOM ||
                        p_ladder.TopNode.Equals(m_nextPlatform) && m_ladderDirection == Direction.TOP).ToList();


                    if (ladder.Count > 0)
                        m_ladder = ladder[0];
                    else
                    {
                        Debug.Log("No Ladders Found");
                        return;
                    }
                }
                else
                {
                    m_nextPlatform = null;
                    m_destinationPlatform = null;
                    m_state = EnemyState.WalkingToEnemy;
                }
            }
        }

        private void MovementLogic()
        {

            if (m_path != null)
            {
                if (m_path.Count > 0 || m_currentPlatform != m_destinationPlatform) 
                {
                    m_state = EnemyState.PathingToEnemy;
                }
                else
                {
                    m_state = EnemyState.WalkingToEnemy;
                }
            }

            if (m_state == EnemyState.PathingToEnemy)
            {
                if(!m_useLadder)
                    HorizontalPathingLogic();
                else
                    VerticalPathingLogic();
            }
        }

        private void VerticalPathingLogic()
        {
            if (gameObject.layer != m_layerId) gameObject.layer = m_layerId;
            m_rb.gravityScale = 0;

            if (m_ladderDirection == Direction.TOP)
            {
                if (transform.position.y - (m_bc.bounds.extents.y * 2) < m_nextPlatform.transform.position.y)
                {
                    transform.Translate(m_movementSpeed * Time.deltaTime * Vector3.up);
                }
                else
                {
                    m_useLadder = false;
                    m_rb.gravityScale = 1;
                    gameObject.layer = m_defaultLayer;
                    m_currentPlatform = m_nextPlatform;
                    UpdateNextNode();
                }
            }
            else
            {
                if (transform.position.y - (m_bc.bounds.extents.y * 2) > m_nextPlatform.transform.position.y)
                {
                    transform.Translate(m_movementSpeed * Time.deltaTime * Vector3.down);
                }
                else
                {
                    m_useLadder = false;
                    m_rb.gravityScale = 1;
                    gameObject.layer = m_defaultLayer;
                    m_currentPlatform = m_nextPlatform;
                    UpdateNextNode();
                }
            }

        }
        
        private void HorizontalPathingLogic()
        {
            if (m_destinationPlatform != null && m_currentPlatform != null && m_ladder != null)
            {
                if (!m_currentPlatform.Equals(m_destinationPlatform))
                {
                    var movementDirection = (m_ladder.transform.position.x < transform.position.x) ? Direction.LEFT : Direction.RIGHT;

                    float direction = 0;

                    if (movementDirection == Direction.LEFT)
                    {
                        direction = -1;

                        if (transform.position.x + (direction * m_enteredLadderOffset) > m_ladder.transform.position.x)
                        {
                            transform.Translate(Time.deltaTime * m_movementSpeed * Vector3.left);
                        }
                        else
                        {
                            m_useLadder = true;
                        }
                    }
                    else if(movementDirection == Direction.RIGHT)
                    {
                        direction = 1;
                        
                        if (transform.position.x + (direction * m_enteredLadderOffset) < m_ladder.transform.position.x)
                        {
                            transform.Translate(m_movementSpeed * Time.deltaTime * Vector3.right);
                        }
                        else
                        {
                            m_useLadder = true;
                        }
                    }
                }
            }
        }
    }
}
