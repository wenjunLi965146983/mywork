using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using WorldGeneralLib.Vision.Forms;
using WorldGeneralLib.Forms;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Table;
using WorldGeneralLib.Data;
using WorldGeneralLib.IO;
using WorldGeneralLib.Vision;
using WorldGeneralLib.Functions;
using WorldGeneralLib.Alarm;

namespace WorldGeneralLib
{
    public enum MacSta
    {
        Stop = 0,
        Running,
        Alarm
    }
    public enum MacHomeSta
    {
        Reseted = 0, 
        WaittingReset, 
        Reseting
    }
    public class MainModule
    {
        public static FormMain formMain;
        public static AlarmManage alarmManage;

        [STAThread]
        public static void StartModule(Form formStart, Form formManual, bool bLoginSwitch, AlarmFormStyle alarmFormStyle)
        {
            alarmManage = new AlarmManage(alarmFormStyle);
            formMain = new FormMain();
            formMain.bLoginSwitch = bLoginSwitch;

            #region Login
            if(bLoginSwitch)
            {
                Login.FormLogin formLogin = new Login.FormLogin();
                formLogin.ShowDialog();
                if(Login.LoginManage.iCurrUserLevel < 0)
                {
                    Application.Exit();
                    return;
                }
            }
            else
            {
                Login.LoginManage.iCurrUserLevel = 3;
                Login.LoginManage.strCurrUserName = "Root";
            }
            #endregion

            //Main Forms
            formMain.formOutput = formMain.CreateNewOutputWindow("System output" , Log.SysLog,false);
            AddRunMessage(">--------------- Program start -----------------", OutputLevel.Trace);

            formMain.formOperator = new FormOperator();
            formMain.formStart = formStart;
            formMain.formManualEx = formManual;
            formMain.formManual = new FormManual();
            formMain.formUserParam = new FormUserParam();
            formMain.formSysParam = new FormSysParam();
            formMain.formIoMonitor = new FormIoMonitor();
            formMain.formVision = new FormVision();
            formMain.formTesting = new FormTesting();

            Application.Run(formMain);
        }

        public static void LoadDoc()
        {
            AddRunMessage(">Load document...", OutputLevel.Trace);
            HardwareManage.LoadDoc();
            TableManage.LoadDoc();
            DataManage.LoadData();
            IOManage.LoadObj();
            VisionManage.LoadDoc();
            alarmManage.LoadDoc();
        }
        public static void InitHardware()
        {
            AddRunMessage(">Hardware initialize...", OutputLevel.Trace);
            HardwareManage.InitHardware();
            TableManage.InitTables();
            IOManage.InitDrivers();
            VisionManage.VisionInit();
            AddRunMessage(">System init ok.", OutputLevel.Trace);
        }
        public static void AddRunMessage(string strMsg, OutputLevel level)
        {
            try
            {
                if(null != formMain.formOutput)
                {
                    formMain.formOutput.AddRunMessage(strMsg , level);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
