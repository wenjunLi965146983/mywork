using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace WorldGeneralLib.Functions
{
    public static class Log
    {
        public static Logger SysLog = LogManager.GetLogger("SysLog");
    }
}
