using System;
using Player.Selection;
using UnityEngine;

namespace Env
{
    public class TurretSpawnController : MonoBehaviour
    {
        [SerializeField] private bool m_status;
        [SerializeField] private GameObject m_onObject;
        [SerializeField] private GameObject m_offObject;


        private SpriteRenderer m_onSr;
        private SpriteRenderer m_offSr;

        private void Awake()
        {
            BindComponents();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void BindComponents()
        {
            m_onSr = m_onObject.GetComponent<SpriteRenderer>();
            m_offSr = m_offObject.GetComponent<SpriteRenderer>();
        }

        private void OnMouseEnter()
        {
            Debug.Log("Mouse entered");

            if (SelectionPrefabsController.Instance.ActiveSelection == null) return;

            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.transform.position =
                transform.position;
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            if (SelectionPrefabsController.Instance.ActiveSelection == null) return;

            Debug.Log("Mouse exited");
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(false);
        }

        public void ChangeBoxStatus(bool p_status)
        {
            m_status = p_status;
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
    }
}
