using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Forms;

namespace WorldGeneralLib
{
    static class Program
    {
        /// <summary>
        /// Ӧ�ó��������ڵ㡣
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainModule.StartModule(new FormStart(), new FormManual(), false, Alarm.AlarmFormStyle.CatlStyle);
        }
    }
}