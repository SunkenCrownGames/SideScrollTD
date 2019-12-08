using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AngieTools.GameObjectTools
{
    public class PhysicsObject : MonoBehaviour
    {
        public float m_minGroundNormalY = .65f;
        public float m_gravityModifier = 1f;

        protected Vector2 m_targetVelocity;
        protected bool m_grounded;
        protected Vector2 m_groundNormal;
        protected Rigidbody2D m_rb2d;
        protected Vector2 m_velocity;
        protected ContactFilter2D m_contactFilter;
        protected readonly RaycastHit2D[] m_hitBuffer = new RaycastHit2D[16];
        protected readonly List<RaycastHit2D> m_hitBufferList = new List<RaycastHit2D> (16);


        protected const float MinMoveDistance = 0.001f;
        protected const float ShellRadius = 0.01f;

        void OnEnable()
        {
            m_rb2d = GetComponent<Rigidbody2D> ();
        }

        void Start () 
        {
            m_contactFilter.useTriggers = false;
            m_contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
            m_contactFilter.useLayerMask = true;
        }

        void Update () 
        {
            m_targetVelocity = Vector2.zero;
            ComputeVelocity ();    
        }

        protected virtual void ComputeVelocity()
        {

        }

        void FixedUpdate()
        {
            PhysicsLogic();
        }

        private void PhysicsLogic()
        {
            m_velocity += m_gravityModifier * Time.deltaTime * Physics2D.gravity;
            m_velocity.x = m_targetVelocity.x;

            m_grounded = false;

            Vector2 deltaPosition = m_velocity * Time.deltaTime;

            Vector2 moveAlongGround = new Vector2 (m_groundNormal.y, -m_groundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement (move, false);

            move = Vector2.up * deltaPosition.y;

            Movement (move, true);
        }

        void Movement(Vector2 p_move, bool p_yMovement)
        {
            var distance = p_move.magnitude;

            if (distance > MinMoveDistance) 
            {
                var count = m_rb2d.Cast (p_move, m_contactFilter, m_hitBuffer, distance + ShellRadius);
                m_hitBufferList.Clear ();
                for (var i = 0; i < count; i++) {
                    m_hitBufferList.Add (m_hitBuffer [i]);
                }

                for (var i = 0; i < m_hitBufferList.Count; i++) 
                {
                    Vector2 currentNormal = m_hitBufferList [i].normal;
                    if (currentNormal.y > m_minGroundNormalY) 
                    {
                        m_grounded = true;
                        if (p_yMovement) 
                        {
                            m_groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot (m_velocity, currentNormal);
                    if (projection < 0) 
                    {
                        m_velocity = m_velocity - projection * currentNormal;
                    }

                    float modifiedDistance = m_hitBufferList [i].distance - ShellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }


            }

            m_rb2d.position = m_rb2d.position + p_move.normalized * distance;
        }
    }
}
