using SCP_008.Config;
using Synapse.Api;
using Synapse.Api.Plugin;
using System.Collections.Generic;

namespace SCP_008
{
    [PluginInformation(
        Author = "AlmightyLks",
        Description = "Brings SCP 008 in the game & adds some 049 utilities",
        Name = "SCP-008",
        SynapseMajor = 2,
        SynapseMinor = 1,
        SynapsePatch = 0,
        Version = "1.1.0"
        )]
    public class SCP_008 : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "SCP-008")]
        public static PluginConfig Config;

        public HashSet<Player> InfectedPlayers { get; private set; }

        public override void Load()
        {
            InfectedPlayers = new HashSet<Player>();
            SynapseController.Server.Logger.Info("<SCP-008> Loaded");
            _ = new PluginEventHandler(InfectedPlayers);
        }
    }
}
