using System.Collections.Generic;
using AngieTools;
using UnityEngine;

namespace Level
{

    [System.Serializable]
    public class HitPlatformResult
    {
        
        [SerializeField] List<HitPlatformResultData> m_data;
        
        public HitPlatformResult()
        {
            m_data = new List<HitPlatformResultData>();
        }

        public void AddToResults(Platform p_hitPlatform, Direction p_direction, Vector3 p_rayStartPosition)
        {
            foreach (var data in m_data)
            {
                if (!data.m_hitPlatform.Equals(p_hitPlatform)) continue;
                
                data.m_rayStartPosition.Add(p_rayStartPosition);
                return;
            }
            
            m_data.Add(new HitPlatformResultData(p_rayStartPosition, p_hitPlatform, p_direction));
        }


        public List<HitPlatformResultData> Data => m_data;
    }
    
    
    [System.Serializable]
    public class HitPlatformResultData
    {

        public HitPlatformResultData()
        {
            m_rayStartPosition = new List<Vector3>();
            m_hitPlatform = null;
            m_direction = Direction.BOTTOM;
        }
        
        public HitPlatformResultData(List<Vector3> p_rayStartPosition, Platform p_hitPlatform, Direction p_direction)
        {
            m_rayStartPosition = p_rayStartPosition;
            m_hitPlatform = p_hitPlatform;
            m_direction = p_direction;
        }
        
        public HitPlatformResultData(Vector3 p_rayStartPosition, Platform p_hitPlatform, Direction p_direction)
        {
            m_rayStartPosition = new List<Vector3>() { p_rayStartPosition };
            m_hitPlatform = p_hitPlatform;
            m_direction = p_direction;
        }
        

        public List<Vector3> m_rayStartPosition;
        public Platform m_hitPlatform;
        public Direction m_direction;
    }
}
