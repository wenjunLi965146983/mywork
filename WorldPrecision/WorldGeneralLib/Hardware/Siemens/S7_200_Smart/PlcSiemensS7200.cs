using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Functions;
using System.Threading;
using System.Windows.Forms;
using WorldGeneralLib.TaskBase;
using OmronFins.Net;
using WorldGeneralLib.Alarm;
using WorldGeneralLib.Hardware;
using ModbusTcpLib;

namespace WorldGeneralLib.Hardware.Siemens.S7_200_Smart
{
    public class PlcSiemensS7200 : HardwareBase//,IPlcAction
    {
        public int times = 0;
        private Master _modbusMaster;
        public PlcSiemensS7200Data plcData;

        public PlcSiemensS7200(PlcSiemensS7200Data plcData)
        {
            this.plcData = plcData;
        }
        public override bool Init(HardwareData hardwareData)
        {
            bInitOk = false;
            try
            {
                _modbusMaster = new Master(plcData.IP, 502);
                return true;
            }
            catch 
            {  

                return false;
            }
        }

        #region Read Coil
        public PlcResonse GetSingleBit(string strStartAddr,ref string strValue)
        {
            //Read coil
            try
            {
                int id = 1;
                int iStartAddr = Convert.ToInt32(strStartAddr);
                byte[] temp = new byte[2];

                _modbusMaster.ReadCoils(id, iStartAddr, 1, ref temp);
                if (null == temp)
                {
                    return PlcResonse.ERROR;
                }
                strValue = temp.ToString();
                return PlcResonse.SUCCESS;
            }
            catch (Exception)
            {
                return PlcResonse.ADDWRONG;
            }
        }
        public PlcResonse GetSingleBit(string strItemName)
        {
            PlcResonse plcRes = PlcResonse.ERROR;
            try
            {
                if(!plcData.m_scanDictionary.ContainsKey(strItemName))
                {
                    return PlcResonse.ERROR;
                }
                string strValue = "";
                plcRes = GetSingleBit(plcData.m_scanDictionary[strItemName].Address, ref strValue);
                plcData.m_scanDictionary[strItemName].strValue = strValue;
            }
            catch (Exception)
            {
                return PlcResonse.ERROR;
            }
            return plcRes;
        }
        public PlcResonse GetMultiBits(string strStartAddr, byte byteLength, ref string strValue)
        {
            if (byteLength <= 0)
            {
                return PlcResonse.ERROR;
            }

            try
            {
                int id = 1;
                int iStartAddr = Convert.ToInt32(strStartAddr);
                byte[] temp = new byte[byteLength];

                _modbusMaster.ReadCoils(id, iStartAddr, byteLength, ref temp);
                if (null == temp)
                {
                    return PlcResonse.ERROR;
                }
                strValue = temp.ToString();
                return PlcResonse.SUCCESS;
            }
            catch (Exception)
            {
                return PlcResonse.ADDWRONG;
            }
        }
        public PlcResonse GetMultiBits(string strItemName)
        {
            PlcResonse plcRes = PlcResonse.ERROR;
            try
            {
                if (!plcData.m_scanDictionary.ContainsKey(strItemName))
                {
                    return PlcResonse.ERROR;
                }
                string strValue = "";
                plcRes = GetMultiBits(plcData.m_scanDictionary[strItemName].Address, plcData.m_scanDictionary[strItemName].Length , ref strValue);
                plcData.m_scanDictionary[strItemName].strValue = strValue;
            }
            catch (Exception)
            {
                return PlcResonse.ERROR;
            }
            return plcRes;
        }

