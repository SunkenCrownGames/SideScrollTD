using AngieTools.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AngieTools.Tools.UI
{
    public sealed class EaseInUi : UIMonoBehaviour
    {
        [Header("Ease In Control")]
        [SerializeField]
        private EaseType m_easeControlType = EaseType.Hover;
        [SerializeField]
        private Direction m_easeDirection = Direction.BOTTOM;
        [SerializeField]
        private Button m_easeButton = null;
        [SerializeField]
        private TextMeshProUGUI m_easeButtonText = null;
        [SerializeField] private float m_dragOffset = 0;
        [SerializeField] private float m_dragSpeed = 0;

        private float m_initialPosition = 0;
        private float m_dragDestination = 0;

        private readonly float m_lerpBias = 0.2f;
        private bool m_drag = false;


        private void Awake()
        {
            BindComponents();
        }

        private void Start()
        {

        }

        private void Update()
        {
            DragControl();
        }


        private void DragControl()
        {
            if (m_drag)
            {
                DragIn();
            }
            else
            {
                DragOut();
            }
        }

        private void BindComponents()
        {
            if(m_easeDirection == Direction.LEFT || m_easeDirection == Direction.RIGHT)
                m_initialPosition = transform.position.x;
            else
                m_initialPosition = transform.position.y;

            m_dragDestination = m_initialPosition + m_dragOffset;
        }

        private void DragOut()
        {
            if (m_easeDirection == Direction.LEFT || m_easeDirection == Direction.RIGHT)
            {
                if (!(transform.position.x + m_lerpBias > m_initialPosition)) return;
                
                
                var position = transform.position;
                var newPosition = position;

                newPosition.x = Mathf.Lerp(position.x, m_initialPosition, Time.deltaTime * m_dragSpeed);

                position = newPosition;
                transform.position = position;
            }
            else
            {
                if (!(transform.position.y + m_lerpBias > m_initialPosition)) return;
                
                var position = transform.position;
                var newPosition = position;
                newPosition.y = Mathf.Lerp(position.y, m_initialPosition, Time.deltaTime * m_dragSpeed);
                position = newPosition;
                transform.position = position;
            }

        }

        private void DragIn()
        {
            if (m_easeDirection == Direction.LEFT || m_easeDirection == Direction.RIGHT)
            {
                if (!(transform.position.x + m_lerpBias < m_dragDestination)) return;
                
                var position = transform.position;
                var newPosition = position;
                newPosition.x = Mathf.Lerp(position.x, m_dragDestination, Time.deltaTime * m_dragSpeed);
                position = newPosition;
                transform.position = position;
            }
            else
            {
                if (!(transform.position.y + m_lerpBias < m_dragDestination)) return;
                
                var position = transform.position;
                var newPosition = position;
                newPosition.y = Mathf.Lerp(position.y, m_dragDestination, Time.deltaTime * m_dragSpeed);
                position = newPosition;
                transform.position = position;
            }
        }
        

        public void ButtonControl()
        {
            if (m_easeButton == null) return;
            
            if(m_easeButtonText == null)
            {
                m_easeButtonText = m_easeButton.GetComponent<TextMeshProUGUI>();
            }

            if(!m_drag)
            {
                m_easeButtonText.text = "Close";
                m_drag = true;
            }
            else
            {
                m_easeButtonText.text = "Craft";
                m_drag = false;
            }
        }

        private void StartDrag()
        {
            if(m_easeControlType == EaseType.Button)
            {
                m_drag = true;
            }
        }

        private void EndDrag()
        {
            if(m_easeControlType == EaseType.Button)
            {
                m_drag = false;
            }
        }

        public override void OnPointerEnter(PointerEventData p_eventData)
        {
            if (m_easeControlType == EaseType.Hover)
            {
                m_drag = true;
            }
        }

        public override void OnPointerExit(PointerEventData p_eventData)
        {
            if (m_easeControlType == EaseType.Hover)
            {
                m_drag = false;
            }
        }
    }



    public enum EaseType
    {
        Button,
        Hover
    }

}
