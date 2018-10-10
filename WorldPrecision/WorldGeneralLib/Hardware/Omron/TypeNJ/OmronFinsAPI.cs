using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmronFins.Net;

namespace WorldGeneralLib.Hardware.Omron.TypeNJ
{
    public class OmronFinsAPI
    {
        public EtherNetPLC omronethernetplc;
        public bool bConnectOmronPLC = false;
        private static readonly object readLock = new object();
        private static readonly object writeLock = new object();
        public OmronFinsAPI()
        {
            omronethernetplc = new EtherNetPLC(System.Net.Sockets.ProtocolType.Tcp);
        }
        public bool ConnectToOmronPLC(string ipaddress, short nNetid, string localIpAddress)
        {
            short bconnect = omronethernetplc.Link(ipaddress, nNetid, localIpAddress,500);
            if (bconnect == 0)
            {
                bConnectOmronPLC = true;
            }
            else
            {
                bConnectOmronPLC = false;
            }
            return bConnectOmronPLC;
        }
        public bool DisconnectToOmronPLC()
        {
            short bconnect = omronethernetplc.Close();
            if (bconnect == 0)
            {
                bConnectOmronPLC = false;
                return true;
            }
            else
            {
                //MessageBox.Show("断开出错！");
                bConnectOmronPLC = false;
                return false;
            }
        }
        public bool WriteString(PlcScanItems item, short ncount, string message)
        {
            if (item.DataType != DataType.STRING || string.IsNullOrEmpty(message))
            {
                return false;
            }

            short sRet = 0;
            short startAddress = short.Parse(item.Address);
            PlcMemory memoryType = item.AddressType;

            lock (readLock)
            {
                try
                {
                    sRet = omronethernetplc.WriteString(PlcMemory.DM, startAddress, ncount, message);
                    return sRet == 0 ? true : false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool WriteString(short startaddress, short ncount, string message)
        {
            short result;
            result = omronethernetplc.WriteString(PlcMemory.DM, startaddress, ncount, message);
            if (result == 0)
                return true;
            else
                return false;
        }

        public bool WriteMultiElement(PlcScanItems item, short ncount, short[] valuearray)
        {
            PlcMemory memorytype = item.AddressType;
            short startaddress = short.Parse(item.Address);
            short count = ncount;
            short result = 0;
            //lock (writeLock)
            lock (readLock)
            {
                result = omronethernetplc.WriteWords(memorytype, startaddress, ncount, valuearray);
                if (result == 0)
                    return true;
                else
                {
                    bConnectOmronPLC = false;
                    return false;
                }
            }
        }
        public bool WriteSingleElement(PlcScanItems item, object value)
        {
            PlcMemory memorytype = item.AddressType;
            string bitstartaddress = string.Empty;
            short startaddress = 0;
            if (item.DataType == DataType.BIT)
            {
                bitstartaddress = item.Address;
            }
            else
            {
                startaddress = short.Parse(item.Address);
            }
            short result = 0;
            //lock (writeLock)
            lock (readLock)
            {
                switch (item.DataType)
                {
                    case DataType.BIT:
                        bool boolvalue = (bool)value;
                        if (boolvalue)
                        {
                            result = omronethernetplc.SetBitState(memorytype, bitstartaddress.ToString(), BitState.ON);
                        }
                        else
                        {
                            result = omronethernetplc.SetBitState(memorytype, bitstartaddress.ToString(), BitState.OFF);
                        }
                        break;
                    case DataType.INT16:
                        Int16 int16value = Convert.ToInt16(value.ToString());
                        result = omronethernetplc.WriteInt16(memorytype, startaddress, int16value);
                        break;
                    case DataType.INT32:
                        Int32 int32value = Convert.ToInt32(value.ToString());
                        result = omronethernetplc.WriteInt32(memorytype, startaddress, int32value);
                        break;
                    case DataType.REAL:
                        float floatvalue = Convert.ToSingle(value.ToString());
                        result = omronethernetplc.WriteFloat(memorytype, startaddress, floatvalue);
                        break;
                    case DataType.UINT16:
                        UInt16 uint16value = Convert.ToUInt16(value.ToString());
                        result = omronethernetplc.WriteUint16(memorytype, startaddress, uint16value);
                        break;
                    case DataType.UINT32:
                        UInt32 uint32value = Convert.ToUInt32(value.ToString());
                        result = omronethernetplc.WriteUint32(memorytype, startaddress, uint32value);
                        break;
                    case DataType.STRING:
                        if ((value.ToString()).Length >= 64)
                        {
                            return false;
                        }
                        return WriteString(item, 32, value.ToString());
                }
                if (result == 0)
                    return true;
                else
                {
                    bConnectOmronPLC = false;
                    return false;
                }
            }
        }

        public bool ReadString(PlcScanItems item, short count, ref string message)
        {
            short result = 0;
            result = omronethernetplc.ReadString(PlcMemory.DM, short.Parse(item.Address), count, ref message);
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ReadString(short startaddress, short count, ref string message)
        {
            short result = 0;
            result = omronethernetplc.ReadString(PlcMemory.DM, startaddress, count, ref message);
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadMultiElement(PlcScanItems item, short ncount, ref short[] value)
        {
            PlcMemory memorytype = item.AddressType;
            short startaddress = short.Parse(item.Address);
            short count = ncount;
            short result = 0;
            lock (readLock)
            {
                result = omronethernetplc.ReadWords(memorytype, startaddress, ncount, out value);
                if (result == 0)
                    return true;
                else
                {
                    bConnectOmronPLC = false;
                    return false;
                }
            }
        }
        public bool ReadSingleElement(PlcScanItems item, ref object value)
        {
            PlcMemory memorytype = item.AddressType;
            string bitstartaddress = string.Empty;
            short startaddress = 0;
            if (item.DataType == DataType.BIT)
            {
                bitstartaddress = item.Address;
            }
            else
            {
                startaddress = short.Parse(item.Address);
            }
            short result = 0;
            lock (readLock)
            {
                switch (item.DataType)
                {
                    case DataType.BIT:
                        short shortvalue = 0;
                        result = omronethernetplc.GetBitState(memorytype, bitstartaddress, out shortvalue);
                        if (shortvalue == 1)
                            value = true;
                        else
                            value = false;
                        break;
                    case DataType.INT16:
                        Int16 int16value = 0;
                        result = omronethernetplc.ReadInt16(memorytype, startaddress, out int16value);
                        value = int16value;
                        break;
                    case DataType.INT32:
                        Int32 int32value = 0;
                        result = omronethernetplc.ReadInt32(memorytype, startaddress, out int32value);
                        value = int32value;
                        break;
                    case DataType.REAL:
                        float floatvalue = 0.0f;
                        result = omronethernetplc.ReadReal(memorytype, startaddress, out floatvalue);
                        value = floatvalue;
                        break;
                    case DataType.UINT16:
                        UInt16 uint16value = 0;
                        result = omronethernetplc.ReadUInt16(memorytype, startaddress, out uint16value);
                        value = uint16value;
                        break;
                    case DataType.UINT32:
                        UInt32 uint32value = 0;
                        result = omronethernetplc.ReadUint32(memorytype, startaddress, out uint32value);
                        value = uint32value;
                        break;
                }
                if (result == 0)
                    return true;
                else
                {
                    bConnectOmronPLC = false;
                    return false;
                }
            }
        }
    }
}
