using AngieTools.UI;
using Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Turrets
{
    public class TurretSlotUiController : UIMonoBehaviour
    {
        [Title("UI Components")]
        [Indent] [SceneObjectsOnly] [SerializeField] private Image m_icon = null;
        
        [Indent] [Title("Cost Components")]
        [Indent] [SceneObjectsOnly] [SerializeField] private TextMeshProUGUI m_costLabel = null;
        [Indent] [SceneObjectsOnly] [SerializeField] private GameObject m_costBackground = null;

        [Title("Data Components")] 
        [Indent] [SerializeField] private TurretData m_assignedTurretData = null;

        
        [Title("Debug Data")] 
        public bool m_debugToggle;

        [ShowIf("m_debugToggle")] [BoxGroup] [Indent] [SerializeField] private bool m_selected = false;
        [ShowIf("m_debugToggle")] [BoxGroup] [Indent] [ShowInInspector] private static TurretSlotUiController _currentSlotController = null;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void UpdateData(TurretData p_data)
        {
            m_assignedTurretData = p_data;
            m_icon.sprite = m_assignedTurretData?.TurretIcons?.StandardSprite;
            m_costLabel.text = (m_assignedTurretData == null) ? "-1" : m_assignedTurretData?.Cost.ToString();
            m_costLabel.gameObject.SetActive(true);
            m_costBackground.SetActive(true);
        }

        public void ResetData(Sprite p_disabledSprite)
        {
            m_assignedTurretData = null;
            m_icon.sprite = p_disabledSprite;
            m_costLabel.gameObject.SetActive(false);
            m_costBackground.SetActive(false);
        }

        public override void OnPointerClick(PointerEventData p_eventData)
        {

            base.OnPointerClick(p_eventData);

            if (m_assignedTurretData != null)
            {
                m_selected = true;

                if (_currentSlotController == null)
                {
                    _currentSlotController = this;
                }
                else
                {
                    _currentSlotController.m_selected = false;
                    _currentSlotController = this;
                }
            }
            else
            {
                Debug.Log("Assigned Data is null");
            }
        }

        public override void OnPointerEnter(PointerEventData p_eventData)
        {
            base.OnPointerEnter(p_eventData);
            TurretInfoUi.UpdateTurret(m_assignedTurretData, transform.position.x);
        }

        public override void OnPointerExit(PointerEventData p_eventData)
        {
            base.OnPointerExit(p_eventData);
            
            TurretInfoUi.DisableTurret();
        }
    }
}
