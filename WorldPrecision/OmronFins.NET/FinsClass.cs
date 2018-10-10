using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmronFins.Net
{
    class FinsClass
    {
        ///// <summary>
        ///// 获取内存区码
        ///// </summary>
        ///// <param name="mr">寄存器类型</param>
        ///// <param name="mt">地址类型</param>
        ///// <returns></returns>
        ///// 
        internal byte pcNode, plcNode;

        private  byte GetMemoryCode(PlcMemory mr, MemoryType mt)
        {
            if (mt == MemoryType.Bit)
            {
                switch (mr)
                {
                    case PlcMemory.CIO:
                        return 0x30;
                    case PlcMemory.WR:
                        return 0x31;
                    case PlcMemory.DM:
                        return 0x02;
                    default:
                        return 0x00;
                }
            }
            else
            {
                switch (mr)
                {
                    case PlcMemory.CIO:
                        return 0xB0;
                    case PlcMemory.WR:
                        return 0xB1;
                    case PlcMemory.DM:
                        return 0x82;
                    default:
                        return 0x00;
                }
            }
        }
        /// <summary>
        /// PC请求连接的握手信号，第一次连接会分配PC节点号
        /// </summary>
        /// <returns></returns>
        internal  byte[] HandShake()
        {
            #region fins command
            byte[] array = new byte[20];
            array[0] = 0x46;
            array[1] = 0x49;
            array[2] = 0x4E;
            array[3] = 0x53;

            array[4] = 0;
            array[5] = 0;
            array[6] = 0;
            array[7] = 0x0C;

            array[8] = 0;
            array[9] = 0;
            array[10] = 0;
            array[11] = 0;

            array[12] = 0;
            array[13] = 0;
            array[14] = 0;
            array[15] = 0;//ERR？

            array[16] = 0;
            array[17] = 0;
            array[18] = 0;
            array[19] = 0;//TODO:ask for client and server node number, the client node will allocated automatically
            //array[19] = this.GetIPNode(lIP);//本机IP地址的末位
            #endregion fins command
            return array;
        }
        /// <summary>
        /// Fins读写指令生成
        /// </summary>
        /// <param name="rw">读写类型</param>
        /// <param name="mr">寄存器类型</param>
        /// <param name="mt">地址类型</param>
        /// <param name="ch">起始地址</param>
        /// <param name="offset">位地址：00-15,字地址则为00</param>
        /// <param name="cnt">地址个数,按位读写只能是1</param>
        /// <returns></returns>
        internal  byte[] FinsCmdTCP(RorW rw, PlcMemory mr, MemoryType mt, short ch, short offset, short cnt)
        {
            //byte[] array;
            //if (rw == RorW.Read)
            //    array = new byte[34];
            //else
            //    array = new byte[(int)(cnt * 2 + 33 + 1)];//长度是如何确定的在fins协议174页
            byte[] array = new byte[34];//写指令还有后面的写入数组需要拼接在一起！
            //TCP FINS header
            array[0] = 0x46;//F
            array[1] = 0x49;//I
            array[2] = 0x4E;//N
            array[3] = 0x53;//S

            array[4] = 0;//cmd length
            array[5] = 0;
            //指令长度从下面字节开始计算array[8]
            if (rw == RorW.Read)
            {
                array[6] = 0;
                array[7] = 0x1A;//26
            }
            else
            {
                //写数据的时候一个字占两个字节，而一个位只占一个字节
                if (mt == MemoryType.Word)
                {
                    array[6] = (byte)((cnt * 2 + 26) / 256);
                    array[7] = (byte)((cnt * 2 + 26) % 256);
                }
                else
                {
                    array[6] = 0;
                    array[7] = 0x1B;
                }
            }

            array[8] = 0;//frame command
            array[9] = 0;
            array[10] = 0;
            array[11] = 0x02;

            array[12] = 0;//err
            array[13] = 0;
            array[14] = 0;
            array[15] = 0;
            //command frame header
            array[16] = 0x80;//ICF
            array[17] = 0x00;//RSV
            array[18] = 0x02;//GCT, less than 8 network layers
            array[19] = 0x00;//DNA, local network

            array[20] = plcNode;//DA1
            array[21] = 0x00;//DA2, CPU unit
            array[22] = 0x00;//SNA, local network
            array[23] = pcNode;//SA1

            array[24] = 0x00;//SA2, CPU unit
            array[25] = 0xFF;
            //TODO：array[25] = Convert.ToByte(21);//SID//?????----------------------------------00-FF任意值

            //指令码
            if (rw == RorW.Read)
            {
                array[26] = 0x01;//cmdCode--0101
                array[27] = 0x01;
            }
            else
            {
                array[26] = 0x01;//write---0102
                array[27] = 0x02;
            }
            //地址
            //array[28] = (byte)mr;
            array[28] = GetMemoryCode(mr, mt);
            array[29] = (byte)(ch / 256);
            array[30] = (byte)(ch % 256);
            array[31] = (byte)offset;

            array[32] = (byte)(cnt / 256);
            array[33] = (byte)(cnt % 256);

            return array;
        }

        /// <summary>
        /// Fins读写指令生成
        /// </summary>
        /// <param name="rw">读写类型</param>
        /// <param name="mr">寄存器类型</param>
        /// <param name="mt">地址类型</param>
        /// <param name="ch">起始地址</param>
        /// <param name="offset">位地址：00-15,字地址则为00</param>
        /// <param name="cnt">地址个数,按位读写只能是1</param>
        /// <returns></returns>
        internal byte[] FinsCmdUDP(RorW rw, PlcMemory mr, MemoryType mt, short ch, short offset, short cnt, ref int iLength)
        {
            int index = 0;
            byte[] array = new byte[256];

            //UDP FINS header
            array[index++] = 0x80;              //ICF     - Display frame information
            array[index++] = 0x00;              //RSV    - Reserved by system
            array[index++] = 0x02;              //GCT    - Permissible number of gateways
            array[index++] = 0x00;             //DNA   - Destination network address
            array[index++] = plcNode;       //DA1    - Destination node address (PLC node)
            array[index++] = 0x00;              //DA2    - Destination unit address
            array[index++] = 0x00;              //SNA    - Source network address 
            array[index++] = pcNode;         //SA1     - Source node address (PC node)
            array[index++] = 0x00;              //SA2     - Source unit address
            array[index++] = 0x00;              //SID      - Service ID

            //FINS Command (read: 0101 / write: 0102)
            array[index++] = 0x01;                                                               //MRC - Main request code
            array[index++] = (rw==RorW.Read) ? (byte)0x01: (byte)0x02;  //SRC  - Sub-request code

            //FINS Parameter/data   Command parameters and send data.The data length depends on the MRC and SRC
            array[index++] = GetMemoryCode(mr, mt);     //I/O Memory area code (DM - 0x82 )
            array[index++] = (byte)(ch / 256);                     //Beginning address (High byte)
            array[index++] = (byte)(ch % 256);                   //Beginning address (Low byte)
            array[index++] = (byte)offset;                           //Bit address (00 - 15 / 00)

            array[index++] = (byte)(cnt / 256);                   //NO. of items (High byte)
            array[index++] = (byte)(cnt % 256);                 //NO. of items (low byte)

            iLength = index;
            return array;
        }
    }
}
