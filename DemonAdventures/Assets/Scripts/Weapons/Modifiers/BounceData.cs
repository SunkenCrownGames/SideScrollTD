using System.Text;
using UnityEngine;

namespace Weapons.Modifiers
{
    [System.Serializable]
    public class BounceData : Modifier
    {
        [SerializeField] private int m_bounceCount;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bounce Count: {m_bounceCount}");

            return sb.ToString();
        }
    }
}
