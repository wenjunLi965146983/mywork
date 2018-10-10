using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WorldGeneralLib.PLC
{
    public enum PLCResponse
    {
        SUCCESS,
        TIMEOUT,
        INITFAIL,
        ADDWRONG,
        ERROR,
        FCSERR,
        OTHERS
    };
    public enum PLCDataType
    {
        BIT16,
        BIT32,
        BIN16,
        HEX16,
        BIN32,
        HEX32,
        DOUBLE,
        STRING,
    };
    public class PLCBaseClass
  {
        protected object objLock = new object();
        public bool bInitOK = false;
        protected System.IO.Ports.SerialPort serialPort;
        public virtual bool Init(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            serialPort = new System.IO.Ports.SerialPort();
            try
            {
                serialPort.PortName = portName;
                serialPort.BaudRate = baudRate;
                serialPort.DataBits = dataBits;
                serialPort.StopBits = stopBits;
                serialPort.Parity = parity;
                serialPort.Open();
                bInitOK = true;
            }
            catch
            {
                bInitOK = false;
               
                return false;
            }
            return true;
        }
        public virtual PLCResponse GetBit(string strAddress,ref bool bOn)
        {
            return PLCResponse.SUCCESS;
        }
        public virtual PLCResponse SetBit(string strAddress, bool bOn)
        {
            return PLCResponse.SUCCESS;
        }
        public virtual PLCResponse GetWord(string strAddress, PLCDataType dataType,ref string strValue)
        {
            return PLCResponse.SUCCESS;
        }
        public virtual PLCResponse SetWord(string strAddress, PLCDataType dataType,string strValue)
        {
            return PLCResponse.SUCCESS;
        }
        protected virtual string ConvertMesToValue(string strMessage, PLCDataType dataType)
        {
            string strReturn="####";
            if (strMessage.Length != 8)
            {
                return "Error";
            }
            string strLowWord = strMessage.Substring(2, 1) + strMessage.Substring(3, 1) + strMessage.Substring(0, 1) + strMessage.Substring(1,1);
            string strDWord = strMessage.Substring(6, 1) + strMessage.Substring(7, 1) + strMessage.Substring(4, 1) + strMessage.Substring(5, 1) + strLowWord;
            Int16 iLowWord = Convert.ToInt16(strLowWord, 16);
            Int32 iDWord = Convert.ToInt32(strDWord, 16);
            //double dValueResult = Convert.ToDoubde(strDWord,new FOR;
            if (dataType == PLCDataType.BIT16)
            {
                strReturn = Convert.ToString(iLowWord, 2);
                strReturn = strReturn.PadLeft(16, '0');
            }
            if (dataType == PLCDataType.BIT32)
            {
                strReturn = Convert.ToString(iDWord, 2);
                strReturn = strReturn.PadLeft(32, '0');
            }
            if (dataType == PLCDataType.BIN16)
            {
                strReturn = iLowWord.ToString();
            }
            if (dataType == PLCDataType.BIN32)
            {
                strReturn = iDWord.ToString();
            }
            if (dataType == PLCDataType.HEX16)
            {
             strReturn = strLowWord;
            }
            if (dataType == PLCDataType.HEX32)
            {
            strReturn = strDWord;
            }
            if (dataType == PLCDataType.DOUBLE)
            {
                int num = int.Parse(strDWord, System.Globalization.NumberStyles.AllowHexSpecifier);
               byte[] floatValues = BitConverter.GetBytes(num);
                float f = BitConverter.ToSingle(floatValues, 0);
                strReturn = f.ToString();
            }
            return strReturn;
        }
        protected virtual string ConvertValueToMessage(string strValue, PLCDataType dataType)
        {
            string strReturn = "00000000";
            string strTemp = "";
            try
            {
                if (dataType == PLCDataType.BIT16)
                {
                    Int16 iTemp16 = Convert.ToInt16(strValue, 2);
                    strTemp = iTemp16.ToString("X4");
                    strReturn = strTemp.Substring(2, 1) + strTemp.Substring(3, 1) + strTemp.Substring(0, 1) + strTemp.Substring(1, 1);
                }
                if (dataType == PLCDataType.BIT32)
                {
                    Int32 iTemp32 = Convert.ToInt32(strValue, 2);
                    strTemp = iTemp32.ToString("X8");
                    strReturn = strTemp.Substring(6, 1) + strTemp.Substring(7, 1) + strTemp.Substring(4, 1) + strTemp.Substring(5, 1)+strTemp.Substring(2, 1) + strTemp.Substring(3, 1) + strTemp.Substring(0, 1) + strTemp.Substring(1, 1);
                }
                if (dataType == PLCDataType.BIN16)
                {
                    Int16 iTemp16 = Convert.ToInt16(strValue, 10);
                    strTemp = iTemp16.ToString("X4");
                    strReturn = strTemp.Substring(2, 1) + strTemp.Substring(3, 1) + strTemp.Substring(0, 1) + strTemp.Substring(1, 1);
                }
                if (dataType == PLCDataType.BIN32)
                {
                    Int32 iTemp32 = Convert.ToInt32(strValue, 10);
                    strTemp = iTemp32.ToString("X8");
                    strReturn = strTemp.Substring(6, 1) + strTemp.Substring(7, 1) + strTemp.Substring(4, 1) + strTemp.Substring(5, 1) + strTemp.Substring(2, 1) + strTemp.Substring(3, 1) + strTemp.Substring(0, 1) + strTemp.Substring(1, 1);
                }
                if (dataType == PLCDataType.HEX16)
                {
                    Int16 iTemp16 = Convert.ToInt16(strValue, 16);
                    strTemp = iTemp16.ToString("X4");
                    strReturn = strTemp.Substring(2, 1) + strTemp.Substring(3, 1) + strTemp.Substring(0, 1) + strTemp.Substring(1, 1);
                }
                if (dataType == PLCDataType.HEX32)
                {
                    Int32 iTemp32 = Convert.ToInt32(strValue, 16);
                    strTemp = iTemp32.ToString("X8");
                    strReturn = strTemp.Substring(6, 1) + strTemp.Substring(7, 1) + strTemp.Substring(4, 1) + strTemp.Substring(5, 1) + strTemp.Substring(2, 1) + strTemp.Substring(3, 1) + strTemp.Substring(0, 1) + strTemp.Substring(1, 1);
                }
                if (dataType == PLCDataType.DOUBLE)
                {
                    float f = Convert.ToSingle(strValue);
                    byte[] floatValues = BitConverter.GetBytes(f);
                    strTemp = floatValues[3].ToString("X2") + floatValues[2].ToString("X2") + floatValues[1].ToString("X2") + floatValues[0].ToString("X2");
                    strReturn = strTemp.Substring(6, 1) + strTemp.Substring(7, 1) + strTemp.Substring(4, 1) + strTemp.Substring(5, 1) + strTemp.Substring(2, 1) + strTemp.Substring(3, 1) + strTemp.Substring(0, 1) + strTemp.Substring(1, 1);

                }
                if (dataType == PLCDataType.STRING)
                {
                    char[] charArrange = strValue.ToCharArray();
                    byte byteTemp = 0;
                    foreach (char tempChar in charArrange)
                    {
                        byteTemp = (byte)tempChar;
                        strTemp = strTemp + byteTemp.ToString("X2");
                    }
                    strReturn = strTemp;
                }
            }
            catch 
            {
            }
            return strReturn;
        }
        protected string CreateFcsCodeFun(string strInput)
        {
            byte byteTemp;
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(strInput);
            byteTemp = byteArray[0];
            for (int i = 1; i < byteArray.Length; i++)
            {
                byteTemp = (byte)(byteTemp ^ byteArray[i]);
            }
            return byteTemp.ToString("X2");
        }
        public virtual bool GetBitAddress(string strAddress, ref string strStationName, ref string strWordType, ref string strWordAddr, ref string strBitAddr)
        {
            
            return false;
        }
        public virtual bool GetWordAddress(string strAddress, ref string strStationName, ref string strWordType, ref string strWordAddr)
        {
            return false;
        }
        public virtual PLCResponse GetWordS(string strAddressStart, string strAddressEnd, ref string strValue)
        {
            return PLCResponse.INITFAIL;
        }
        public virtual PLCResponse GetWordSS(string strAddressStart, string strAddressEnd, ref string strValue)
        {
            return PLCResponse.INITFAIL;
        }
    }
}
