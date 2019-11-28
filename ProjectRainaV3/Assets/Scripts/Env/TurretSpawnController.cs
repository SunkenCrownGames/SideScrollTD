using System;
using AngieTools.Effects.Fade;
using Managers;
using Player.MergedTurret.Data;
using Player.Selection;
using Player.Turrets;
using Player.UI;
using UnityEditor.Animations;
using UnityEngine;

namespace Env
{
    public class TurretSpawnController : MonoBehaviour
    {
        [SerializeField] private bool m_status = false;
        [SerializeField] private TurretController m_turretReference;
        [SerializeField] private GameObject m_whiteOnObject = null;

        private BoxCollider2D m_boxCollider2D;
        private TurretLaneController m_laneController = null;
        private SpriteRenderer m_spriteRenderer;
        private Animator m_animator;
        private AnimatorState m_currentState;

        #region Unity Functions
        private void Awake()
        {
            BindComponents();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            
        }
        #endregion
        
        #region Pointer Functions
        
        private void OnMouseEnter()
        {
            Debug.Log("Mouse entered");
            EnterState();
            
        }

        private void OnMouseExit()
        {
            ExitState();
        }

        private void OnMouseDown()
        {
            ClickState();
        }

        #endregion

        #region Mouse States
        /// <summary>
        /// Will Call the lane controller to update the state of the lane to true when the mouse enters any of the slots
        /// </summary>
        private void EnterState()
        {
            if (SelectionPrefabsController.Instance.ActiveSelection == null || m_status) return;

            m_laneController.ToggleSpawnControllers(true);

            
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.transform.position =
                transform.position;
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(true);
        }

        /// <summary>
        /// Will Call the lane controller to udpate the state of the lanes to false when the mouse enter any of the slots
        /// </summary>
        private void ExitState()
        {
            if (SelectionPrefabsController.Instance.ActiveSelection == null) return;

            m_laneController.ToggleSpawnControllers(false);

            Debug.Log("Mouse exited");
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(false);
        }

        private void ClickState()
        {
            if (SelectionPrefabsController.Instance.ActiveSelection == null || m_status) return;

            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(false);
            
            m_status = true;
            var newTurret = Instantiate(ResultSlotController.Instance.MergedData.Prefab, transform.position,
                Quaternion.identity, ParentManager.Instance.TurretParent);

            var tc = newTurret.AddComponent<TurretController>();

            m_turretReference = tc;

            UpdateSpawnBoxCollider();
            SlotToggle();
        }
        #endregion

        private void BindComponents()
        {
            m_laneController = GetComponentInParent<TurretLaneController>();
            m_spriteRenderer = m_whiteOnObject.GetComponent<SpriteRenderer>();
            m_animator = m_whiteOnObject.GetComponent<Animator>();
            m_boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void SlotToggle()
        {
            m_whiteOnObject.SetActive(!m_status);
        }

        public void UpdateSpawnBoxCollider()
        {
            m_boxCollider2D.enabled = (m_turretReference == null) ? true : false;
        }
        

        public void SetState(bool p_toggle)
        {
            if (!p_toggle || m_status)
            {
                m_spriteRenderer.enabled = false;
            }
            else
            {
                m_spriteRenderer.enabled = true;
            }
        }
    }
}
