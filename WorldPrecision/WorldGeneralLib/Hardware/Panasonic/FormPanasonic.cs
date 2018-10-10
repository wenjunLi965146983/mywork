using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using WorldGeneralLib.Hardware.Panasonic;

namespace WorldGeneralLib.Hardware.Panasonic
{
    public partial class FormPanasonic : Form
    {
        private PlcPanasonicData _plcData;
        private PlcPanasonic _plcDriver;

        //private Thread threadScan;
        public FormPanasonic()
        {
            InitializeComponent();
        }
        public FormPanasonic(PlcPanasonicData plcData):this()
        {
            _plcData = plcData;
            _plcDriver = (PlcPanasonic)HardwareManage.dicHardwareDriver[_plcData.Name];
        }

        private void FormPanasonic_Load(object sender, EventArgs e)
        {
            label1.Text = _plcData.Name;
        }
    }
}
