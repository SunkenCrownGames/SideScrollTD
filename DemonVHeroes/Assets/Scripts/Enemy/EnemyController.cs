using System;
using System.Collections.Generic;
using System.Linq;
using AngieTools;
using AngieTools.V2Tools.Pathing.Dijkstra;
using Level;
using Sirenix.OdinInspector;
using Spawners;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
  [SerializeField] private EnemyState m_state;

        [Title("Components")] 
        [SerializeField] private Rigidbody2D m_rb;
        [SerializeField] private BoxCollider2D m_bc;
        [SerializeField] private EnemySpineController m_esc;
        
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
            
        }

        private void FixedUpdate()
        {
            MovementLogic();
        }

        private void OnCollisionEnter2D(Collision2D p_other)
        {
            var collidedCollider = p_other.collider;

            if (m_currentPlatform != null && m_destinationPlatform == null) return;
            
            if (collidedCollider.CompareTag("Ground") || collidedCollider.CompareTag("Platform") && transform.position.y > collidedCollider.transform.position.y)
            {
                if (m_destinationPlatform != null && m_currentPlatform.Equals(m_destinationPlatform))
                {
                    m_currentPlatform.AddEnemyToPlatform(this);
                }
                
                m_currentPlatform = collidedCollider.GetComponent<Platform>();

                m_path?.Clear();
            }

            if (m_path == null || m_path.Count == 0)
            {
                var destionationNode = SpawnerManager.Instance.GetDestination();

                m_destinationPlatform = destionationNode;
                
                if(destionationNode != null)
                    m_path = PathingManager.GetPath(m_currentPlatform, destionationNode);
                else
                    Debug.Log("No Destination Node Found");
            }

            if(m_path != null)
                UpdateNextNode();
        }

        private void OnCollisionExit2D(Collision2D p_other)
        {
            var collidedCollider = p_other.collider;

            if (collidedCollider.CompareTag("Platform"))
            {
                var platform = collidedCollider.GetComponent<Platform>();

                if (platform.Equals(m_destinationPlatform))
                {
                    platform.RemoveEnemyFromPlatform(this);
                }
            }

        }

        private void UpdateNextNode()
        {
            if (m_currentPlatform != m_destinationPlatform)
            {
                if (m_path.Count > 0)
                {
                    m_nextPlatform = m_path.Pop();

                    if (m_nextPlatform == m_currentPlatform && m_path.Count > 0)
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

                    if (m_currentPlatform.Equals(m_destinationPlatform))
                    {
                        m_path.Clear();
                    }
                }
            }
            else
            {
                m_nextPlatform = null;
                m_state = EnemyState.WalkingToEnemy;
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

            if (m_state == EnemyState.PathingToEnemy && m_path != null)
            {
                if(!m_useLadder)
                    HorizontalPathingLogic();
                else
                    VerticalPathingLogic();
            }
            else
            {
                
            }
        }

        private void VerticalPathingLogic()
        {
            if (gameObject.layer != m_layerId) gameObject.layer = m_layerId;
            
            m_rb.gravityScale = 0;

            if (m_ladderDirection == Direction.TOP)
            {
                if (transform.position.y - (m_bc.bounds.extents.y) < m_nextPlatform.transform.position.y)
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
                if (transform.position.y - (m_bc.bounds.extents.y) > m_nextPlatform.transform.position.y)
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
                m_esc.SetWalk();
                
                if (!m_currentPlatform.Equals(m_destinationPlatform))
                {
                    var movementDirection = Direction.RIGHT;
                    if (m_ladder.transform.position.x < transform.position.x)
                    {
                        m_esc.Flip(false);
                        movementDirection = Direction.LEFT;
                    }
                    else
                    {
                        m_esc.Flip(true);
                        movementDirection = Direction.RIGHT;
                    }

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
                            TriggerLadder();
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
                            TriggerLadder();
                        }
                    }
                }
                else
                {
                    
                }
            }
        }

        private void TriggerLadder()
        {
            m_useLadder = true;
            
            if(m_ladderDirection == Direction.TOP)
                m_esc.SetUpAnimation();
            else
                m_esc.SetDownAnimation();
        }
    }
}
