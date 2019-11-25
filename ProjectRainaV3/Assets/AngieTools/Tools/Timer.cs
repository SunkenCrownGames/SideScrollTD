using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngieTools
{
    [System.Serializable]
    public class Timer
    {
        [SerializeField]
        protected float m_currentTime = 0;
        [SerializeField]
        protected Range m_timerRange = null;


        public Timer()
        {
            m_timerRange = new Range();
            m_currentTime = 0;
        }

        public Timer(Timer p_otherTimer)
        {
            m_currentTime = p_otherTimer.m_currentTime;
            m_timerRange = new Range(p_otherTimer.m_timerRange);
        }



        public virtual void InitializeTimer()
        {
            m_currentTime = m_timerRange.StartValue;
        }

        public virtual bool Count()
        {
            if (m_currentTime < m_timerRange.EndValue)
            {
                if (TimeManager.Manager != null)
                {
                    m_currentTime += TimeManager.Manager.CustomDeltaTime;
                }
                else
                {
                    m_currentTime += Time.deltaTime;
                }
            }
            else
            {
                m_currentTime = 0;
                return true;
            }

            return false;
        }

        public void CountUp()
        {

                if (TimeManager.Manager != null)
                {
                    m_currentTime += TimeManager.Manager.CustomDeltaTime;
                }
                else
                {
                    m_currentTime += Time.deltaTime;
                }
        }

        public Range TimerRange
        { 
            get { return m_timerRange; }
            set { m_timerRange = value; }
        }

        public float CurrentTime
        {
            get { return m_currentTime; }
            set { m_currentTime = value; }
        }
    }
}