        public PlcResonse SetSingleBit(string strStartAddr, bool bSet)
        {
            try
            {
                int id = 1;
                int iStartAddr = Convert.ToInt16(strStartAddr);
                byte[] temp = new byte[256];

                _modbusMaster.WriteSingleCoils(id, iStartAddr, bSet, ref temp);
                if (null == temp)
                    return PlcResonse.TIMEOUT;
                return PlcResonse.SUCCESS;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.GetType() == typeof(FormatException))
                    return PlcResonse.ADDWRONG;
                else
                    return PlcResonse.ERROR;
            }
        }
        public PlcResonse SetMultiBits(string strStartAddr, int iLen, byte[] values)
        {
            try
            {
                int id = 6;
                int iStartAddr = Convert.ToInt16(strStartAddr);
                byte[] temp = new byte[256];

                _modbusMaster.WriteMultipleCoils(id, iStartAddr, iLen, values, ref temp);
                if (null == temp)
                    return PlcResonse.TIMEOUT;
                return PlcResonse.SUCCESS;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.GetType() == typeof(FormatException))
                    return PlcResonse.ADDWRONG;
                else
                    return PlcResonse.ERROR;
            }
        }
        #endregion

        #region Read Holding Register
        public PlcResonse ReadHoldingRegister(string strStartAddr, int iLen, ref string strValue)
        {
            return PlcResonse.ERROR;
        }
        public PlcResonse ReadHoldingRegister(string strItemName)
        {
            return PlcResonse.ERROR;
        }
        public PlcResonse WriteHoldingRegister(string strStartAddr, int iLen, string strValue, ref byte[] restult)
        {
            return PlcResonse.ERROR;
        }
        #endregion
#if false
        public PlcResonse ReadHoldingRegister(string strItemName)
        {
            try
            {
                if (!plcData.m_scanDictionary.ContainsKey(strItemName))
                {
                    return PlcResonse.OTHERS;
                }

                byte len = plcData.m_scanDictionary[strItemName].Length;
                if (len <= 0) return PlcResonse.ERROR;

                int id = 1;
                byte[] temp = new byte[len];
                _modbusMaster.ReadHoldingRegister(id,Convert.ToInt32(plcData.m_scanDictionary[strItemName].Address),len,ref temp);
                if (null == temp)
                {
                    return PlcResonse.ERROR;
                }
                plcData.m_scanDictionary[strItemName].strValue = temp.ToString();
                return PlcResonse.SUCCESS;
            }
            catch (Exception)
            {
                return PlcResonse.ERROR;
            }
        }
        //public PlcResonse WriteSingleRegister(string strItemName)
        public PlcResonse GetWord(string strItemName)
        {

            return PlcResonse.ERROR;
        }
        public PlcResonse SetWord(string strItemName, string strValue)
        {
            return PlcResonse.ERROR;
        }
#endif
#region Event for response data
        private void ModbusMaster_EventResponseDataHandler(int ID, byte function, byte[] values)
        {
            // Ignore watchdog response data
            if (ID == 0xFF) return;

            // ------------------------------------------------------------------------
            // Identify requested data
            //switch (ID)
            {
                //case 1:
                //    grpData.Text = "Read coils";
                //    data = values;
                //    ShowAs(null, null);
                //    break;
                //case 2:
                //    grpData.Text = "Read discrete inputs";
                //    data = values;
                //    ShowAs(null, null);
                //    break;
                //case 3:
                //    grpData.Text = "Read holding register";
                //    data = values;
                //    ShowAs(null, null);
                //    break;
                //case 4:
                //    grpData.Text = "Read input register";
                //    data = values;
                //    ShowAs(null, null);
                //    break;
                //case 5:
                //    grpData.Text = "Write single coil";
                //    break;
                //case 6:
                //    grpData.Text = "Write multiple coils";
                //    break;
                //case 7:
                //    grpData.Text = "Write single register";
                //    break;
                //case 8:
                //    grpData.Text = "Write multiple register";
                //    break;
            }
        }
#endregion
#region Event for slave exception
        private void ModbusMaster_EventExceptionHandler(int ID, byte function, byte ex)
        {
            string strEx = "Modbus says errlr: ";
            switch (ex)
            {
                case Master.excIllegalFunction: strEx += "Illegal function!"; break;
                case Master.excIllegalDataAdr: strEx += "Illegal data adress!"; break;
                case Master.excIllegalDataVal: strEx += "Illegal data value!"; break;
                case Master.excSlaveDeviceFailure: strEx += "Slave device failure!"; break;
                case Master.excAck: strEx += "Acknoledge!"; break;
                case Master.excMemParityErr: strEx += "Memory parity error!"; break;
                case Master.excGatePathUnavailable: strEx += "Gateway path unavailbale!"; break;
                case Master.excExceptionTimeout: strEx += "Slave timed out!"; break;
                case Master.excExceptionConnectionLost: strEx += "Connection is lost!"; break;
                case Master.excExceptionNotConnected: strEx += "Not connected!"; break;
            }

            MessageBox.Show(strEx, "Modbus slave exception");
        }
#endregion
    }
}
