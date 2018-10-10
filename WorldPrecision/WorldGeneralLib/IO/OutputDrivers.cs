using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.IO
{
    public class OutputDrivers
    {
        public Dictionary<string, OutputDriver> dicDrivers;
        public OutputDrivers()
        {
            dicDrivers = new Dictionary<string, OutputDriver>();
        }
    }
}
