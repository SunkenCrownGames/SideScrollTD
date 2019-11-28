using Player.Soldiers.Data.Modifiers;
using UnityEngine;

namespace Player.Turrets.Modifiers
{
    public class DodModifier : MonoBehaviour
    {
        [SerializeField] private DamageOverDistanceModifierData m_data;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void InitializeDod(DamageOverDistanceModifierData p_data)
        {
            m_data = new DamageOverDistanceModifierData(p_data);
        }
    }
}
