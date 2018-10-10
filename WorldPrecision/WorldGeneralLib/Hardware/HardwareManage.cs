using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware.Demo;
using WorldGeneralLib.Hardware.AdvanTech;
using WorldGeneralLib.Hardware.Googol;
using WorldGeneralLib.Hardware.LeadShine;
using WorldGeneralLib.Hardware.Omron;
using WorldGeneralLib.Hardware.Panasonic;
using WorldGeneralLib.Hardware.Omron.TypeNJ;
using WorldGeneralLib.Hardware.Omron.TypeNX1P;
using WorldGeneralLib.Hardware.Siemens.S7_200_Smart;
using WorldGeneralLib.Hardware.Yamaha;
using WorldGeneralLib.Hardware.Yamaha.RCX340;
using WorldGeneralLib.Hardware.Camera.ImagingSource;
using WorldGeneralLib.Hardware.Camera.ImagingSource.GigE;
using WorldGeneralLib.Hardware.CodeReader.Keyence.SR700;
using WorldGeneralLib.Hardware.Camera.Basler.Aca500M;

namespace WorldGeneralLib.Hardware
{
    public class HardwareManage
    {
        static public HardwareDoc docHardware;
        static public Dictionary<string, HardwareBase> dicHardwareDriver;
        static public Dictionary<string, Form> dicSettingForms = null;

