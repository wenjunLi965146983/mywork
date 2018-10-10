using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib;
using WorldGeneralLib.Forms;
using WorldGeneralLib.TaskBase;
using WorldGeneralLib.Table;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Hardware.Yamaha;
using WorldGeneralLib.Hardware.Yamaha.RCX340;
using WorldPrecision.NameItems;
using WorldGeneralLib.Data;

namespace WorldPrecision.Forms
{
    public partial class FormManual : Form
    {
        public FormManual()
        {
            InitializeComponent();
        }

        private void FormManual_Load(object sender, EventArgs e)
        {
        }

        private void tbCtrlIOView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
