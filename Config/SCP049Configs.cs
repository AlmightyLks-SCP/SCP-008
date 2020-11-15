using System.ComponentModel;

namespace SCP_008.Config
{
    public class SCP049Configs
    {
        [Description("Chance to not one hit K.O. as SCP 049")]
        public float NonOHKChance { get; set; } = 0.0f;

        [Description("Amount of damage in % of target's max-life when SCP 049 does not one hit K.O.")]
        public float NonOHKDamage { get; set; } = 0.0f;

        [Description("Chance of 049 infecting the target on non-one hit K.O.?")]
        public float InfectChanceOnNonOHK { get; set; } = 0.0f;
    }
}
