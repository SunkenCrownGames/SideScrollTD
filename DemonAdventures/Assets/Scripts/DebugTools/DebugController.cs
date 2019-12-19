using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace DebugTools
{
    public class DebugController : MonoBehaviour
    {
        [SerializeField] [Required] private Rigidbody m_rigidbody;
        [SerializeField] [Required] private CapsuleCollider m_capsuleCollider;
        [SerializeField] private float m_movementSpeed;
        [SerializeField] private Vector3 m_movementVector;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            UpdateMovementVector();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void UpdateMovementVector()
        {
            m_movementVector.x = Input.GetAxisRaw("Horizontal");
            m_movementVector.z = Input.GetAxisRaw("Vertical");
        }

        private void Move()
        {
            switch (m_movementVector.x)
            {
                //Going Right
                case -1:
                    m_rigidbody.position += Vector3.left * (Time.deltaTime * m_movementSpeed);
                    break;
                case 1:
                    m_rigidbody.position += Vector3.right * (Time.deltaTime * m_movementSpeed);
                    break;
            }

            switch (m_movementVector.z)
            {
                //Going up
                case 1:
                    m_rigidbody.position += Vector3.forward * (Time.deltaTime * m_movementSpeed);
                    break;
                case -1:
                    m_rigidbody.position += Vector3.back * (Time.deltaTime * m_movementSpeed);
                    break;
            }
        }
    }
}
