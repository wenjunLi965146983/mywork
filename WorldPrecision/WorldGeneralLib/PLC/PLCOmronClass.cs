using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    internal class PLCOmronClass : PLCBaseClass
    {
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
            return SetBitValue(strArrage, bOn);
        }
        internal PLCResponse GetBitValue(string[] strArray, out bool bOn)
        {
            bOn = false;
            HiPerfTimer timeM = new HiPerfTimer();
            string strCommand = "@" + strArray[0] + "FA000000000" + strArray[1];
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "*"+"\r";
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
                                if (strRecieve.Length != 29)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 25);
                                string strFcsReturn = strRecieve.Substring(25, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                if (strRecieve.Substring(23, 2) == "01")
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
            
            string strOn = "0";
            if (bOn)
            {
                strOn = "01";
            }
            else
            {
                strOn = "00";
            }

            string strCommand = "@" + strArray[0] + "FA000000000" + "0102"+strArray[1].Substring(4) +strOn;
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "*" + "\r";
            string strMessage = strCommand + strFcs + strEndCode;
            string strRecieve;
            HiPerfTimer timeM = new HiPerfTimer();
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
                                if (strRecieve.Length != 27)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 23);
                                string strFcsReturn = strRecieve.Substring(23, 2);
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
            if (strArray[0].Length != 2 || strArray[1].Length != 7)
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
            if (strArray[1][0] != 'C'&& strArray[1][0] != 'W')
            {

                return false;
            }
            int iAddStart = int.Parse(strArray[1].Substring(1, 4));
            string strStartAdd = iAddStart.ToString("X4"); ;
            int iStartBit = int.Parse(strArray[1].Substring(5, 2));
            string strStartBit = iStartBit.ToString("X2");
            if (strArray[1][0] == 'C')
            {
                strArray[1] = "010130" + strStartAdd + strStartBit + "0001";
            }
            if (strArray[1][0] == 'W')
            {
                strArray[1] = "010131" + strStartAdd + strStartBit + "0001";
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
        public override PLCResponse SetWord(string strAddress, PLCDataType dataType, string strValue)
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
            return SetWordValue(strArrage, dataType, strValue);
        }
        internal PLCResponse GetWordValue(string[] strArray, PLCDataType dataType, out string strValue)
        {
            strValue = "";
            
            string strCommand = "@" + strArray[0] + "FA000000000" + strArray[1];
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "*" + "\r";
            string strMessage = strCommand + strFcs + strEndCode;
            string strRecieve;
            HiPerfTimer timeM = new HiPerfTimer();
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
                                if (strRecieve.Length != 35)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 31);
                                string strFcsReturn = strRecieve.Substring(31, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                strValue = strRecieve.Substring(23, 8);
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
            
            string strMessageValue = ConvertValueToMessage(strValue, dataType);
            string strCommand = "@" + strArray[0] + "FA000000000" + "0102" + strArray[1].Substring(4) + strMessageValue;
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "*" + "\r";
            string strMessage = strCommand + strFcs + strEndCode;
            string strRecieve;
            HiPerfTimer timeM = new HiPerfTimer();
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
                                if (strRecieve.Length != 27)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, 23);
                                string strFcsReturn = strRecieve.Substring(23, 2);
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
            strArray = strAddress.Split(charArray);
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
            if (strArray[1] != "C" && strArray[1] != "W"&&strArray[1] != "D")
            {

                return false;
            }
            try
            {
                if (strArray[1] == "C")
                {
                    strArray[1] = "0101B0" + int.Parse(strArray[2]).ToString("X4") +"00" + "0002";
                }
                if (strArray[1] == "W")
                {
                    strArray[1] = "0101B1" + int.Parse(strArray[2]).ToString("X4") + "00" + "0002";
                }
                if (strArray[1] == "D")
                {
                    strArray[1] = "010182" + int.Parse(strArray[2]).ToString("X4") + "00" + "0002";
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        protected override string ConvertMesToValue(string strMessage, PLCDataType dataType)
        {
            string strReturn = "####";
            if (strMessage.Length != 8)
            {
                return "Error";
            }
            string strLowWord = strMessage.Substring(0, 4);
            string strDWord = strMessage.Substring(4, 4)+ strLowWord;
            Int16 iLowWord = Convert.ToInt16(strLowWord, 16);
            Int32 iDWord = Convert.ToInt32(strDWord, 16);
            //double dValueResult = Convert.ToDouble(strDWord,new FOR;
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
        protected override string ConvertValueToMessage(string strValue, PLCDataType dataType)
        {
            string strReturn = "00000000";
            string strTemp = "";
            try
            {
                if (dataType == PLCDataType.BIT16)
                {
                    Int16 iTemp16 = Convert.ToInt16(strValue, 2);
                    strTemp = iTemp16.ToString("X4");
                    strReturn = strTemp;
                }
                if (dataType == PLCDataType.BIT32)
                {
                    Int32 iTemp32 = Convert.ToInt32(strValue, 2);
                    strTemp = iTemp32.ToString("X8");
                    strReturn = strTemp.Substring(4,4) + strTemp.Substring(0, 4);
                }
                if (dataType == PLCDataType.BIN16)
                {
                    Int16 iTemp16 = Convert.ToInt16(strValue, 10);
                    strTemp = iTemp16.ToString("X4");
                    strReturn = strTemp;
                }
                if (dataType == PLCDataType.BIN32)
                {
                    Int32 iTemp32 = Convert.ToInt32(strValue, 10);
                    strTemp = iTemp32.ToString("X8");
                    strReturn = strTemp.Substring(4, 4) + strTemp.Substring(0, 4);
                }
                if (dataType == PLCDataType.HEX16)
                {
                    Int16 iTemp16 = Convert.ToInt16(strValue, 16);
                    strTemp = iTemp16.ToString("X4");
                    strReturn = strTemp;
                }
                if (dataType == PLCDataType.HEX32)
                {
                    Int32 iTemp32 = Convert.ToInt32(strValue, 16);
                    strTemp = iTemp32.ToString("X8");
                    strReturn = strTemp.Substring(4, 4) + strTemp.Substring(0, 4);
                }
                if (dataType == PLCDataType.DOUBLE)
                {
                    float f = Convert.ToSingle(strValue);
                    byte[] floatValues = BitConverter.GetBytes(f);
                    strTemp = floatValues[3].ToString("X2") + floatValues[2].ToString("X2") + floatValues[1].ToString("X2") + floatValues[0].ToString("X2");
                    strReturn = strReturn = strTemp.Substring(4, 4) + strTemp.Substring(0, 4);

                }
            }
            catch
            {
            }
            return strReturn;
        }
        public override bool GetBitAddress(string strAddress, ref string strStationName, ref string strWordType, ref string strWordAddr, ref string strBitAddr)
        {
            char[] charArray = new char[1];
            charArray[0] = '#';
            string[] strArray = strAddress.Split(charArray);
            if (strArray.Length != 2)
            {
                return false;
            }
            if (strArray[0].Length != 2 || strArray[1].Length != 7)
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
            if (strArray[1][0] != 'C' && strArray[1][0] != 'W')
            {

                return false;
            }
            int iAddStart = int.Parse(strArray[1].Substring(1, 4));
            string strStartAdd = iAddStart.ToString("00000");
            int iStartBit = int.Parse(strArray[1].Substring(5, 2));
            string strStartBit = iStartBit.ToString("00");
            strStationName = strArray[0];
            strWordType = strArray[1].Substring(0, 1);
            strWordAddr = strStartAdd;
            strBitAddr = strStartBit;
            return true;
        }
        public override PLCResponse GetWordS(string strAddressStart, string strAddressEnd,ref string strValue)
        {
            if (bInitOK == false)
            {
                return PLCResponse.INITFAIL;
            }
            string strMessage = "";
            int iWordCount = 0;
            if (WordsAddressIsOK(strAddressStart, strAddressEnd,out iWordCount, out strMessage) == false)
            {
                return PLCResponse.ADDWRONG;
            }
            return GetWordSValue(strMessage,iWordCount,out strValue);
            
        }
        internal bool WordsAddressIsOK(string strAddressStart,string strAddressEnd,out int iWordCount, out string strMessage)
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
            string strAddr = iStartAddr.ToString("X4");
            if (DAddressIsOK(strAddressEnd, out strArrage) == false)
            {
                return false;
            }
            strArrage = strAddressEnd.Split(charArray);
            iWordCount = int.Parse(strArrage[2]) - iStartAddr+1;
            string strWordCount = (iWordCount).ToString("X4");



            try
            {
                if (strArrage[1] == "C")
                {
                    strMessage = "@" + strArrage[0] + "FA000000000" + "0101B0" + strAddr + "00" + strWordCount;
                }
                if (strArrage[1] == "W")
                {
                    strMessage = "@" + strArrage[0] + "FA000000000" + "0101B1" + strAddr + "00" + strWordCount;
                }
                if (strArrage[1] == "D")
                {
                    strMessage = "@" + strArrage[0] + "FA000000000" + "010182" + strAddr + "00" + strWordCount;
                }
            }
            catch
            {
                strMessage = "";
                return false;
            }
            if (iWordCount < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        internal PLCResponse GetWordSValue(string strMessage, int iWordCount,out string strValue)
        {
            strValue = "";

            string strCommand = strMessage;
            string strFcs = CreateFcsCodeFun(strCommand);
            string strEndCode = "*" + "\r";
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
                                int iLenthNeed=27+iWordCount*4;
                                if (strRecieve.Length != iLenthNeed)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, iLenthNeed-4);
                                string strFcsReturn = strRecieve.Substring(iLenthNeed - 4, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                strValue = strRecieve.Substring(23, iWordCount*4);
                                strValue = ConvertMesToBit(strValue,iWordCount);
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
            string strReturn ="";
            string strLowWord; 
            Int16 iLowWord;
            string strTemp = "";
            for (int i = 0; i < iWordCount; i++)
            {
                strLowWord = strMessage.Substring(i*4, 4);
                iLowWord= Convert.ToInt16(strLowWord, 16);
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
            if (strArray[1] != "C" && strArray[1] != "W" && strArray[1] != "D")
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
            string strEndCode = "*" + "\r";
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
                                int iLenthNeed = 27 + iWordCount * 4;
                                if (strRecieve.Length != iLenthNeed)
                                {
                                    return PLCResponse.ERROR;
                                }
                                string strMessageReturn = strRecieve.Substring(0, iLenthNeed - 4);
                                string strFcsReturn = strRecieve.Substring(iLenthNeed - 4, 2);
                                strFcs = CreateFcsCodeFun(strMessageReturn);
                                if (strFcs != strFcsReturn)
                                {
                                    return PLCResponse.FCSERR;
                                }
                                strValue = strRecieve.Substring(23, iWordCount * 4);
                                //strValue = ConvertMesToBit(strValue, iWordCount);
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
    }
}
