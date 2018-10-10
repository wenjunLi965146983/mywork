using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware.Panasonic
{
    public class PlcPanasonic:HardwareBase
    {
        public PlcPanasonicData plcData;

        public PlcPanasonic(PlcPanasonicData plcData)
        {
            this.plcData = plcData;
        }
    }
}
