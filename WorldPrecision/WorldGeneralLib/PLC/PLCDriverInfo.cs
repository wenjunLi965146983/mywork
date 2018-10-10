


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ComponentModel;
using System.Diagnostics;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.IO.Ports;

namespace WorldGeneralLib.PLC
{
    [Serializable]
    public enum PLCType
    {
        Panasonic=0,
        Omron=1
    };
    [Serializable]
    public class PLCDriverInfo
    {
        public PLCDriverInfo()
        {
            plcType = PLCType.Panasonic;
            PortName = "COM1";
            BaudRate = 9600;
            DataBits = 8;
            //PortAddress = 1;
            Parity = System.IO.Ports.Parity.None;
            StopBits = System.IO.Ports.StopBits.None;
        }
        [CategoryAttribute("PLC设定"),DescriptionAttribute("PLC品牌设定")]
        [Browsable(true)]
        public PLCType plcType { get; set; }
        //
        // 摘要:
        //     获取或设置通信端口，包括但不限于所有可用的 COM 端口。
        //
        // 返回结果:
        //     通信端口。默认值为 COM1。
        //
        // 异常:
        //   System.ArgumentException:
        //     System.IO.Ports.SerialPort.PortName 属性已设置为长度为零的值。- 或 -System.IO.Ports.SerialPort.PortName
        //     属性已设置为以“\\”开始的值。- 或 -端口名称无效。
        //
        //   System.ArgumentNullException:
        //     System.IO.Ports.SerialPort.PortName 属性已设置为 null。
        //
        //   System.InvalidOperationException:
        //     指定的端口已打开。
        [CategoryAttribute("PLC设定")]
        [Browsable(true)]
        [DefaultValue("COM1")]
        [MonitoringDescription("PortName")]
        public string PortName { get; set; }
        //
        // 摘要:
        //     获取或设置串行波特率。
        //
        // 返回结果:
        //     波特率。
        //
        // 异常:
        //   System.ArgumentOutOfRangeException:
        //     指定的波特率小于或等于零，或者大于设备所允许的最大波特率。
        //
        //   System.IO.IOException:
        //     此端口处于无效状态。- 或 -尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        [CategoryAttribute("PLC设定")]
        [Browsable(true)]
        [DefaultValue(9600)]
        [MonitoringDescription("BaudRate")]
        public int BaudRate { get; set; }
        //
        // 摘要:
        //     获取或设置每个字节的标准数据位长度。
        //
        // 返回结果:
        //     数据位长度。
        //
        // 异常:
        //   System.IO.IOException:
        //     此端口处于无效状态。- 或 -尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        //
        //   System.ArgumentOutOfRangeException:
        //     数据位的值小于 5 或大于 8。
        [CategoryAttribute("PLC设定")]
        [Browsable(true)]
        [DefaultValue(8)]
        [MonitoringDescription("DataBits")]
        public int DataBits { get; set; }
        //
        // 摘要:
        //     获取或设置奇偶校验检查协议。
        //
        // 返回结果:
        //     System.IO.Ports.Parity 值之一，表示奇偶校验检查协议。默认值为 None。
        //
        // 异常:
        //   System.IO.IOException:
        //     此端口处于无效状态。- 或 -尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        //
        //   System.ArgumentOutOfRangeException:
        //     传递的 System.IO.Ports.SerialPort.Parity 值不是 System.IO.Ports.Parity 枚举中的有效值。
        [CategoryAttribute("PLC设定")]
        [Browsable(true)]
        [DefaultValue(8)]
        //[MonitoringDescription("PortAddress")]
        //public int PortAddress { get; set; }
        ////
        //// 摘要:
        ////     获取或设置端口地址。
        ////
        //// 返回结果:
        ////     System.IO.Ports.Parity 值之一。
        ////
        //// 异常:
        ////   System.IO.IOException:
        ////     此端口处于无效状态。- 或 -尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     传递的 System.IO.Ports.SerialPort.Parity 值不是 System.IO.Ports.Parity 枚举中的有效值。
        //[CategoryAttribute("PLC设定")]
        //[Browsable(true)]
        [MonitoringDescription("Parity")]
        public Parity Parity { get; set; }
        //
        // 摘要:
        //     获取或设置每个字节的标准停止位数。
        //
        // 返回结果:
        //     System.IO.Ports.StopBits 值之一。
        //
        // 异常:
        //   System.ArgumentOutOfRangeException:
        //     System.IO.Ports.SerialPort.StopBits 值不是来自 System.IO.Ports.StopBits 枚举的值之一。
        //
        //   System.IO.IOException:
        //     此端口处于无效状态。- 或 -尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        [CategoryAttribute("PLC设定")]
        [Browsable(true)]
        [MonitoringDescription("StopBits")]
        public StopBits StopBits { get; set; }

        [CategoryAttribute("Design")]
        [Browsable(true)]
        [MonitoringDescription("PLC Name")]
        public string Name { get; set; }
    }
}
