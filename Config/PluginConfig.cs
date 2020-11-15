using SCP_008.Config;
using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;

namespace SCP_008.Config
{
    public class PluginConfig : AbstractConfigSection
    {
        public SCP049Configs Scp049Configs { get; set; } = new SCP049Configs();
        public SCP008Configs Scp008Configs { get; set; } = new SCP008Configs();
    }
}