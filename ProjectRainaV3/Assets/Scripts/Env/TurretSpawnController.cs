using System;
using AngieTools.Effects.Fade;
using Player.Selection;
using UnityEngine;

namespace Env
{
    public class TurretSpawnController : MonoBehaviour
    {
        [SerializeField] private float m_maxAlpha;
        [SerializeField] private FadeDirection m_fadeDirection;
        [Space][SerializeField] private bool m_status;
        [SerializeField] private GameObject m_onObject;
        [SerializeField] private GameObject m_offObject;

        private SpriteRenderer m_onSr;
        private SpriteRenderer m_offSr;

        private TurretLaneController m_laneController = null;

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
            Fade();
        }

        private void BindComponents()
        {
            m_onSr = m_onObject.GetComponent<SpriteRenderer>();
            m_offSr = m_offObject.GetComponent<SpriteRenderer>();
            m_laneController = GetComponentInParent<TurretLaneController>();
        }

        #region Pointer Functions
        
        private void OnMouseEnter()
        {
            Debug.Log("Mouse entered");

            if (SelectionPrefabsController.Instance.ActiveSelection == null) return;

            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.transform.position =
                transform.position;
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(true);
            
            m_laneController.TriggerBoxOn();
        }

        private void OnMouseExit()
        {
            if (SelectionPrefabsController.Instance.ActiveSelection == null) return;

            Debug.Log("Mouse exited");
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(false);

            m_laneController.TriggerBoxOff();
        }

        #endregion
        
        public void ChangeBoxStatus(bool p_status)
        {
            m_status = p_status;
        }

        private void Fade()
        {
            if (m_fadeDirection == FadeDirection.FadeIn)
            {
                var spriteRenderer = !m_status ? m_onSr : m_offSr;
                var color = spriteRenderer.color;

                if (color.a < m_maxAlpha)
                {
                    color.a += Time.deltaTime;
                }
                else
                {
                    color.a = m_maxAlpha;
                    m_fadeDirection = FadeDirection.None;
                }

                spriteRenderer.color = color;
            }
            else if (m_fadeDirection == FadeDirection.FadeOut)
            {
                var spriteRenderer = !m_status ? m_onSr : m_offSr;
                var color = spriteRenderer.color;

                if (color.a > 0)
                {
                    color.a -= Time.deltaTime;
                }
                else
                {
                    color.a = 0;
                    m_fadeDirection = FadeDirection.None;
                }

                spriteRenderer.color = color;
            }
        }

        public void TriggerBox()
        {
            if (!m_status)
            {
                m_offObject.SetActive(false);
                m_onObject.SetActive(true);
            }
            else
            {
                m_offObject.SetActive(true);
                m_onObject.SetActive(false);
            }
        }

        public void TriggerBoxOff()
        {
            m_offObject.SetActive(false);
            m_onObject.SetActive(false);
        }

        public void SetupFade()
        {
            var offColor = m_offSr.color;
            offColor.a = 0;
            var onColor = m_onSr.color;
            onColor.a = 0;
            
            m_offSr.color = offColor;
            m_onSr.color = onColor;
        }

        public void UpdateFadeDirection(FadeDirection p_direction)
        {
            m_fadeDirection = p_direction;
        }

        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public FadeDirection FadeDirection => m_fadeDirection;
    }
}
