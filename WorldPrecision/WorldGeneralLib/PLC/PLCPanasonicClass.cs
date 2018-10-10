using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WorldGeneralLib.PLC
{
    internal class PLCPanasonicClass : PLCBaseClass
    {
        public override bool Init(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
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

        public override PLCResponse GetBit(string strAddress, ref bool bOn)
        {
            if (bInitOK == false)
            {
                return PLCResponse.INITFAIL;
            }
            string[] strArrage;
            if (BitAddressIsOK(strAddress, out strArrage) == false)
            {
                return PLCResponse.ADDWRONG;
            }
            return GetBitValue(strArrage, out bOn);
        }
        public override PLCResponse SetBit(string strAddress, bool bOn)
        {
            if (bInitOK == false)
            {
                return PLCResponse.INITFAIL;
            }
            string[] strArrage;
            if (BitAddressIsOK(strAddress, out strArrage) == false)
            {
                return PLCResponse.ADDWRONG;
            }
            return SetBitValue(strArrage,bOn);
        }
        internal PLCResponse GetBitValue(string[] strArray, out bool bOn)
        {
            bOn = false;
            HiPerfTimer timeM = new HiPerfTimer();
            string strCommand = "%" + strArray[0] + "#RCS" + strArray[1];
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "\r";
            string strMessage = strCommand + strFcs + strEndCode;
            string strRecieve;
            lock (objLock)
            {
                #region 通信
                try
                {
                    serialPort.ReadExisting();
                    serialPort.Write(strMessage);
                    timeM.Start();
                    strRecieve = serialPort.ReadExisting();
                    while (true)
                    {
                        try
                        {
                            strRecieve = strRecieve + serialPort.ReadExisting();
                            if (strRecieve.IndexOf("\r") > -1)
                            {
                                //strRecieve = strRecieve.Trim();
                                if (strRecieve.IndexOf("%") < 0 || strRecieve.IndexOf("$") < 0 || strRecieve.Length != 10)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 7);
                                string strFcsReturn = strRecieve.Substring(7, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                if (strRecieve[6] == '1')
                                {
                                    bOn = true;
                                }
                                else
                                {
                                    bOn = false;
                                }
                                break;
                            }
                            if (timeM.TimeUp(0.5))
                            {
                                return PLCResponse.TIMEOUT;
                            }
                        }
                        catch
                        {
                            return PLCResponse.OTHERS;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch
                {
                    return PLCResponse.OTHERS;
                }
                #endregion
            }
            return PLCResponse.SUCCESS;
        }
        internal PLCResponse SetBitValue(string[] strArray, bool bOn)
        {
            HiPerfTimer timeM = new HiPerfTimer();
            string strOn = "0";
            if (bOn)
            {
                strOn = "1";
            }
            else
            {
                strOn="0";
            }
            string strCommand = "%" + strArray[0] + "#WCS" + strArray[1] + strOn;
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "\r";
            string strMessage = strCommand + strFcs + strEndCode;
            string strRecieve;
            lock (objLock)
            {
                #region 通信
                try
                {
                    serialPort.ReadExisting();
                    serialPort.Write(strMessage);
                    timeM.Start();
                    strRecieve = serialPort.ReadExisting();
                    while (true)
                    {
                        try
                        {
                            strRecieve = strRecieve + serialPort.ReadExisting();
                            if (strRecieve.IndexOf("\r") > -1)
                            {
                                if (strRecieve.IndexOf("%") < 0 || strRecieve.IndexOf("$") < 0 || strRecieve.Length != 9)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 6);
                                string strFcsReturn = strRecieve.Substring(6, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                break;
                            }
                            if (timeM.TimeUp(0.5))
                            {
                                return PLCResponse.TIMEOUT;
                            }
                        }
                        catch
                        {
                            return PLCResponse.OTHERS;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch
                {
                    return PLCResponse.OTHERS;
                }
                #endregion
            }
            return PLCResponse.SUCCESS;
        }
        internal bool BitAddressIsOK(string strAddress, out string[] strArray)
        {
            char[] charArray = new char[1];
            charArray[0] = '#';
            strArray = strAddress.Split(charArray);
            if (strArray.Length != 2)
            {
                return false;
            }
            if (strArray[0].Length != 2 || strArray[1].Length != 5)
            {
                return false;
            }
            int iAddress = 0;
            try
            {
                iAddress = int.Parse(strArray[0]);
            }
            catch
            {
                return false;
            }
            if (iAddress < 0)
            {
                return false;
            }
            if (strArray[1][0] != 'X' && strArray[1][0] != 'Y' && strArray[1][0] != 'R'
                && strArray[1][0] != 'T' && strArray[1][0] != 'C' && strArray[1][0] != 'L')
            {
                return false;
            }
            return true;
        }
        public override PLCResponse GetWord(string strAddress, PLCDataType dataType, ref string strValue)
        {
            if (bInitOK == false)
            {
                return PLCResponse.INITFAIL;
            }
            string[] strArrage;
            if (DAddressIsOK(strAddress, out strArrage) == false)
            {
                return PLCResponse.ADDWRONG;
            }
            return GetWordValue(strArrage, dataType, out strValue);
        }
        public override PLCResponse SetWord(string strAddress, PLCDataType dataType,string strValue)
        {
            if (bInitOK == false)
            {
                return PLCResponse.INITFAIL;
            }
            string[] strArrage;
            if (DAddressIsOK(strAddress, out strArrage) == false)
            {
                return PLCResponse.ADDWRONG;
            }
            return SetWordValue(strArrage, dataType,strValue);
        }
        internal PLCResponse GetWordValue(string[] strArray, PLCDataType dataType, out string strValue)
        {
            strValue = "";
            HiPerfTimer timeM = new HiPerfTimer();
            string strCommand = "%" + strArray[0] + "#" + strArray[1] + strArray[2] + strArray[3]+ strArray[4];
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "\r";
            string strMessage = strCommand + strFcs + strEndCode;
            string strRecieve;
            lock (objLock)
            {
                #region 通信
                try
                {
                    serialPort.ReadExisting();
                    System.Threading.Thread.Sleep(1);
                    serialPort.Write(strMessage);
                    System.Threading.Thread.Sleep(1);
                    timeM.Start();
                    strRecieve = serialPort.ReadExisting();
                    while (true)
                    {
                        try
                        {
                            strRecieve = strRecieve + serialPort.ReadExisting();
                            if (strRecieve.IndexOf("\r") > -1)
                            {
                                //strRecieve = strRecieve.Trim();
                                if (strRecieve.IndexOf("%") < 0 || strRecieve.IndexOf("$") < 0 || strRecieve.Length != 17)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 14);
                                string strFcsReturn = strRecieve.Substring(14, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                strValue = strRecieve.Substring(6, 8);
                                strValue = ConvertMesToValue(strValue, dataType);
                                break;
                            }
                            if (timeM.TimeUp(0.5))
                            {
                                return PLCResponse.TIMEOUT;
                            }
                        }
                        catch//(Exception ex)
                        {
                            return PLCResponse.OTHERS;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch
                {
                    return PLCResponse.OTHERS;
                }
                #endregion
            }
            return PLCResponse.SUCCESS;
        }
        internal PLCResponse SetWordValue(string[] strArray, PLCDataType dataType, string strValue)
        {
            //strValue = "";
            HiPerfTimer timeM = new HiPerfTimer();
            strArray[1]=strArray[1].Replace('R', 'W');
            string strMessageValue = ConvertValueToMessage(strValue, dataType);
            string strCommand = "";
            if (dataType == PLCDataType.BIN16 || dataType == PLCDataType.HEX16 || dataType == PLCDataType.BIT16 || (dataType == PLCDataType.STRING && strValue.Length <= 2))
            {
                strCommand = "%" + strArray[0] + "#" + strArray[1] + strArray[2] + strArray[3] + strArray[4] + strMessageValue + strMessageValue;
            }
            else
            {
                strCommand = "%" + strArray[0] + "#" + strArray[1] + strArray[2] + strArray[3] + strArray[4] + strMessageValue;
            }
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "\r";
            string strMessage = strCommand + strFcs + strEndCode;
            string strRecieve;
            lock (objLock)
            {
                #region 通信
                try
                {
                    serialPort.ReadExisting();
                    serialPort.Write(strMessage);
                    timeM.Start();
                    strRecieve = serialPort.ReadExisting();
                    while (true)
                    {
                        try
                        {
                            strRecieve = strRecieve + serialPort.ReadExisting();
                            if (strRecieve.IndexOf("\r") > -1)
                            {
                                //strRecieve = strRecieve.Trim();
                                if (strRecieve.IndexOf("%") < 0 || strRecieve.IndexOf("$") < 0 || strRecieve.Length !=9)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 6);
                                string strFcsReturn = strRecieve.Substring(6, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                break;
                            }
                            if (timeM.TimeUp(0.5))
                            {
                                return PLCResponse.TIMEOUT;
                            }
                        }
                        catch//(Exception ex)
                        {
                            return PLCResponse.OTHERS;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch
                {
                    return PLCResponse.OTHERS;
                }
                #endregion
            }
            return PLCResponse.SUCCESS;
        }
        internal bool DAddressIsOK(string strAddress, out string[] strArray)
        {
            char[] charArray = new char[1];
            charArray[0] = '#';
            string[] strArrayTemp = strAddress.Split(charArray);
            strArray = new string[5];
            if (strArrayTemp.Length != 3)
            {
                
                return false;
            }
            if (strArrayTemp[0].Length != 2 || strArrayTemp[1].Length != 1)
            {
                return false;
            }
            int iAddress = 0;
            try
            {
                iAddress = int.Parse(strArrayTemp[0]);
            }
            catch
            {
                return false;
            }
            if (iAddress < 0)
            {
                return false;
            }
            if (strArrayTemp[1] != "X" && strArrayTemp[1] != "Y" && strArrayTemp[1] != "R"
                && strArrayTemp[1] != "D" && strArrayTemp[1] != "L" && strArrayTemp[1] != "F")
            {
                return false;
            }
            if (strArrayTemp[1] == "X" || strArrayTemp[1] == "Y" || strArrayTemp[1] == "R")
            {
                strArray[1] = "RCC";
                if (strArrayTemp[2].Length != 5)
                {
                    return false;
                }
                strArray[3] = strArrayTemp[2];
                int iTemp;
                if (int.TryParse(strArray[3], out iTemp))
                {
                    strArray[3] = iTemp.ToString("0000");
                    strArray[4] = (iTemp+1).ToString("0000");
                }
                else
                {
                    return false;
                }
                
            }
            if (strArrayTemp[1] == "D" || strArrayTemp[1] == "L" || strArrayTemp[1] == "F")
            {
                strArray[1] = "RD";
                if (strArrayTemp[2].Length != 5)
                {
                    return false;
                }
                strArray[3] = strArrayTemp[2];
                int iTemp;
                if (int.TryParse(strArray[3], out iTemp))
                {
                    strArray[4] = (iTemp+1).ToString("00000");
                }
                else
                {
                    return false;
                }
            }
            strArray[0] = strArrayTemp[0];
            strArray[2] = strArrayTemp[1];
            
            return true;
        }
        public override bool GetBitAddress(string strAddress,ref string strStationName,ref string strWordType,ref string strWordAddr,ref string strBitAddr)
        {
            string[] strArrage;
            if (BitAddressIsOK(strAddress, out strArrage) == false)
            {
                return false;
            }
            strStationName = strArrage[0];
            strWordType = strArrage[1].Substring(0, 1);
            strWordAddr = int.Parse(strArrage[1].Substring(1, 3)).ToString("00000");
            strBitAddr = Convert.ToInt16(strArrage[1].Substring(4, 1),16).ToString("00");
            return true;
        }
        public override PLCResponse GetWordS(string strAddressStart, string strAddressEnd, ref string strValue)
        {
            if (bInitOK == false)
            {
                return PLCResponse.INITFAIL;
            }
            string strMessage = "";
            int iWordCount = 0;
            if (WordsAddressIsOK(strAddressStart, strAddressEnd, out iWordCount, out strMessage) == false)
            {
                return PLCResponse.ADDWRONG;
            }
            return GetWordSValue(strMessage, iWordCount, out strValue);

        }
        internal bool WordsAddressIsOK(string strAddressStart, string strAddressEnd, out int iWordCount, out string strMessage)
        {
            strMessage = "";
            iWordCount = 0;
            string[] strArrage;
            char[] charArray = new char[1];
            charArray[0] = '#';
            if (DAddressIsOK(strAddressStart, out strArrage) == false)
            {
                return false;
            }
            strArrage = strAddressStart.Split(charArray);
            int iStartAddr = int.Parse(strArrage[2]);
            string strStartAddr;
            if (strArrage[1] == "X" || strArrage[1] == "Y" || strArrage[1] == "R")
            {
                strStartAddr = iStartAddr.ToString("0000");
            }
            else
            {
                strStartAddr = iStartAddr.ToString("00000");
            }
            if (DAddressIsOK(strAddressEnd, out strArrage) == false)
            {
                return false;
            }
            strArrage = strAddressEnd.Split(charArray);
            int iEndAddr = int.Parse(strArrage[2]);
            if (iStartAddr == iEndAddr)
            {
                iEndAddr = iStartAddr + 1;
            }
            string strEndAddr;
            if (strArrage[1] == "X" || strArrage[1] == "Y" || strArrage[1] == "R")
            {
                strEndAddr = iEndAddr.ToString("0000");
            }
            else
            {
                strEndAddr = iEndAddr.ToString("00000");
            }

            iWordCount = iEndAddr-iStartAddr+1;
            try
            {
                if (strArrage[1] == "X")
                {
                    strMessage = "%" + strArrage[0] + "#" + "RCC" + "X" + strStartAddr + strEndAddr;
                    
                }
                if (strArrage[1] == "Y")
                {
                    strMessage = "%" + strArrage[0] + "#" + "RCC" + "Y" + strStartAddr + strEndAddr;
                }
                if (strArrage[1] == "R")
                {
                    strMessage = "%" + strArrage[0] + "#" + "RCC" + "R" + strStartAddr + strEndAddr;
                }
                if (strArrage[1] == "D")
                {
                    strMessage = "%" + strArrage[0] + "#" + "RD" + "D" + strStartAddr + strEndAddr;
                }
                if (strArrage[1] == "L")
                {
                    strMessage = "%" + strArrage[0] + "#" + "RD" + "L" + strStartAddr + strEndAddr;
                }
                if (strArrage[1] == "F")
                {
                    strMessage = "%" + strArrage[0] + "#" + "RD" + "F" + strStartAddr + strEndAddr;
                }
            }
            catch
            {
                strMessage = "";
                return false;
            }
            return true;
        }
        internal PLCResponse GetWordSValue(string strMessage, int iWordCount, out string strValue)
        {
            
            strValue = "";
            string strCommand = strMessage;
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "\r";
            string strMessageSend = strCommand + strFcs + strEndCode;
            string strRecieve;
            HiPerfTimer timeM = new HiPerfTimer();
            lock (objLock)
            {
                #region 通信
                try
                {
                    serialPort.ReadExisting();
                    serialPort.Write(strMessageSend);
                    timeM.Start();
                    strRecieve = serialPort.ReadExisting();
                    while (true)
                    {
                        try
                        {
                            strRecieve = strRecieve + serialPort.ReadExisting();
                            if (strRecieve.IndexOf("\r") > -1)
                            {
                                //strRecieve = strRecieve.Trim();
                                if (strRecieve.IndexOf("%") < 0 || strRecieve.IndexOf("$") < 0 || strRecieve.Length != (9 + iWordCount*4))
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 6 + iWordCount*4);
                                string strFcsReturn = strRecieve.Substring(6 + iWordCount * 4, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                strValue = strRecieve.Substring(6, iWordCount * 4);
                                strValue = ConvertMesToBit(strValue, iWordCount);
                                break;
                            }
                            if (timeM.TimeUp(0.5))
                            {
                                return PLCResponse.TIMEOUT;
                            }
                        }
                        catch//(Exception ex)
                        {
                            return PLCResponse.OTHERS;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch
                {
                    return PLCResponse.OTHERS;
                }
                #endregion
            }
            return PLCResponse.SUCCESS;
        }
        protected string ConvertMesToBit(string strMessage, int iWordCount)
        {
            string strReturn = "";
            string strLowWord;
            Int16 iLowWord;
            string strTemp = "";
            for (int i = 0; i < iWordCount; i++)
            {
                strLowWord = strMessage.Substring(i * 4, 4);
                strLowWord = strLowWord.Substring(2, 2) + strLowWord.Substring(0, 2);
                iLowWord = Convert.ToInt16(strLowWord, 16);
                strTemp = Convert.ToString(iLowWord, 2);
                strTemp = strTemp.PadLeft(16, '0');
                char[] arr = strTemp.ToCharArray();
                Array.Reverse(arr);
                strTemp = "";
                foreach (char charTemp in arr)
                {
                    strTemp += charTemp.ToString();
                }
                strReturn = strReturn + strTemp;
            }

            return strReturn;
        }
        public override bool GetWordAddress(string strAddress, ref string strStationName, ref string strWordType, ref string strWordAddr)
        {
            char[] charArray = new char[1];
            charArray[0] = '#';
            string[] strArray = strAddress.Split(charArray);
            if (strArray.Length != 3)
            {
                return false;
            }
            if (strArray[0].Length != 2 || strArray[1].Length != 1 || strArray[2].Length != 5)
            {
                return false;
            }
            int iAddress = 0;
            try
            {
                iAddress = int.Parse(strArray[0]);
            }
            catch
            {
                return false;
            }
            if (iAddress < 0)
            {
                return false;
            }
            if ( strArray[1] != "D" && strArray[1] != "L" && strArray[1] != "F")
            {

                return false;
            }
            int iAddStart = int.Parse(strArray[2]);
            string strStartAdd = iAddStart.ToString("00000");
            strStationName = strArray[0];
            strWordType = strArray[1];
            strWordAddr = strStartAdd;
            return true;
        }
        public override PLCResponse GetWordSS(string strAddressStart, string strAddressEnd, ref string strValue)
        {
            if (bInitOK == false)
            {
                return PLCResponse.INITFAIL;
            }
            string strMessage = "";
            int iWordCount = 0;
            if (WordsAddressIsOK(strAddressStart, strAddressEnd, out iWordCount, out strMessage) == false)
            {
                return PLCResponse.ADDWRONG;
            }
            return GetWordSSValue(strMessage, iWordCount, out strValue);

        }
        internal PLCResponse GetWordSSValue(string strMessage, int iWordCount, out string strValue)
        {
            strValue = "";
            string strCommand = strMessage;
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "\r";
            string strMessageSend = strCommand + strFcs + strEndCode;
            string strRecieve;
            HiPerfTimer timeM = new HiPerfTimer();
            lock (objLock)
            {
                #region 通信
                try
                {
                    serialPort.ReadExisting();
                    serialPort.Write(strMessageSend);
                    timeM.Start();
                    strRecieve = serialPort.ReadExisting();
                    while (true)
                    {
                        try
                        {
                            strRecieve = strRecieve + serialPort.ReadExisting();
                            if (strRecieve.IndexOf("\r") > -1)
                            {
                                //strRecieve = strRecieve.Trim();
                                if (strRecieve.IndexOf("%") < 0 || strRecieve.IndexOf("$") < 0 || strRecieve.Length != (9 + iWordCount * 4))
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 6 + iWordCount * 4);
                                string strFcsReturn = strRecieve.Substring(6 + iWordCount * 4, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                strValue = strRecieve.Substring(6, iWordCount * 4);
                                strValue = ConvertMesToDWord(strValue, iWordCount);
                                break;
                            }
                            if (timeM.TimeUp(0.5))
                            {
                                return PLCResponse.TIMEOUT;
                            }
                        }
                        catch//(Exception ex)
                        {
                            return PLCResponse.OTHERS;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch
                {
                    return PLCResponse.OTHERS;
                }
                #endregion
            }
            return PLCResponse.SUCCESS;
        }
        protected string ConvertMesToDWord(string strMessage, int iWordCount)
        {
            string strReturn = "";
            string strLowWord;
            string strTemp = "";
            for (int i = 0; i < iWordCount; i++)
            {
                strLowWord = strMessage.Substring(i * 4, 4);
                strLowWord = strLowWord.Substring(2, 2) + strLowWord.Substring(0, 2);
                strTemp = strLowWord;
                strReturn = strReturn + strTemp;
            }

            return strReturn;
        }
    }
}
