using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public interface IControlPLC
    {
        PLCResponse GetDriverStatus();
        void FreshDriverStatus();
    }
}
