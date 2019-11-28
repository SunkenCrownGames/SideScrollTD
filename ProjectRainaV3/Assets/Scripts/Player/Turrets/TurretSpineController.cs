using Spine.Unity;
using UnityEngine;

namespace Player.Turrets
{
    public class TurretSpineController : MonoBehaviour
    {
        [SerializeField] private bool m_outlineEnabled= false;
        [SerializeField] private SkeletonAnimation m_skeleton = null;
        [SerializeField] private SkeletonAnimation m_outline = null;

        [SerializeField] [SpineAnimation] private string m_idle = null;
        [SerializeField] [SpineAnimation] private string m_attack = null;
        [SerializeField] [SpineAnimation] private string m_charging = null;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void SetIdle()
        {
            if(m_skeleton == null) return;
            
            m_skeleton.AnimationState.SetAnimation(0, m_idle, true);

            if (!m_outlineEnabled) return;

            m_outline.AnimationState.SetAnimation(0, m_idle, true);
        }

    }
}
