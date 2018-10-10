using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace WorldPrecision.Logs
{
     public static class Log
    {
        public static Logger ScanTaskLog = LogManager.GetLogger("ScanTaskLog");
        public static Logger WsTask1Log = LogManager.GetLogger("WsTask1Log");
        public static Logger WsTask2Log = LogManager.GetLogger("WsTask2Log");
        public static Logger WsTask3Log = LogManager.GetLogger("WsTask3Log");
        public static Logger WsTask4Log = LogManager.GetLogger("WsTask4Log");
        public static Logger WsTask5Log = LogManager.GetLogger("WsTask5Log");
        public static Logger WsTask6Log = LogManager.GetLogger("WsTask6Log");
    }
}
