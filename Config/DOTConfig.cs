using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_008.Config
{
    public class DOTConfig
    {
        public bool Enabled { get; set; }
        public float Amount { get; set; }
        public DOTConfig()
        {
            Enabled = false;
            Amount = 0.0f;
        }
    }
}
