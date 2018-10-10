using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.Hardware.Camera.ImagingSource.GigE
{
    public partial class FormImagingSourceGigE : Form
    {
        private ImagingSourceGigEData _camData;
        public FormImagingSourceGigE()
        {
            InitializeComponent();
        }
        public FormImagingSourceGigE(ImagingSourceGigEData camData):this()
        {
            this._camData = camData;
        }

        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
