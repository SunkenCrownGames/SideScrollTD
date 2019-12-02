using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AngieTools.UI
{
    public class UIMonoBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {

        [SerializeField]
        private bool m_debugPointers = false;

        #region Default Pointer Functions

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (m_debugPointers)
            {
                Debug.Log("Pointer Click UI");
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (m_debugPointers)
            {
                Debug.Log("Pointer Down UI");
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (m_debugPointers)
            {
                Debug.Log("Pointer Entered UI");
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (m_debugPointers)
            {
                Debug.Log("Pointer Exit UI");
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (m_debugPointers)
            {
                Debug.Log("Pointer Up UI");
            }
        }

        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}