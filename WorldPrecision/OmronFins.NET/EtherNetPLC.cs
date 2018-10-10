using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace OmronFins.Net
{
    public class EtherNetPLC
    {
        public EthernetBase ethernetbase;
        private FinsClass finsClass = new FinsClass();
        private ProtocolType protocolType;
        /// <summary>
        /// PLC节点号，调试方法，一般不需要使用
        /// </summary>
        /// 
        public string PLCNode
        {
            get { return ethernetbase.plcNode.ToString(); }
        }

        /// <summary>
        /// PC节点号，调试方法，一般不需要使用
        /// </summary>
        public string PCNode
        {
            get { return ethernetbase.pcNode.ToString(); }
        }

        /// <summary>
        /// 实例化PLC操作对象
        /// </summary>
        public EtherNetPLC(ProtocolType protocolType)
        {
            this.protocolType = protocolType;
        }

        /// <summary>
        /// 与PLC建立TCP/UDP连接
        /// </summary>
        /// <param name="rIP">PLC的IP地址</param>
        /// <param name="rPort">端口号，默认9600</param>
        /// /// <param name="lIP">本地IP（TCP协议下不需要）</param>
        /// <param name="timeOut">超时时间，默认3000毫秒</param>
        /// <returns></returns>
        public short Link(string rIP, short rPort, string lIP, short timeOut = 3000)
        {
            try
            {
                int iRet = -1;
                ethernetbase = new EthernetBase();
                if (ethernetbase.PingCheck(rIP, timeOut))
                {
                    ethernetbase.protocolType = this.protocolType;

                    if (protocolType == ProtocolType.Tcp)
                    {
                        #region TCP
                        ethernetbase.Client.Connect(rIP, (int)rPort);
                        ethernetbase.Client.Client.IOControl(IOControlCode.KeepAliveValues, KeepAlive(1, 100, 10), null);
                        ethernetbase.Stream = ethernetbase.Client.GetStream();
                        Thread.Sleep(10);
                        byte[] sd = finsClass.HandShake();
                        iRet = ethernetbase.SendData(sd, sd.Length);
                        if (iRet != 0)
                        {
                            return -1;
                        }
                        else
                        {
                            //开始读取返回信号
                            byte[] buffer = new byte[24];
                            if (ethernetbase.ReceiveData(buffer) != 0)
                            {
                                return -1;
                            }
                            else
                            {
                                if (buffer[15] != 0)//TODO:这里的15号是不是ERR信息暂时不能完全肯定
                                    return -1;
                                else
                                {
                                    ethernetbase.pcNode = buffer[19];
                                    ethernetbase.plcNode = buffer[23];
                                    finsClass.pcNode = ethernetbase.pcNode;
                                    finsClass.plcNode = ethernetbase.plcNode;
                                    return 0;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region UDP
                        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                        ethernetbase.remoteIP = new IPEndPoint(IPAddress.Parse(rIP), rPort);
                        ethernetbase.udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        ethernetbase.udpSocket.ReceiveTimeout = 1000 * 3;
                        ethernetbase.Remote = (EndPoint)sender;
                        Thread.Sleep(10);
                        // iRet = ethernetbase.SendHandShakeData(finsClass.HandShake());

                        string strpcNode = lIP.Split('.')[3];
                        ethernetbase.pcNode = (byte)Convert.ToInt16(strpcNode);
                        ethernetbase.plcNode = 0x01;
                        finsClass.pcNode = ethernetbase.pcNode;
                        finsClass.plcNode = ethernetbase.plcNode;
                        return 0;
                        #endregion
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }

        /// <summary>
        /// 关闭PLC操作对象的TCP连接
        /// </summary>
        /// <returns></returns>
        public short Close()
        {
            try
            {
                if(protocolType == ProtocolType.Tcp)
                {
                    ethernetbase.Stream.Close();
                    ethernetbase.Client.Close();
                }
                else
                {
                    ethernetbase.udpSocket.Close();
                }

                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private byte[] KeepAlive(int onOff, int keepAliveTime, int keepAliveInterval)
        {
            byte[] buffer = new byte[12];
            BitConverter.GetBytes(onOff).CopyTo(buffer, 0);
            BitConverter.GetBytes(keepAliveTime).CopyTo(buffer, 4);
            BitConverter.GetBytes(keepAliveInterval).CopyTo(buffer, 8);
            return buffer;
        }
        /// <summary>
        /// 读取字符串，cnt是字符串strtemp长度的一半strtemp长度19，cnt为10；
        /// </summary>
        /// <param name="mr">寄存器类型</param>
        /// <param name="ch">开始地址</param>
        /// <param name="cnt">地址长度</param>
        /// <param name="strTemp">返回的字符串</param>
        /// <returns>成功失败</returns>
        public short ReadString(PlcMemory mr, short ch, short cnt, ref string strTemp)
        {
            byte[] reData = new byte[cnt * 2];//储存读取到的数据

            try
            {
                int num = (int)(30 + cnt * 2);//接收数据(Text)的长度,字节数
                byte[] buffer = new byte[num];//用于接收数据的缓存区大小
                if(protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] array = finsClass.FinsCmdTCP(RorW.Read, mr, MemoryType.Word, ch, 00, cnt);
                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                    {
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    }
                        
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            for (int i = 0; i < cnt; i++)
                            {
                                //返回的数据从第30字节开始储存的,
                                //PLC每个字占用两个字节，且是高位在前，这和微软的默认低位在前不同
                                //因此无法直接使用，reData[i] = BitConverter.ToInt16(buffer, 30 + i * 2);
                                //先交换了高低位的位置，然后再使用BitConverter.ToInt16转换
                                byte[] temp = new byte[] { buffer[30 + i * 2 + 1], buffer[30 + i * 2] };
                                reData[i * 2] = temp[0];
                                reData[i * 2 + 1] = temp[1];
                                strTemp = System.Text.Encoding.ASCII.GetString(reData);
                            }
                            return 0;
                        }
                        else
                        {
                            return -3;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if(protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] array = finsClass.FinsCmdUDP(RorW.Read, mr, MemoryType.Word, ch, 00, cnt, ref length);
                    if (ethernetbase.SendData(array, length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（读数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x01)
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            for (int i = 0; i < cnt; i++)
                            {
                                //返回的数据从第14字节开始储存的,
                                //PLC每个字占用两个字节，且是高位在前，这和微软的默认低位在前不同
                                //因此无法直接使用，reData[i] = BitConverter.ToInt16(buffer, 30 + i * 2);
                                //先交换了高低位的位置，然后再使用BitConverter.ToInt16转换
                                byte[] temp = new byte[] { buffer[14 + i * 2 + 1], buffer[14 + i * 2] };
                                reData[i * 2] = temp[0];
                                reData[i * 2 + 1] = temp[1];
                                strTemp = System.Text.Encoding.ASCII.GetString(reData);
                            }
                            return 0;
                        }
                        else
                        {
                            return -3;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 读值方法（多个连续值）
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址</param>
        /// <param name="cnt">地址个数</param>
        /// <param name="reData">返回值</param>
        /// <returns></returns>
        public short ReadWords(PlcMemory mr, short ch, short cnt, out short[] reData)
        {
            reData = new short[(int)(cnt)];//储存读取到的数据

            try
            {
                int num = (int)(30 + cnt * 2);//接收数据(Text)的长度,字节数
                byte[] buffer = new byte[num];//用于接收数据的缓存区大小

                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] array = finsClass.FinsCmdTCP(RorW.Read, mr, MemoryType.Word, ch, 00, cnt);
                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                    {
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    }

                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            for (int i = 0; i < cnt; i++)
                            {
                                //返回的数据从第30字节开始储存的,
                                //PLC每个字占用两个字节，且是高位在前，这和微软的默认低位在前不同
                                //因此无法直接使用，reData[i] = BitConverter.ToInt16(buffer, 30 + i * 2);
                                //先交换了高低位的位置，然后再使用BitConverter.ToInt16转换
                                byte[] temp = new byte[] { buffer[30 + i * 2 + 1], buffer[30 + i * 2] };
                                reData[i] = BitConverter.ToInt16(temp, 0);
                            }
                            return 0;
                        }
                        else
                        {
                            return -3;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] array = finsClass.FinsCmdUDP(RorW.Read, mr, MemoryType.Word, ch, 00, cnt, ref length);
                    if (ethernetbase.SendData(array, length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（读数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x01)
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            for (int i = 0; i < cnt; i++)
                            {
                                //返回的数据从第30字节开始储存的,
                                //PLC每个字占用两个字节，且是高位在前，这和微软的默认低位在前不同
                                //因此无法直接使用，reData[i] = BitConverter.ToInt16(buffer, 30 + i * 2);
                                //先交换了高低位的位置，然后再使用BitConverter.ToInt16转换
                                byte[] temp = new byte[] { buffer[30 + i * 2 + 1], buffer[30 + i * 2] };
                                reData[i] = BitConverter.ToInt16(temp, 0);
                            }
                            return 0;
                        }
                        else
                        {
                            return -3;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 读单个字方法
        /// </summary>
        /// <param name="mr"></param>
        /// <param name="ch"></param>
        /// <param name="reData"></param>
        /// <returns></returns>
        public short ReadWord(PlcMemory mr, short ch, out short reData)
        {
            short[] temp;
            reData = new short();
            short re = ReadWords(mr, ch, (short)1, out temp);
            if (re != 0)
                return -1;
            else
            {
                reData = temp[0];
                return 0;
            }
        }

        /// <summary>
        /// 写入字符串,
        /// </summary>
        /// <param name="mr"></param>
        /// <param name="ch">写入的起始地址</param>
        /// <param name="cnt">写入的地址范围</param>
        /// <param name="message">字符串</param>
        /// <returns></returns>
        public short WriteString(PlcMemory mr, short ch, short cnt, string message)
        {
            try
            {
                char[] c = new char[cnt * 2];
                for (int i = 0; i < message.Length; i++)
                {
                    c[i] = message[i];
                }
                byte[] b = System.Text.Encoding.ASCII.GetBytes(c);
                byte[] buffer = new byte[30];

                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] arrayhead = finsClass.FinsCmdTCP(RorW.Write, mr, MemoryType.Word, ch, 00, cnt);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[cnt * 2];
                    //转换写入值到wdata数组
                    for (int i = 0; i < cnt; i++)
                    {
                        wdata[i * 2] = b[i * 2 + 1];//转换为PLC的高位在前储存方式
                        wdata[i * 2 + 1] = b[i * 2];
                    }
                    //拼接写入数组
                    byte[] array = new byte[(int)(cnt * 2 + 34)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, 34);

                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                        else
                        {
                            return -3;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] arrayhead = finsClass.FinsCmdUDP(RorW.Write, mr, MemoryType.Word, ch, 00, cnt, ref length);//前18字节和读指令基本一致，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[cnt * 2];
                    //转换写入值到wdata数组
                    for (int i = 0; i < cnt; i++)
                    {
                        wdata[i * 2] = b[i * 2 + 1];//转换为PLC的高位在前储存方式
                        wdata[i * 2 + 1] = b[i * 2];
                    }
                    //拼接写入数组
                    byte[] array = new byte[(int)(cnt * 2 + arrayhead.Length)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, length);

                    if (ethernetbase.SendData(array, length + cnt * 2) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（写数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x02)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                    }

                    return -1;
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 写值方法（多个连续值）
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址</param>
        /// <param name="cnt">地址个数</param>
        /// <param name="inData">写入值</param>
        /// <returns></returns>
        public short WriteWords(PlcMemory mr, short ch, short cnt, short[] inData)
        {
            try
            {
                byte[] buffer = new byte[30];

                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] arrayhead = finsClass.FinsCmdTCP(RorW.Write, mr, MemoryType.Word, ch, 00, cnt);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(cnt * 2)];
                    //转换写入值到wdata数组
                    for (int i = 0; i < cnt; i++)
                    {
                        byte[] temp = BitConverter.GetBytes(inData[i]);
                        wdata[i * 2] = temp[1];//转换为PLC的高位在前储存方式
                        wdata[i * 2 + 1] = temp[0];
                    }
                    //拼接写入数组
                    byte[] array = new byte[(int)(cnt * 2 + 34)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, 34);

                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] arrayhead = finsClass.FinsCmdUDP(RorW.Write, mr, MemoryType.Word, ch, 00, cnt, ref length);
                    byte[] wdata = new byte[(int)(cnt * 2)];
                    //转换写入值到wdata数组
                    for (int i = 0; i < cnt; i++)
                    {
                        byte[] temp = BitConverter.GetBytes(inData[i]);
                        wdata[i * 2] = temp[1];//转换为PLC的高位在前储存方式
                        wdata[i * 2 + 1] = temp[0];
                    }
                    //拼接写入数组
                    byte[] array = new byte[(int)(cnt * 2 + arrayhead.Length)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, length);

                    if (ethernetbase.SendData(array, length+cnt*2) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（写数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x02)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                    }
                    return -1;
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 写32有符号整形数值
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址</param>
        /// <param name="inData">写入值</param>
        /// <returns></returns>
        public short WriteInt32(PlcMemory mr, short ch, Int32 inData)
        {
            try
            {
                byte[] buffer = new byte[30];

                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] arrayhead = finsClass.FinsCmdTCP(RorW.Write, mr, MemoryType.Word, ch, 00, 2);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(2 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    wdata[2] = temp[3];//转换为PLC的高位在前储存方式
                    wdata[3] = temp[2];

                    //拼接写入数组
                    byte[] array = new byte[(int)(2 * 2 + 34)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, 34);

                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] arrayhead = finsClass.FinsCmdUDP(RorW.Write, mr, MemoryType.Word, ch, 00, 2,ref length);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(2 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    wdata[2] = temp[3];//转换为PLC的高位在前储存方式
                    wdata[3] = temp[2];

                    //拼接写入数组
                    byte[] array = new byte[(int)(2 * 2 + arrayhead.Length)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, length);

                    if (ethernetbase.SendData(array, length + 4) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（写数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x02)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                    }
                    return -1;
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 写32位浮点数值
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址</param>
        /// <param name="inData">写入值</param>
        /// <returns></returns>
        public short WriteFloat(PlcMemory mr, short ch, float inData)
        {
            try
            {
                byte[] buffer = new byte[30];

                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] arrayhead = finsClass.FinsCmdTCP(RorW.Write, mr, MemoryType.Word, ch, 00, 2);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(2 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    wdata[2] = temp[3];//转换为PLC的高位在前储存方式
                    wdata[3] = temp[2];
                    //拼接写入数组
                    byte[] array = new byte[(int)(2 * 2 + 34)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, 34);

                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] arrayhead = finsClass.FinsCmdUDP(RorW.Write, mr, MemoryType.Word, ch, 00, 2, ref length);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(2 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    wdata[2] = temp[3];//转换为PLC的高位在前储存方式
                    wdata[3] = temp[2];
                    //拼接写入数组
                    byte[] array = new byte[(int)(2 * 2 + arrayhead.Length)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, length);

                    if (ethernetbase.SendData(array, length + 4) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（写数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x02)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                    }
                    return -1;
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 写32位UINT32数值
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址</param>
        /// <param name="inData">写入值</param>
        /// <returns></returns>
        public short WriteUint32(PlcMemory mr, short ch, UInt32 inData)
        {
            try
            {
                byte[] buffer = new byte[30];

                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] arrayhead = finsClass.FinsCmdTCP(RorW.Write, mr, MemoryType.Word, ch, 00, 2);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(2 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    wdata[2] = temp[3];//转换为PLC的高位在前储存方式
                    wdata[3] = temp[2];
                    //拼接写入数组
                    byte[] array = new byte[(int)(2 * 2 + 34)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, 34);

                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] arrayhead = finsClass.FinsCmdUDP(RorW.Write, mr, MemoryType.Word, ch, 00, 2, ref length);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(2 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    wdata[2] = temp[3];//转换为PLC的高位在前储存方式
                    wdata[3] = temp[2];
                    //拼接写入数组
                    byte[] array = new byte[(int)(2 * 2 + arrayhead.Length)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, length);

                    if (ethernetbase.SendData(array, length + 4) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（写数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x02)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                    }
                    return -1;
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 写16位UINT16数值
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址</param>
        /// <param name="inData">写入值</param>
        /// <returns></returns>
        public short WriteUint16(PlcMemory mr, short ch, UInt16 inData)
        {
            try
            {
                byte[] buffer = new byte[30];

                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] arrayhead = finsClass.FinsCmdTCP(RorW.Write, mr, MemoryType.Word, ch, 00, 1);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(1 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    byte[] array = new byte[(int)(1 * 2 + 34)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, 34);

                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] arrayhead = finsClass.FinsCmdUDP(RorW.Write, mr, MemoryType.Word, ch, 00, 1, ref length);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(1 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    //拼接写入数组
                    byte[] array = new byte[(int)(1 * 2 + arrayhead.Length)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, length);
                    

                    if (ethernetbase.SendData(array, length + 2) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（写数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x02)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                    }
                    return -1;
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 写16位INT16有符号数值
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址</param>
        /// <param name="inData">写入值</param>
        /// <returns></returns>
        public short WriteInt16(PlcMemory mr, short ch, Int16 inData)
        {
            try
            {
                byte[] buffer = new byte[30];

                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] arrayhead = finsClass.FinsCmdTCP(RorW.Write, mr, MemoryType.Word, ch, 00, 1);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(1 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    byte[] array = new byte[(int)(1 * 2 + 34)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, 34);

                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] arrayhead = finsClass.FinsCmdUDP(RorW.Write, mr, MemoryType.Word, ch, 00, 1, ref length);//前34字节和读指令基本一直，还需要拼接下面的输入数据数组
                    byte[] wdata = new byte[(int)(1 * 2)];
                    byte[] temp = BitConverter.GetBytes(inData);
                    wdata[0] = temp[1];//转换为PLC的高位在前储存方式
                    wdata[1] = temp[0];
                    byte[] array = new byte[(int)(1 * 2 + arrayhead.Length)];
                    arrayhead.CopyTo(array, 0);
                    wdata.CopyTo(array, length);

                    if (ethernetbase.SendData(array, length + 2) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（写数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x02)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                    }
                    return -1;
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 写单个字方法
        /// </summary>
        /// <param name="mr"></param>
        /// <param name="ch"></param>
        /// <param name="inData"></param>
        /// <returns></returns>
        public short WriteWord(PlcMemory mr, short ch, short inData)
        {
            short[] temp = new short[] { inData };
            short re = WriteWords(mr, ch, (short)1, temp);
            if (re != 0)
                return -1;
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 读值方法-按位bit（单个）
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">地址000.00</param>
        /// <param name="bs">返回开关状态枚举EtherNetPLC.BitState，0/1</param>
        /// <returns></returns>
        public short GetBitState(PlcMemory mr, string ch, out short bs)
        {
            bs = new short();
            try
            {
                byte[] buffer = new byte[31];//用于接收数据的缓存区大小
                short cnInt = short.Parse(ch.Split('.')[0]);
                short cnBit = short.Parse(ch.Split('.')[1]);

                if(protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] array = finsClass.FinsCmdTCP(RorW.Read, mr, MemoryType.Bit, cnInt, cnBit, 1);
                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            bs = (short)buffer[30];
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if(protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] array = finsClass.FinsCmdUDP(RorW.Read, mr, MemoryType.Bit, cnInt, cnBit, 1,ref length);
                    if (ethernetbase.SendData(array, length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }
                    //命令头为0XC0 并且命令类型为0101（读数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x01)
                    {
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            bs = (short)buffer[14];
                            return 0;
                        }
                    }
                    return -1;
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        /// <summary>
        /// 写值方法-按位bit（单个）
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">地址000.00</param>
        /// <param name="bs">开关状态枚举EtherNetPLC.BitState，0/1</param>
        /// <returns></returns>
        public short SetBitState(PlcMemory mr, string ch, BitState bs)
        {
            try
            {
                byte[] buffer = new byte[30];
                short cnInt = short.Parse(ch.Split('.')[0]);
                short cnBit = short.Parse(ch.Split('.')[1]);

                if(protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] arrayhead = finsClass.FinsCmdTCP(RorW.Write, mr, MemoryType.Bit, cnInt, cnBit, 1);
                    byte[] array = new byte[35];
                    arrayhead.CopyTo(array, 0);
                    array[34] = (byte)bs;

                    if (ethernetbase.SendData(array, array.Length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }
                    //命令返回成功，继续查询是否有错误码，然后在读取数据
                    bool succeed = true;
                    if (buffer[11] == 3)
                        succeed = ErrorCode.CheckHeadError(buffer[15]);
                    if (succeed)//no header error
                    {
                        //endcode为fins指令的返回错误码
                        if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if(protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] arrayhead = finsClass.FinsCmdUDP(RorW.Write, mr, MemoryType.Bit, cnInt, cnBit, 1,ref length);
                    byte[] array = new byte[arrayhead.Length+1];
                    arrayhead.CopyTo(array, 0);
                    array[length] = (byte)bs;

                    if (ethernetbase.SendData(array, length+1) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（写数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x02)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回0
                            return 0;
                        }
                    }
                    return -1;
                }
                else
                {
                    return -1;
                }
                #endregion
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 读一个浮点数的方法，单精度，在PLC中占两个字
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址，会读取两个连续的地址，因为单精度在PLC中占两个字</param>
        /// <param name="reData">返回一个float型</param>
        /// <returns></returns>
        public short ReadReal(PlcMemory mr, short ch, out float reData)
        {
            reData = new float();
            try
            {
                int num = (int)(30 + 2 * 2);//接收数据(Text)的长度,字节数
                byte[] buffer = new byte[num];//用于接收数据的缓存区大小
                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] array = finsClass.FinsCmdTCP(RorW.Read, mr, MemoryType.Word, ch, 00, 2);
                    if (ethernetbase.SendData(array, array.Length) == 0)
                    {
                        if (ethernetbase.ReceiveData(buffer) == 0)
                        {
                            //命令返回成功，继续查询是否有错误码，然后在读取数据
                            bool succeed = true;
                            if (buffer[11] == 3)
                                succeed = ErrorCode.CheckHeadError(buffer[15]);
                            if (succeed)//no header error
                            {
                                //endcode为fins指令的返回错误码
                                if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                                {
                                    //完全正确的返回，开始读取返回的具体数值
                                    byte[] temp = new byte[] { buffer[30 + 1], buffer[30], buffer[30 + 3], buffer[30 + 2] };
                                    reData = BitConverter.ToSingle(temp, 0);
                                    return 0;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] array = finsClass.FinsCmdUDP(RorW.Read, mr, MemoryType.Word, ch, 00, 2, ref length);
                    if (ethernetbase.SendData(array, length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（读数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x01)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            byte[] temp = new byte[] { buffer[14 + 1], buffer[14], buffer[14 + 3], buffer[14 + 2] };
                            reData = BitConverter.ToSingle(temp, 0);
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 读一个整数的方法，，在PLC中占2个字
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址，会读取两个连续的地址，因为单精度在PLC中占两个字</param>
        /// <param name="reData">返回一个有符号32位整型</param>
        /// <returns></returns>
        public short ReadInt32(PlcMemory mr, short ch, out Int32 reData)
        {
            reData = new Int32();
            try
            {
                int num = (int)(30 + 2 * 2);//接收数据(Text)的长度,字节数
                byte[] buffer = new byte[num];//用于接收数据的缓存区大小
                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] array = finsClass.FinsCmdTCP(RorW.Read, mr, MemoryType.Word, ch, 00, 2);
                    if (ethernetbase.SendData(array, array.Length) == 0)
                    {
                        if (ethernetbase.ReceiveData(buffer) == 0)
                        {
                            //命令返回成功，继续查询是否有错误码，然后在读取数据
                            bool succeed = true;
                            if (buffer[11] == 3)
                                succeed = ErrorCode.CheckHeadError(buffer[15]);
                            if (succeed)//no header error
                            {
                                //endcode为fins指令的返回错误码
                                if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                                {
                                    //完全正确的返回，开始读取返回的具体数值
                                    byte[] temp = new byte[] { buffer[30 + 1], buffer[30], buffer[30 + 3], buffer[30 + 2] };
                                    reData = BitConverter.ToInt32(temp, 0);
                                    return 0;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] array = finsClass.FinsCmdUDP(RorW.Read, mr, MemoryType.Word, ch, 00, 2, ref length);
                    if (ethernetbase.SendData(array, length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（读数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x01)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            byte[] temp = new byte[] { buffer[14 + 1], buffer[14], buffer[14 + 3], buffer[14 + 2] };
                            reData = BitConverter.ToInt32(temp, 0);
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 读一个32位无符号整数，，在PLC中占两个字
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址，会读取两个连续的地址，因为单精度在32位无符号占两个字</param>
        /// <param name="reData">返回一个无32位无符号整数整型</param>
        /// <returns></returns>
        public short ReadUint32(PlcMemory mr, short ch, out UInt32 reData)
        {
            reData = new UInt32();
            try
            {
                int num = (int)(30 + 2 * 2);//接收数据(Text)的长度,字节数
                byte[] buffer = new byte[num];//用于接收数据的缓存区大小
                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] array = finsClass.FinsCmdTCP(RorW.Read, mr, MemoryType.Word, ch, 00, 2);
                    if (ethernetbase.SendData(array, array.Length) == 0)
                    {
                        if (ethernetbase.ReceiveData(buffer) == 0)
                        {
                            //命令返回成功，继续查询是否有错误码，然后在读取数据
                            bool succeed = true;
                            if (buffer[11] == 3)
                                succeed = ErrorCode.CheckHeadError(buffer[15]);
                            if (succeed)//no header error
                            {
                                //endcode为fins指令的返回错误码
                                if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                                {
                                    //完全正确的返回，开始读取返回的具体数值
                                    byte[] temp = new byte[] { buffer[30 + 1], buffer[30], buffer[30 + 3], buffer[30 + 2] };
                                    reData = BitConverter.ToUInt32(temp, 0);
                                    return 0;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] array = finsClass.FinsCmdUDP(RorW.Read, mr, MemoryType.Word, ch, 00, 2, ref length);
                    if (ethernetbase.SendData(array, length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（读数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x01)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            byte[] temp = new byte[] { buffer[14 + 1], buffer[14], buffer[14 + 3], buffer[14 + 2] };
                            reData = BitConverter.ToUInt32(temp, 0);
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 读一个16位无符号整数，，在PLC中占1个字
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址，会读取两个连续的地址，因为16位无符号占1个字</param>
        /// <param name="reData">返回一个无16位无符号整数整型</param>
        /// <returns></returns>
        public short ReadUInt16(PlcMemory mr, short ch, out UInt16 reData)
        {
            reData = new UInt16();
            try
            {
                int num = (int)(30 + 2 * 1);//接收数据(Text)的长度,字节数
                byte[] buffer = new byte[num];//用于接收数据的缓存区大小
                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] array = finsClass.FinsCmdTCP(RorW.Read, mr, MemoryType.Word, ch, 00, 1);
                    if (ethernetbase.SendData(array, array.Length) == 0)
                    {
                        if (ethernetbase.ReceiveData(buffer) == 0)
                        {
                            //命令返回成功，继续查询是否有错误码，然后在读取数据
                            bool succeed = true;
                            if (buffer[11] == 3)
                                succeed = ErrorCode.CheckHeadError(buffer[15]);
                            if (succeed)//no header error
                            {
                                //endcode为fins指令的返回错误码
                                if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                                {
                                    //完全正确的返回，开始读取返回的具体数值
                                    byte[] temp = new byte[] { buffer[30 + 1], buffer[30] };
                                    reData = BitConverter.ToUInt16(temp, 0);
                                    return 0;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] array = finsClass.FinsCmdUDP(RorW.Read, mr, MemoryType.Word, ch, 00, 1, ref length);
                    if (ethernetbase.SendData(array, length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（读数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x01)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            byte[] temp = new byte[] { buffer[14 + 1], buffer[14] };
                            reData = BitConverter.ToUInt16(temp, 0);
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 读一个16位有符号整数，，在PLC中占1个字
        /// </summary>
        /// <param name="mr">地址类型枚举</param>
        /// <param name="ch">起始地址，会读取两个连续的地址，因为16位有符号占1个字</param>
        /// <param name="reData">返回一个16位有符号整数整型</param>
        /// <returns></returns>
        public short ReadInt16(PlcMemory mr, short ch, out Int16 reData)
        {
            reData = new Int16();
            try
            {
                int num = (int)(30 + 2 * 1);//接收数据(Text)的长度,字节数
                byte[] buffer = new byte[num];//用于接收数据的缓存区大小
                if (protocolType == ProtocolType.Tcp)
                {
                    #region TCP
                    byte[] array = finsClass.FinsCmdTCP(RorW.Read, mr, MemoryType.Word, ch, 00, 1);
                    if (ethernetbase.SendData(array, array.Length) == 0)
                    {
                        if (ethernetbase.ReceiveData(buffer) == 0)
                        {
                            //命令返回成功，继续查询是否有错误码，然后在读取数据
                            bool succeed = true;
                            if (buffer[11] == 3)
                                succeed = ErrorCode.CheckHeadError(buffer[15]);
                            if (succeed)//no header error
                            {
                                //endcode为fins指令的返回错误码
                                if (ErrorCode.CheckEndCode(buffer[28], buffer[29]))
                                {
                                    //完全正确的返回，开始读取返回的具体数值
                                    byte[] temp = new byte[] { buffer[30 + 1], buffer[30] };
                                    reData = BitConverter.ToInt16(temp, 0);
                                    return 0;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (protocolType == ProtocolType.Udp)
                {
                    #region UDP
                    int length = 0;
                    byte[] array = finsClass.FinsCmdUDP(RorW.Read, mr, MemoryType.Word, ch, 00, 1, ref length);
                    if(ethernetbase.SendData(array,length) != 0)
                    {
                        return -1;
                    }
                    if (ethernetbase.ReceiveData(buffer) != 0)
                    {
                        return -1;
                    }

                    //命令头为0XC0 并且命令类型为0101（读数据返回）
                    if (buffer[0] == 0xc0 && buffer[10] == 0x01 && buffer[11] == 0x01)
                    {
                        //是否正常反馈（0000）
                        if (ErrorCode.CheckEndCode(buffer[12], buffer[13]))
                        {
                            //完全正确的返回，开始读取返回的具体数值
                            byte[] temp = new byte[] { buffer[14 + 1], buffer[14] };
                            reData = BitConverter.ToInt16(temp, 0);
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}