        public static void LoadDoc()
        {
            docHardware = HardwareDoc.LoadObj();
        }
        public static void InitHardware()
        {
            if (null == dicSettingForms)
            {
                dicSettingForms = new Dictionary<string, Form>();
            }

            try
            {
                dicHardwareDriver = new Dictionary<string, HardwareBase>();
                foreach (KeyValuePair<string, HardwareData> item in docHardware.dicHardwareData)
                {
                    #region Demo
                    if (item.Value.Vender == HardwareVender.Demo)
                    {
                        if (item.Value.Series == HardwareSeries.Demo_InputCard_Common)
                        {
                            DemoInputCard demoCard = new DemoInputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                        if (item.Value.Series == HardwareSeries.Demo_InputOutputCard_Common)
                        {
                            DemoInputOutputCard demoCard = new DemoInputOutputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                        if (item.Value.Series == HardwareSeries.Demo_MotionCard_Common)
                        {
                            DemoMotionCard demoCard = new DemoMotionCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                        if (item.Value.Series == HardwareSeries.Demo_OutputCard_Common)
                        {
                            DemoOutputCard demoCard = new DemoOutputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                    }
                    #endregion
                    #region LEISAI
                    if (item.Value.Vender == HardwareVender.LeadShine)
                    {
                        if (item.Value.Series == HardwareSeries.LeadShine_InputCard_Common)
                        {
                            LeadShineInputCard demoCard = new LeadShineInputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                        if (item.Value.Series == HardwareSeries.LeadShine_OutputCard_Common)
                        {
                            LeadShineOutputCard demoCard = new LeadShineOutputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                        if (item.Value.Series == HardwareSeries.LeadShine_InputOutputCard_Common)
                        {
                            LeadShineInputOutputCard demoCard = new LeadShineInputOutputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                        if (item.Value.Series == HardwareSeries.LeadShine_MotionCard_Common)
                        {
                            LeadShineMotionCard demoCard = new LeadShineMotionCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                            dicSettingForms.Add(item.Value.Name, new FormLeadShineMotionCard((LeadShineMotionCardData)item.Value));
                        }
                        if (item.Value.Series == HardwareSeries.LeadShine_MotionCard_Dmc1000)
                        {
                            LeadShineMotionCardDmc1000 demoCard = new LeadShineMotionCardDmc1000();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                            dicSettingForms.Add(item.Value.Name, new FormLeadShineMotionCard((LeadShineMotionCardData)item.Value));
                        }
                    }
                    #endregion
                    #region GOOGOL
                    if (item.Value.Vender == HardwareVender.GoogolTech)
                    {
                        if (item.Value.Series == HardwareSeries.GoogolTech_MotionCard_Common)
                        {
                            GoogolMotionCard demoCard = new GoogolMotionCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);

                            dicSettingForms.Add(item.Value.Name, new FormGoogolSetting());
                        }
                    }
                    #endregion
                    #region ADVANCE
                    if (item.Value.Vender == HardwareVender.AdvanTech)
                    {
                        if (item.Value.Series == HardwareSeries.AdvanTech_InputCard_Common)
                        {
                            AdvanInputCard demoCard = new AdvanInputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                        if (item.Value.Series == HardwareSeries.AdvanTech_OutputCard_Common)
                        {
                            AdvanOutputCard demoCard = new AdvanOutputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }
                        if (item.Value.Series == HardwareSeries.AdvanTech_InputOutputCard_Common)
                        {
                            AdvanInputOutputCard demoCard = new AdvanInputOutputCard();
                            dicHardwareDriver.Add(item.Value.Name, demoCard);
                        }

                    }
                    #endregion
                    #region Panasonic
                    if (item.Value.Vender == HardwareVender.Panasonic)
                    {
                        if (item.Value.Series == HardwareSeries.Panasonic_PLC_Common)
                        {
                            PlcPanasonic plcPanasonic = new PlcPanasonic((PlcPanasonicData)item.Value);
                            dicHardwareDriver.Add(item.Value.Name, plcPanasonic);
                            dicSettingForms.Add(item.Value.Name, new FormPanasonic((PlcPanasonicData)item.Value));
                        }
                    }
                    #endregion
                    #region Omron
                    if (item.Value.Vender == HardwareVender.Omron)
                    {
                        if (item.Value.Series == HardwareSeries.Omron_PLC_NJ)
                        {
                            PlcOmronTypeNJ demoCard = new PlcOmronTypeNJ((PlcOmronTypeNJData)item.Value);
                            dicHardwareDriver.Add(item.Value.Name, demoCard);

                            dicSettingForms.Add(item.Value.Name, new FormOmronTypeNJ((PlcOmronTypeNJData)item.Value));
                        }
                        else if (item.Value.Series == HardwareSeries.Omron_PLC_NX1P)
                        {
                            PlcOmronTypeNX1P plc = new PlcOmronTypeNX1P((PlcOmronTypeNX1PData)item.Value);
                            dicHardwareDriver.Add(item.Value.Name, plc);
                            dicSettingForms.Add(item.Value.Name, new FormOmronTypeNX1P((PlcOmronTypeNX1PData)item.Value));
                        }
                    }
                    #endregion
                    #region Siemens
                    if (item.Value.Vender == HardwareVender.Siemens)
                    {
                        if (item.Value.Series == HardwareSeries.Siemens_PLC_S7200Smart)
                        {
                            PlcSiemensS7200 demoCard = new PlcSiemensS7200((PlcSiemensS7200Data)item.Value);
                            dicHardwareDriver.Add(item.Value.Name, demoCard);

                            dicSettingForms.Add(item.Value.Name, new FormSiemensS7200((PlcSiemensS7200Data)item.Value));
                        }
                    }
                    #endregion
                    #region Yamaha
                    if (item.Value.Vender == HardwareVender.Yamaha)
                    {
                        if (item.Value.Series == HardwareSeries.Yamaha_Robot_RCX340)
                        {
                            YamahaRobotRCX340 demoCard = new YamahaRobotRCX340((YamahaRobotData)item.Value);
                            dicHardwareDriver.Add(item.Value.Name, demoCard);

                            dicSettingForms.Add(item.Value.Name, new FormYamahaRobotRCX340((YamahaRobotData)item.Value));
                        }
                    }
                    #endregion
                    #region Cognex

                    #endregion
                    #region Keyence
                    if (item.Value.Vender == HardwareVender.Keyence)
                    {
                        if (item.Value.Series == HardwareSeries.Keyence_CodeReader_SR700)
                        {
                            KeyenceSR700 demoCard = new KeyenceSR700((KeyenceSR700Data)item.Value);
                            dicHardwareDriver.Add(item.Value.Name, demoCard);

                            dicSettingForms.Add(item.Value.Name, new FormKeyenceSR700((KeyenceSR700Data)item.Value));
                        }
                    }
                    #endregion
                    #region Basler
                    if (item.Value.Vender == HardwareVender.Basler)
                    {
                        if (item.Value.Series == HardwareSeries.Basler_Camera_Aca500M)
                        {
                            Aca500M driver = new Aca500M((Aca500MData)item.Value);
                            dicHardwareDriver.Add(item.Value.Name, driver);
                            dicSettingForms.Add(item.Value.Name, new FormAca500M((Aca500MData)item.Value));
                        }
                    }
                    #endregion
                    #region ImagingSource
                    if (item.Value.Vender == HardwareVender.ImagingSource)
                    {
                        if(item.Value.Series == HardwareSeries.ImagingSource_Camera_GigE)
                        {
                            ImagingSourceGigE driver = new ImagingSourceGigE((ImagingSourceGigEData)item.Value);
                            dicHardwareDriver.Add(item.Value.Name, driver);
                            dicSettingForms.Add(item.Value.Name, new FormImagingSourceGigE((ImagingSourceGigEData)item.Value));
                        }
                    }
                    #endregion
                }
                foreach (KeyValuePair<string, HardwareBase> item in dicHardwareDriver)
                {
                    item.Value.Init(docHardware.dicHardwareData[item.Key]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                throw;
            }

        }

        #region Add/Remove/Rename hardware
        public static string GetNewHardwareName(HardwareVender hardwareVender, HardwareType type, HardwareSeries series)
        {
            string strTemp = hardwareVender.ToString() + "_" + type.ToString() + "_" + series.ToString() + "_";
            int index = 1;

            if (docHardware == null)
                return strTemp + index.ToString();

            while (true)
            {
                if (!docHardware.dicHardwareData.ContainsKey(strTemp + index.ToString()))
                    return strTemp + index.ToString();
                index++;
                continue;
            }
        }
        public static string AddNewHardware(HardwareVender vender, HardwareType type, HardwareSeries series)
        {
            bool bUnknowHardware = true;
            string strName = string.Empty;
            try
            {
                #region Demo
                if (vender == HardwareVender.Demo)
                {
                    #region Motion Card
                    if(type == HardwareType.MotionCard)
                    {
                        if(series == HardwareSeries.Demo_MotionCard_Common)
                        {
                            DemoMotionCardData data = new DemoMotionCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new DemoMotionCard());
                            bUnknowHardware = false;
                        }
                    }
                    if(type == HardwareType.InputCard)
                    {
                        if (series == HardwareSeries.Demo_InputCard_Common)
                        {
                            DemoInputCardData data = new DemoInputCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new DemoInputCard());
                            bUnknowHardware = false;
                        }
                    }
                    if (type == HardwareType.OutputCard)
                    {
                        if (series == HardwareSeries.Demo_OutputCard_Common)
                        {
                            DemoOutputCardData data = new DemoOutputCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new DemoOutputCard());
                            bUnknowHardware = false;
                        }
                    }
                    if (type == HardwareType.InputOutputCard)
                    {
                        if (series == HardwareSeries.Demo_InputOutputCard_Common)
                        {
                            DemoInputOutputCardData data = new DemoInputOutputCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new DemoInputOutputCard());
                            bUnknowHardware = false;
                        }
                    }

                    #endregion
                    #region GEMotion Card
                    if (type == HardwareType.GEMotionCard)
                    {

                    }
                    #endregion
                    #region Input Card
                    if (type == HardwareType.InputCard)
                    {
                        DemoInputCardData data = new DemoInputCardData
                        {
                            Name = GetNewHardwareName(vender, type, series),
                            Vender = vender,
                            Type = type
                        };
                        strName = data.Name;
                        data.Text = data.Name;
                        docHardware.dicHardwareData.Add(data.Name, data);
                        docHardware.listHardwareData.Add(data);
                        dicHardwareDriver.Add(data.Name, new DemoInputCard());
                        bUnknowHardware = false;
                    }
                    #endregion
                    #region Output Card
                    if (type == HardwareType.OutputCard)
                    {
                        DemoOutputCardData data = new DemoOutputCardData
                        {
                            Name = GetNewHardwareName(vender, type, series),
                            Vender = vender,
                            Type = type
                        };
                        strName = data.Name;
                        data.Text = data.Name;
                        docHardware.dicHardwareData.Add(data.Name, data);
                        docHardware.listHardwareData.Add(data);
                        dicHardwareDriver.Add(data.Name, new DemoOutputCard());
                        bUnknowHardware = false;
                    }
                    #endregion
                    #region InputOutput Card
                    if (type == HardwareType.InputOutputCard)
                    {
                        DemoInputOutputCardData data = new DemoInputOutputCardData
                        {
                            Name = GetNewHardwareName(vender, type, series),
                            Vender = vender,
                            Type = type
                        };
                        strName = data.Name;
                        data.Text = data.Name;
                        docHardware.dicHardwareData.Add(data.Name, data);
                        docHardware.listHardwareData.Add(data);
                        dicHardwareDriver.Add(data.Name, new DemoInputOutputCard());
                        bUnknowHardware = false;
                    }
                    #endregion
                    #region AIO Card
                    if (type == HardwareType.AIOCard)
                    {

                    }
                    #endregion
                }
                #endregion
                #region GoogolTech
                if (vender == HardwareVender.GoogolTech)
                {
                    #region Motion Card
                    if (type == HardwareType.MotionCard)
                    {
                        if(series == HardwareSeries.GoogolTech_MotionCard_Common)
                        {
                            GoogolMotionCardData data = new GoogolMotionCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);
                            dicHardwareDriver.Add(data.Name, new GoogolMotionCard());
                            dicSettingForms.Add(data.Name, new FormGoogolSetting());
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                    #region GEMotion Card
                    if (type == HardwareType.GEMotionCard)
                    {

                    }
                    #endregion
                    #region Input Card
                    if (type == HardwareType.InputCard)
                    {
                        if(series == HardwareSeries.GoogolTech_InputCard_Common)
                        {
                            GoogolInputCardData data = new GoogolInputCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);
                            dicHardwareDriver.Add(data.Name, new GoogolInputCard());
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                    #region Output Card
                    if (type == HardwareType.OutputCard)
                    {
                        if(series == HardwareSeries.GoogolTech_OutputCard_Common)
                        {
                            GoogolOutputCardData data = new GoogolOutputCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);
                            dicHardwareDriver.Add(data.Name, new GoogolOutputCard());
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                    #region InputOutput Card
                    if (type == HardwareType.InputOutputCard)
                    {
                        if(series == HardwareSeries.GoogolTech_InputOutputCard_Common)
                        {
                            GoogolInputOutputCardData data = new GoogolInputOutputCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);
                            dicHardwareDriver.Add(data.Name, new GoogolInputOutputCard());
                            bUnknowHardware = false;
                        }

                    }
                    #endregion
                    #region AIO Card
                    if (type == HardwareType.AIOCard)
                    {

                    }
                    #endregion
                }
                #endregion
                #region LeadShine
                if (vender == HardwareVender.LeadShine)
                {
                    #region Motion Card
                    if (type == HardwareType.MotionCard)
                    {
                        if(series== HardwareSeries.LeadShine_MotionCard_Common)
                        {
                            LeadShineMotionCardData data = new LeadShineMotionCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);
                            dicHardwareDriver.Add(data.Name, new LeadShineMotionCard());
                            dicSettingForms.Add(data.Name, new FormLeadShineMotionCard(data));
                            bUnknowHardware = false;
                        }
                        if(series == HardwareSeries.LeadShine_MotionCard_Dmc1000)
                        {
                            LeadShineMotionCardData data = new LeadShineMotionCardData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);
                            dicHardwareDriver.Add(data.Name, new LeadShineMotionCardDmc1000());
                            dicSettingForms.Add(data.Name, new FormLeadShineMotionCard(data));
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                    #region GEMotion Card
                    if (type == HardwareType.GEMotionCard)
                    {

                    }
                    #endregion
                    #region Input Card
                    if (type == HardwareType.InputCard)
                    {
                        LeadShineInputCardData data = new LeadShineInputCardData
                        {
                            Name = GetNewHardwareName(vender, type, series),
                            Vender = vender,
                            Type = type
                        };
                        strName = data.Name;
                        data.Text = data.Name;
                        docHardware.dicHardwareData.Add(data.Name, data);
                        docHardware.listHardwareData.Add(data);
                        dicHardwareDriver.Add(data.Name, new LeadShineInputCard());
                        bUnknowHardware = false;
                    }
                    #endregion
                    #region Output Card
                    if (type == HardwareType.OutputCard)
                    {
                        LeadShineOutputCardData data = new LeadShineOutputCardData
                        {
                            Name = GetNewHardwareName(vender, type, series),
                            Vender = vender,
                            Type = type
                        };
                        strName = data.Name;
                        data.Text = data.Name;

                        docHardware.dicHardwareData.Add(data.Name, data);
                        docHardware.listHardwareData.Add(data);
                        dicHardwareDriver.Add(data.Name, new LeadShineOutputCard());
                        bUnknowHardware = false;
                    }
                    #endregion
                    #region InputOutput Card
                    if (type == HardwareType.InputOutputCard)
                    {
                        LeadShineInputOutputCardData data = new LeadShineInputOutputCardData
                        {
                            Name = GetNewHardwareName(vender, type, series),
                            Vender = vender,
                            Type = type
                        };
                        strName = data.Name;
                        data.Text = data.Name;
                        docHardware.dicHardwareData.Add(data.Name, data);
                        docHardware.listHardwareData.Add(data);
                        dicHardwareDriver.Add(data.Name, new LeadShineInputOutputCard());
                        bUnknowHardware = false;
                    }
                    #endregion
                    #region AIO Card
                    if (type == HardwareType.AIOCard)
                    {

                    }
                    #endregion
                }
                #endregion
                #region AdvanTech
                if (vender == HardwareVender.AdvanTech)
                {
                    #region Motion Card
                    if (type == HardwareType.MotionCard)
                    {
                    }
                    #endregion
                    #region GEMotion Card
                    if (type == HardwareType.GEMotionCard)
                    {

                    }
                    #endregion
                    #region Input Card
                    if (type == HardwareType.InputCard)
                    {
                    }
                    #endregion
                    #region Output Card
                    if (type == HardwareType.OutputCard)
                    {
                    }
                    #endregion
                    #region InputOutput Card
                    if (type == HardwareType.InputOutputCard)
                    {
                    }
                    #endregion
                    #region AIO Card
                    if (type == HardwareType.AIOCard)
                    {

                    }
                    #endregion
                }
                #endregion
                #region Omron
                if (vender == HardwareVender.Omron)
                {
                    #region PLC
                    if (type == HardwareType.PLC)
                    {
                        if(series == HardwareSeries.Omron_PLC_NJ)
                        {
                            PlcOmronTypeNJData data = new PlcOmronTypeNJData
                            {
                                Name = GetNewHardwareName(HardwareVender.Omron, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new PlcOmronTypeNJ(data));
                            dicSettingForms.Add(data.Name, new FormOmronTypeNJ(data));
                            bUnknowHardware = false;
                        }
                        if(series == HardwareSeries.Omron_PLC_NX1P)
                        {
                            PlcOmronTypeNX1PData data = new PlcOmronTypeNX1PData
                            {
                                Name = GetNewHardwareName(HardwareVender.Omron, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new PlcOmronTypeNX1P(data));
                            dicSettingForms.Add(data.Name, new FormOmronTypeNX1P(data));
                            bUnknowHardware = false;
                        }

                    }
                    #endregion
                }
                #endregion
                #region Panasonic
                if (vender == HardwareVender.Panasonic)
                {
                    #region PLC
                    if (type == HardwareType.PLC)
                    {
                        if(series == HardwareSeries.Panasonic_PLC_Common)
                        {
                            PlcPanasonicData data = new PlcPanasonicData
                            {
                                Name = GetNewHardwareName(vender, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new PlcPanasonic(data));
                            dicSettingForms.Add(data.Name, new FormPanasonic(data));
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                }
                #endregion
                #region Siemens
                if (vender == HardwareVender.Siemens)
                {
                    #region PLC
                    if (type == HardwareType.PLC)
                    {
                        if(series == HardwareSeries.Siemens_PLC_S7200Smart)
                        {
                            PlcSiemensS7200Data data = new PlcSiemensS7200Data
                            {
                                Name = GetNewHardwareName(HardwareVender.Siemens, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new PlcSiemensS7200(data));
                            dicSettingForms.Add(data.Name, new FormSiemensS7200(data));

                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                }
                #endregion
                #region Yamaha
                if (vender == HardwareVender.Yamaha)
                {
                    #region Robot
                    if (type == HardwareType.Robot)
                    {
                        if(series == HardwareSeries.Yamaha_Robot_RCX340)
                        {
                            YamahaRobotData data = new YamahaRobotData
                            {
                                Name = GetNewHardwareName(HardwareVender.Yamaha, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new YamahaRobotRCX340(data));
                            dicSettingForms.Add(data.Name, new FormYamahaRobotRCX340(data));
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                }
                #endregion
                #region Basler
                if (vender == HardwareVender.Basler)
                {
                    #region Camera
                    if (type == HardwareType.Camera)
                    {
                        if (series == HardwareSeries.Basler_Camera_Aca500M)
                        {
                            Aca500MData data = new Aca500MData
                            {
                                Name = GetNewHardwareName(HardwareVender.Basler, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new Aca500M(data));
                            dicSettingForms.Add(data.Name, new FormAca500M(data));
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                }
                #endregion
                #region ImagingSource
                if (vender == HardwareVender.ImagingSource)
                {
                    #region Camera
                    if (type == HardwareType.Camera)
                    {
                        if (series == HardwareSeries.ImagingSource_Camera_GigE)
                        {
                            ImagingSourceGigEData data = new ImagingSourceGigEData
                            {
                                Name = GetNewHardwareName(HardwareVender.ImagingSource, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new ImagingSourceGigE(data));
                            dicSettingForms.Add(data.Name, new FormImagingSourceGigE(data));
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                }
                #endregion
                #region Cognex

                #endregion
                #region Keyence
                if(vender == HardwareVender.Keyence)
                {
                    #region CodeReader
                    if (type == HardwareType.CodeReader)
                    {
                        if(series == HardwareSeries.Keyence_CodeReader_SR700)
                        {
                             KeyenceSR700Data data = new KeyenceSR700Data
                             {
                                Name = GetNewHardwareName(HardwareVender.Keyence, type, series),
                                Vender = vender,
                                Series = series,
                                Type = type
                            };
                            strName = data.Name;
                            data.Text = data.Name;
                            docHardware.dicHardwareData.Add(data.Name, data);
                            docHardware.listHardwareData.Add(data);

                            dicHardwareDriver.Add(data.Name, new KeyenceSR700(data));
                            dicSettingForms.Add(data.Name, new FormKeyenceSR700(data));
                            bUnknowHardware = false;
                        }
                    }
                    #endregion
                }
                #endregion

                if (bUnknowHardware)
                {
                    MessageBox.Show("Unknow hardware!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return null;
                }
                return strName;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool RemoveHardware(string strName)
        {
            try
            {
                docHardware.dicHardwareData.Remove(strName);
                docHardware.listHardwareData.Clear();
                foreach (KeyValuePair<string, HardwareData> item in docHardware.dicHardwareData)
                {
                    docHardware.listHardwareData.Add(item.Value);
                }
                if (dicSettingForms.ContainsKey(strName))
                {
                    dicSettingForms[strName].Close();
                    dicSettingForms.Remove(strName);
                }
                if(dicHardwareDriver.ContainsKey(strName))
                {
                    dicHardwareDriver.Remove(strName);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool RenameHardware(string strOldName, string strNewName)
        {
            if (string.IsNullOrEmpty(strOldName) || string.IsNullOrEmpty(strNewName) || docHardware == null)
                return false;
            try
            {
                if (docHardware.dicHardwareData.ContainsKey(strNewName) || !docHardware.dicHardwareData.ContainsKey(strOldName))
                {
                    return false;
                }
                foreach (HardwareData item in HardwareManage.docHardware.listHardwareData)
                {
                    if (item.Name.Equals(strOldName))
                    {
                        item.Name = strNewName;
                        item.Text = strNewName;
                        docHardware.dicHardwareData.Remove(strOldName);
                        docHardware.dicHardwareData.Add(strNewName, item);

                        if (dicSettingForms.ContainsKey(strOldName))
                        {
                            Form form = dicSettingForms[strOldName];
                            dicSettingForms.Remove(strOldName);
                            dicSettingForms.Add(strNewName, form);
                        }

                        if(dicHardwareDriver.ContainsKey(strOldName))
                        {
                            HardwareBase hardware = dicHardwareDriver[strOldName];
                            dicHardwareDriver.Remove(strOldName);
                            dicHardwareDriver.Add(strNewName, hardware);
                        }
                        break;
                    }
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
