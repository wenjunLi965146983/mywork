using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.TaskBase;

namespace WorldGeneralLib.Hardware.Demo
{
    public class DemoInputOutputCard : HardwareBase
    {
        private bool[] _bArrayInputSta = new bool[128];
        private bool[] _bArrayOutputSta = new bool[128];

        public bool GetInputSta(int iBit)
        {
            return (iBit < 128 && iBit >= 0) ? _bArrayInputSta[iBit] : false;
        }
        public bool GetOutputSta(int iBit)
        {
            return (iBit < 128 && iBit >= 0) ? _bArrayOutputSta[iBit] : false;
        }
        public bool SetOutput(int iBit)
        {
            if(iBit < 128 && iBit >= 0)
            {
                _bArrayOutputSta[iBit] = true;
                return true;
            }

            return false;
        }

        public override bool Init(HardwareData hardeareData)
        {
            bInitOk = true;
            System.Threading.Thread threadRefreshSta = new System.Threading.Thread(ThreadRefreshStaHandler);
            threadRefreshSta.IsBackground = true;
            threadRefreshSta.Start();
            return base.Init(hardeareData);
        }

        private void ThreadRefreshStaHandler()
        {


        }
    }
}
