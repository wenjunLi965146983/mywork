using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware
{
    public enum HardwareType
    {
        MotionCard,
        GEMotionCard,
        InputCard,
        OutputCard,
        InputOutputCard,
        AIOCard,
        PLC,
        Robot,
        Camera,
        CodeReader,
        LedController,
        Unknow
    }
    public enum HardwareSeries
    {
        Demo_MotionCard_Common,
        Demo_InputCard_Common,
        Demo_OutputCard_Common,
        Demo_InputOutputCard_Common,
        GoogolTech_MotionCard_Common,
        GoogolTech_InputCard_Common,
        GoogolTech_OutputCard_Common,
        GoogolTech_InputOutputCard_Common,
        AdvanTech_MotionCard_Common,
        AdvanTech_InputCard_Common,
        AdvanTech_OutputCard_Common,
        AdvanTech_InputOutputCard_Common,
        LeadShine_MotionCard_Common,
        LeadShine_MotionCard_Dmc1000,
        LeadShine_InputCard_Common,
        LeadShine_OutputCard_Common,
        LeadShine_InputOutputCard_Common,
        Panasonic_PLC_Common,
        Omron_PLC_NJ,
        Omron_PLC_NX1P,
        Siemens_PLC_S7200Smart,
        Yamaha_Robot_RCX340,
        Epson_Robot_Common,
        ImagingSource_Camera_GigE,
        Basler_Camera_Comm,
        Basler_Camera_Aca500M,
        Cognex_CodeReader_Comm,
        Keyence_CodeReader_SR700
    }
    public enum HardwareVender
    {
        Demo = 0,
        GoogolTech,
        AdvanTech,
        LeadShine,
        Omron,
        Panasonic,
        Siemens,
        Yamaha,
        Epson,
        Basler,
        Keyence,
        Cognex,
        ImagingSource
    }
}
