using System;
using UnityEngine;

namespace Managers
{
    public class ParentManager : MonoBehaviour
    {
        [SerializeField] Transform m_turretParent = null;
        private void Awake()
        {
            BindComponents();
        }

        private void BindComponents()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }

        public static ParentManager Instance { get; private set; } = null;

        public Transform TurretParent => m_turretParent;
    }
}
