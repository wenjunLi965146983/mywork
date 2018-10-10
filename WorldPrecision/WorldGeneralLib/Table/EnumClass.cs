using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Table
{
        public enum PointMoveMode
        {
            PTP,
            NTP
        }

        public enum SenserLogic
        {
            NC,
            NO,
            DISABLE
        }
        public enum PulseMode
        {
            PLDI,
            CWCCW
        }
        public enum HomeMode
        {
            CCWL = 0,
            CWL,
            CWORG,
            CCWORG
        }
    
}
