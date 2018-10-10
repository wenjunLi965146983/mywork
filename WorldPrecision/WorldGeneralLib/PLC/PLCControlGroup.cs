using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public class PLCControlGroup
    {
        public List<IControlPLC> PLCControls;
        public PLCControlGroup()
        {
            PLCControls = new List<IControlPLC>();
        }
    }
}
