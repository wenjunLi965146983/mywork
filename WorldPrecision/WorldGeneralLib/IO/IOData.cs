using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace WorldGeneralLib.IO
{
    public class IOData
    {
        //public string strName = "";
        //public string strCardName = "";
        //public int iIndex = 0;
        //public bool bIgnore = false;
        //public string strText = "";
        //public string strRemark = "";

        [CategoryAttribute("Base attribute"), DescriptionAttribute("Base attribute")]
        [Browsable(true)]
        [MonitoringDescription("IO Name")]
        public string Name { get; set; }

        [CategoryAttribute("Base attribute"), DescriptionAttribute("Base attribute")]
        [Browsable(true)]
        [MonitoringDescription("Card Name")]
        public string CardName { get; set; }

        [CategoryAttribute("Base attribute"), DescriptionAttribute("Base attribute")]
        [Browsable(true)]
        [MonitoringDescription("IO Index")]
        public int Index { get; set; }

        [CategoryAttribute("Base attribute"), DescriptionAttribute("Base attribute")]
        [Browsable(true)]
        [MonitoringDescription("Ignore")]
        public bool Ignore { get; set; }

        [CategoryAttribute("Base attribute"), DescriptionAttribute("Base attribute")]
        [Browsable(true)]
        [MonitoringDescription("Inversion")]
        public bool Inversion { get; set; }

        [CategoryAttribute("Base attribute"), DescriptionAttribute("Base attribute")]
        [Browsable(true)]
        [MonitoringDescription("Text")]
        public string Text { get; set; }

        [CategoryAttribute("Base attribute"), DescriptionAttribute("Base attribute")]
        [Browsable(true)]
        [MonitoringDescription("Remark")]
        public string Remark { get; set; }

        public IOData()
        {
            Name = "";
            CardName = "";
            Index = 0;
            Ignore = false;
            Inversion = false;
            Text = "";
            Remark = "";
        }
    }
}
