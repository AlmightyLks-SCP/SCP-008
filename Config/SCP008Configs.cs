using System.Collections.Generic;
using System.ComponentModel;

namespace SCP_008.Config
{
    public class SCP008Configs
    {
        [Description("Roles which can spread 008")]
        public List<int> InfectingRoles { get; set; } = new List<int>() { 10 };

        [Description("Toggle % damage and the amount for 008's damage over time tick")]
        public DOTConfig DamagerPerTickPercentage { get; set; } = new DOTConfig();

        [Description("Toggle static damage and the amount for 008's damage over time tick")]
        public DOTConfig DamagerPerTickStatic { get; set; } = new DOTConfig();

        [Description("008 damage per tick")]
        public float DamagerOverTimeInterval { get; set; } = 2.5f;

        [Description("Healing items which remove SCP 008")]
        public List<int> InfectionHealItems { get; set; } = new List<int>() { (int)ItemType.SCP500 };
    }
}
