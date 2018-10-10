using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;

namespace WorldGeneralLib.Hardware.Panasonic
{
    public class PlcPanasonicData : HardwareData
    {

        [CategoryAttribute("Communication")]
        [Browsable(true)]
        //[DefaultValue("COM1")]
        [MonitoringDescription("PortName")]
        public string PortName { get; set; }

        [CategoryAttribute("Communication")]
        [Browsable(true)]
        //[DefaultValue(9600)]
        [MonitoringDescription("BaudRate")]
        public int BaudRate { get; set; }

        [CategoryAttribute("Communication")]
        [Browsable(true)]
        //[DefaultValue(8)]
        [MonitoringDescription("DataBits")]
        public int DataBits { get; set; }

        [CategoryAttribute("Communication")]
        [Browsable(true)]
        //[DefaultValue(8)]
        [MonitoringDescription("Parity")]
        public Parity Parity { get; set; }


        [CategoryAttribute("Communication")]
        [Browsable(true)]
        [MonitoringDescription("StopBits")]
        public StopBits StopBits { get; set; }

        public PlcPanasonicData()
        {
            PortName = "COM1";
            BaudRate = 9600;
            DataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.None;
            Text = "New PLC";
        }
    }
}
