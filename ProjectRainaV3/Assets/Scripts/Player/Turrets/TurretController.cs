using System;
using System.Collections.Generic;
using Player.Soldiers.Data;
using Player.Soldiers.Data.Modifiers;
using Player.Turrets.Modifiers;
using Player.UI;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Player.Turrets
{
    public class TurretController : MonoBehaviour
    {
        
        [SerializeField] private LineRenderer m_lineRenderer;
        [SerializeField] private Stats m_stats;
        [SerializeField] private BoxCollider2D m_boxCollider2D;

        private TurretSpineController m_turretSpineController;
        private SoldierData m_soldierData;


        #region  Untiy Functions

        private void Awake()
        {
            BindComponents();
            InitializeTurret();
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeData();
            InitializedRange();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        #endregion


        #region Initialization Of Turret
        
        private void InitializeTurret()
        {
            m_boxCollider2D.enabled = true;
            m_turretSpineController.SetIdle();
        }
        
        private void InitializedRange()
        {
            if (m_lineRenderer == null) return;
            
            var points = new Vector3[2];
            points[1].x += m_stats.AttackRange;

            m_lineRenderer.SetPositions(points);
        }


        private void BindComponents()
        {
            if (m_turretSpineController == null)
                m_turretSpineController = GetComponent<TurretSpineController>();

            if (m_lineRenderer == null)
            {
                m_lineRenderer = GetComponentInChildren<LineRenderer>();
                m_lineRenderer.gameObject.SetActive(false);
            }

            if (m_boxCollider2D == null)
                m_boxCollider2D = GetComponent<BoxCollider2D>();
        }


        private void InitializeData()
        {
            ResultSlotController.Instance.RequestData(out m_stats, out m_soldierData);
            InitializeModifiers();
        }


        private void InitializeModifiers()
        {
            foreach (var mod in m_soldierData.ActiveModifiers)
            {
                switch (mod)
                {
                    case Modifier.Pierce:
                        var pm = gameObject.AddComponent<PierceModifier>();
                        pm.InitializePierce(m_soldierData.PierceModifierData);
                        break;
                    case Modifier.Dod:
                        var dodData = gameObject.AddComponent<DodModifier>();
                        dodData.InitializeDod(m_soldierData.DamageOverDistanceModifierData);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        #endregion
        
        
        #region Pointer Functions

        private void OnMouseEnter()
        {
            if(m_lineRenderer)
                m_lineRenderer.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            if(m_lineRenderer)
                m_lineRenderer.gameObject.SetActive(false);
        }

        #endregion
    }
}
