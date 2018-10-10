using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware
{
    interface IInputAction
    {
        bool GetInputBit(int iBit);
    }
}
