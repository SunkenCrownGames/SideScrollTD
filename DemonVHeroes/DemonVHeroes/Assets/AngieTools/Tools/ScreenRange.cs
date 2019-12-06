using UnityEngine;

namespace AngieTools.Tools
{
    [System.Serializable]
    public class ScreenRange
    {
        [Range(0, 1)] [SerializeField] private float m_minValue = 0;
        [Range(0, 1)] [SerializeField] private float m_maxValue = 1;

        public float MinValue => m_minValue;
        public float MaxValue => m_maxValue;
    }
}
