using AngieTools.UI;
using Sirenix.OdinInspector;
using Spawners.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Spawners.UI
{
    public class SpawnerSlot : UIMonoBehaviour
    {
        [Title("Components")]
        [SceneObjectsOnly] [SerializeField] private Image m_icon;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override void OnPointerClick(PointerEventData p_eventData)
        {
            base.OnPointerClick(p_eventData);

            if (p_eventData.button == PointerEventData.InputButton.Left)
            {
                SpawnerUiController.Instance.UpdateSelectedSlot(this);
            }
        }

        public void UpdateSlot(SpawnerData p_spawnerData)
        {
            if(p_spawnerData == null) gameObject.SetActive(false);

            m_icon.sprite = p_spawnerData.SoldierIcon;
        }
    }
}
