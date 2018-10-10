using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware
{
    public enum DefaultAxis:short
    {
        X = 0, Y, Z, U, A, B, C, D
    }
    public enum PlcDataType
    {
        BIT,
        INT16,
        UINT16,
        INT32,
        UINT32,
        REAL,
        STRING
    }
    public enum CodeReaderRes
    {
        SUCCESS,
        TIMEOUT,
        ERROR,
        INITFAIL,
        OTHERS
    };
    public enum Protocol
    {
        RS232,
        TcpIP
    }
    public class HardwareBase
    {
        public bool bInitOk = false;
        public bool bConnected = false;
        protected object lockObj = new object();

        public HardwareBase()
        {

        }

        virtual public bool Init(HardwareData hardeareData)
        {
            return false;
        }
        virtual public bool IsConnected()
        {
            return bConnected;
        }
        virtual public bool Close()
        {
            return true;
        }
    }
}
