using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmronFins.Net
{
    /// <summary>
    /// 寄存器类型,十六进制表示形式
    /// </summary>
    public enum PlcMemory
    {
        CIO,
        WR,
        DM
    }

    /// <summary>
    /// 地址类型
    /// </summary>
    public enum MemoryType
    {
        Bit,
        Word
    }

    /// <summary>
    /// 数据类型,PLC字为16位数，最高位为符号位，负数表现形式为“取反加一”
    /// </summary>
    public enum DataType
    {
        BIT,
        INT16,
        UINT16,
        INT32,
        UINT32,
        REAL,
        STRING
    }

    /// <summary>
    /// bit位开关状态，on=1，off=0
    /// </summary>
    public enum BitState
    {
        ON = 1,
        OFF = 0
    }

    /// <summary>
    /// 区分指令的读写类型
    /// </summary>
    public enum RorW
    {
        Read,
        Write
    }
}
