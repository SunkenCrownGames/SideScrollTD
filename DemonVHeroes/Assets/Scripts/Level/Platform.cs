using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level
{
    public class Platform : MonoBehaviour
    {
        private SpriteRenderer m_sr;

        private PlatformRayData m_data;
        
        private void Awake()
        {
            BindComponents();
        }
        
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        private void BindComponents()
        {
            if (m_sr == null)
                m_sr = GetComponentInParent<SpriteRenderer>();
        }


        public void BindRayData(PlatformRayData p_data)
        {
            m_data = new PlatformRayData(p_data);
        }

        [Button("Draw Ray")]
        public void DrawRays()
        {
            PlatformRayData.DrawRays(m_data);
            PlatformRayData.FireRay(m_data);
        }
    }
}
