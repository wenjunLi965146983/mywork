using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.TaskBase;

namespace WorldGeneralLib.Hardware.Demo
{
    public class DemoInputCard : HardwareBase
    {
        private bool[] _bArrayInputSta = new bool[128];

        public bool GetInputSta(int iBit)
        {
            return (iBit < 128 && iBit >= 0) ? _bArrayInputSta[iBit] : false;
        }

        public override bool Init(HardwareData hardeareData)
        {
            bInitOk = true;
            System.Threading.Thread threadRefreshSta = new System.Threading.Thread(ThreadRefreshStaHandler);
            threadRefreshSta.IsBackground = true;
            threadRefreshSta.Start();
            return true;
        }

        private void ThreadRefreshStaHandler()
        {
            HiPerfTimer timer = new HiPerfTimer();
            System.Threading.Thread.Sleep(1000);

            int iStep = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                switch (iStep)
                {
                    case 0:
                        {
                            timer.Start();
                            iStep = 10;
                        }
                        break;
                    case 10:
                        {
                            if (timer.TimeUp(1))
                            {
                                timer.Start();
                                for (int i = 0; i < 128; i++)
                                {
                                    _bArrayInputSta[i] = true;
                                }
                                iStep = 20;
                            }
                        }
                        break;
                    case 20:
                        {
                            if (timer.TimeUp(1))
                            {
                                for (int i = 0; i < 128; i++)
                                {
                                    _bArrayInputSta[i] = false;
                                }
                                iStep = 0;
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
