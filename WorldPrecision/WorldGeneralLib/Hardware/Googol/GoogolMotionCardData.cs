using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

namespace WorldGeneralLib.Hardware.Googol
{
    public class GoogolMotionCardData : HardwareData
    {
        [Description("Configuration File"), Category("Base attribute"), EditorAttribute(typeof(PropertyGridFileItem), typeof(System.Drawing.Design.UITypeEditor))]
        [Browsable(true)]
        [MonitoringDescription("Configuration File")]
        public string Path { get; set; }
    }
}
