using UnityEngine;

namespace Weapons.Modifiers
{
    [System.Serializable]
    public abstract class Modifier
    {
        protected int m_id;
        protected ModifierType m_type;
        protected bool m_status = false;


        public void SetStatus(bool p_status)
        {
            m_status = p_status;
        }


        public int Id => m_id;

        public ModifierType Type => m_type;

        public bool Status => m_status;
    }

    public enum ModifierType
    {
        MultipleCast,
        Split,
        Explosion,
        Bounce
    }
}
