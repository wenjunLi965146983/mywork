using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.IO
{
    public class InputDrivers
    {
        public Dictionary<string, InputDriver> dicDrivers;
        public InputDrivers()
        {
            dicDrivers = new Dictionary<string, InputDriver>();
        }
    }
}
