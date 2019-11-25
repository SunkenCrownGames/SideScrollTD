using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngieTools
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField]
        private float m_customTimeScale = 0;

        [SerializeField]
        private float m_customTime = 0;

        [SerializeField]
        private static TimeManager m_instance = null;

        private void Awake()
        {
            BindInstance();
            BindComponents();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_customTime = Time.deltaTime * m_customTimeScale;
        }

        private void BindInstance()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Debug.LogError("Instance Already Bound Please Check: " + gameObject.name + " For Duplicate");
            }
        }

        private void BindComponents()
        {
            m_customTime = Time.deltaTime * m_customTimeScale;
        }

        public void SetNewDeltaTimeScale(float p_newScale)
        {
            m_customTimeScale = p_newScale;
        }

        public float CustomDeltaTime
        {
            get { return m_customTime; }
        }
        public float CustomTimeScale
        {
            get { return m_customTimeScale; }
        }

        public static TimeManager Manager
        {
            get { return m_instance; }
        }
    }
}