using Spine.Unity;
using UnityEngine;
using Spine;
using Animation = Spine.Animation;

namespace Enemy
{
    public class EnemySpineController : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation m_skele;

        [SerializeField] [SpineAnimation] private string m_idle;
        [SerializeField] [SpineAnimation] private string m_walk;
        [SerializeField] [SpineAnimation] private string m_jump;
        [SerializeField] [SpineAnimation] private string m_down;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Flip(bool p_flip)
        {
            var flipDir = !p_flip ? -1 : 1;
            m_skele.skeleton.ScaleX = flipDir;
        }

        public void SetWalk()
        {
            var animTrack = m_skele.AnimationState.GetCurrent(0);

            if (animTrack != null)
            {
                var anim = animTrack.Animation;

                if (anim == null || !anim.Name.Equals(m_walk))
                {
                    m_skele.state.SetAnimation(0, m_walk, true);
                }
            }
            else
            {
                m_skele.state.SetAnimation(0, m_walk, true);
            }
        }

        public void SetUpAnimation()
        {
            var animTrack = m_skele.AnimationState.GetCurrent(0);

            if (animTrack != null)
            {
                var anim = animTrack.Animation;

                if (anim == null || !anim.Name.Equals(m_jump))
                {
                    m_skele.state.SetAnimation(0, m_jump, false);
                }
            }
            else
            {
                m_skele.state.SetAnimation(0, m_jump, false);
            }
        }
        
        public void SetDownAnimation()
        {
            var animTrack = m_skele.AnimationState.GetCurrent(0);

            if (animTrack != null)
            {
                var anim = animTrack.Animation;

                if (anim == null || !anim.Name.Equals(m_down))
                {
                    m_skele.state.SetAnimation(0, m_down, false);
                }
            }
            else
            {
                m_skele.state.SetAnimation(0, m_down, false);
            }
        }
    }
}
