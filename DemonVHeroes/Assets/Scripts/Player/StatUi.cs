using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class StatUi : MonoBehaviour
    {
        [Title("UI Components")] 
        [SceneObjectsOnly] [SerializeField] protected Image m_statIcon;
        [SceneObjectsOnly] [SerializeField] private TextMeshProUGUI m_statValue;

        public void UpdateStat(string p_newStat)
        {
            m_statValue.text = p_newStat;
        }
    }
}
