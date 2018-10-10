using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using WorldGeneralLib.Hardware.Demo;
using WorldGeneralLib.Hardware.Googol;
using WorldGeneralLib.Hardware.AdvanTech;
using WorldGeneralLib.Hardware.LeadShine;
using WorldGeneralLib.Hardware.Omron.TypeNJ;
using WorldGeneralLib.Hardware.Omron.TypeNX1P;
using WorldGeneralLib.Hardware.Panasonic;
using WorldGeneralLib.Hardware.Yamaha;
using WorldGeneralLib.Hardware.Camera.Basler.BaslerComm;
using WorldGeneralLib.Hardware.Camera.ImagingSource.GigE;
using WorldGeneralLib.Hardware.CodeReader.Keyence.SR700;
using WorldGeneralLib.Hardware.Camera.Basler.Aca500M;

namespace WorldGeneralLib.Hardware
{
    public class PropertyGridFileItem : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)

            {

                // 可以打开任何特定的对话框  
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.AddExtension = false;

                if (dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    return dialog.FileName;
                }
            }
            return value;
        }

    }

    //[Serializable]
    [XmlInclude(typeof(HardwareData)), XmlInclude(typeof(DemoInputCardData)), XmlInclude(typeof(DemoOutputCardData)), XmlInclude(typeof(DemoInputOutputCardData)), XmlInclude(typeof(DemoMotionCardData)),
     XmlInclude(typeof(LeadShineInputCardData)), XmlInclude(typeof(LeadShineInputOutputCardData)), XmlInclude(typeof(LeadShineMotionCardData)), XmlInclude(typeof(LeadShineOutputCardData)),
     XmlInclude(typeof(GoogolInputCardData)), XmlInclude(typeof(GoogolInputOutputCardData)), XmlInclude(typeof(GoogolMotionCardData)), XmlInclude(typeof(GoogolOutputCardData)),
     XmlInclude(typeof(AdvanInputCardData)), XmlInclude(typeof(AdvanInputOutputCardData)), XmlInclude(typeof(AdvanMotionCardData)), XmlInclude(typeof(AdvanOutputCardData)),
     XmlInclude(typeof(PlcOmronTypeNJData)), XmlInclude(typeof(PlcOmronTypeNX1PData)), XmlInclude(typeof(PlcPanasonicData)), XmlInclude(typeof(YamahaRobotData)),
        XmlInclude(typeof(ImagingSourceGigEData)), XmlInclude(typeof(BaslerCommData)),XmlInclude(typeof(KeyenceSR700Data)), XmlInclude(typeof(Aca500MData))]
    public class HardwareData
    {
        //[NonSerialized]
        [CategoryAttribute("Base attribute"), DescriptionAttribute("Base attribute")]
        [Browsable(true)]
        [MonitoringDescription("Card Index")]
        public int Index { get; set; }

        [CategoryAttribute("Base attribute")]
        [ReadOnly(true)]
        [Browsable(true)]
        [MonitoringDescription("Hardware Vender")]
        public HardwareVender Vender { get; set; }

        [CategoryAttribute("Base attribute")]
        [ReadOnly(true)]
        [Browsable(true)]
        [MonitoringDescription("Hardware Type")]
        public HardwareType Type { get; set; }

        [CategoryAttribute("Base attribute")]
        [ReadOnly(true)]
        [Browsable(true)]
        [MonitoringDescription("Hardware Series")]
        public HardwareSeries Series { get; set; }

        [CategoryAttribute("Design"), DescriptionAttribute("Hardware Name")]
        [Browsable(true)]
        public string Name { get; set; }

        [CategoryAttribute("Design"), DescriptionAttribute("Hardware Text")]
        [Browsable(true)]
        public string Text { get; set; }

        virtual public void DataInit()
        {

        }
    }
}
