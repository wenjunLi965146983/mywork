using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib;
using WorldPrecision.Forms;
using WorldGeneralLib.Alarm;

namespace WorldPrecision
{
    static class Program
    {
        public static FormStart formStart;
        public static FormManual formManual;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formStart = new FormStart();
            formManual = new FormManual();
            MainModule.StartModule(formStart, formManual, false, AlarmFormStyle.Normal);
        }
    }
}
