using UnityEngine;

namespace Player.UI
{
    public class InfoUiData
    {
        public InfoUiData(string p_name, string p_description, float p_hpStat, float p_damageStat, float p_attackSpeedStat, float p_rangeStat)
        {
            Name = p_name;
            Description = p_description;
            HpStat = p_hpStat;
            DamageStat = p_damageStat;
            AttackSpeedStat = p_attackSpeedStat;
            RangeStat = p_rangeStat;
        }

        public string Name { get; }

        public string Description { get; }

        public float HpStat { get; }

        public float DamageStat { get; }

        public float AttackSpeedStat { get; }

        public float RangeStat { get; }
    }
}
