using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Table;

namespace WorldGeneralLib.Forms
{
    public partial class FormManual : Form
    {
        private FormTableDriver formTableDriver;
        public FormManual()
        {
            InitializeComponent();
        }

        #region Events
        private void FormManual_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        public void EventTableDataReLoadHandler()
        {
            try
            {
                if(null != formTableDriver)
                    formTableDriver.EventTableDataReLoadHandler();
            }
            catch (Exception)
            {
            }
            
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            formTableDriver = new FormTableDriver();
            formTableDriver.TopLevel = false;
            panelMain.Controls.Add(formTableDriver);
            formTableDriver.Dock = DockStyle.Fill;
            formTableDriver.Show();

            formTableDriver.panelExternView.Controls.Clear();
            MainModule.formMain.formManualEx.TopLevel = false;
            MainModule.formMain.formManualEx.Dock = DockStyle.Fill;
            MainModule.formMain.formManualEx.Size = formTableDriver.panelExternView.Size;
            formTableDriver.panelExternView.Controls.Add(MainModule.formMain.formManualEx);
            MainModule.formMain.formManualEx.Show();
            timer1.Stop();
        }
    }
}