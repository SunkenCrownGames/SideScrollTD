using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngieTools
{
    [System.Serializable]
    public class Range
    {
        [SerializeField]
        private float m_startValue;

        [SerializeField]
        private float m_endValue;

        public Range()
        {
            m_startValue = 0;
            m_endValue = 0;
        }

        public Range(Range p_otherRange)
        {
            m_startValue = p_otherRange.m_startValue;
            m_endValue = p_otherRange.m_endValue;
        }

        public Range(float m_min, float m_max)
        {
            m_startValue = m_min;
            m_endValue = m_max;
        }

        public float StartValue
        {
            get { return m_startValue; }
            set { m_startValue = value; }
        }

        public float EndValue
        {
            get { return m_endValue; }
            set { m_endValue = value; }
        }
    }
}
