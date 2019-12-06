using UnityEngine;

namespace Turrets
{
    [System.Serializable]
    public class SpriteState
    {
        [SerializeField] private Sprite m_standardSprite = null;
        [SerializeField] private Sprite m_selectedSprite = null;


        public Sprite StandardSprite => m_standardSprite;

        public Sprite SelectedSprite => m_selectedSprite;
    }
}
