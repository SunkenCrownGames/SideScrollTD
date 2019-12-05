using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level
{
    public class Ground : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private List<Platform> m_platforms;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void AddToPlatforms(Platform p_platformToAdd)
        {
            var foundPlatform = Enumerable.Contains(m_platforms, p_platformToAdd);

            if(!foundPlatform) m_platforms.Add(p_platformToAdd);
        }
    }
}
