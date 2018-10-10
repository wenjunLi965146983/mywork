using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.IO;

namespace WorldGeneralLib.Forms
{
    public partial class FormIoMonitor : Form
    {
        private Dictionary<string, UtrlIOStatus> dicInputSta;
        private Dictionary<string, UtrlIOStatus> dicOutputSta;
        public FormIoMonitor()
        {
            InitializeComponent();
        }

        public void RefreshView()
        {
            try
            {
                panelInput.Controls.Clear();
                panelOutput.Controls.Clear();
                panelInput.Height = this.Height / 2;
                Point point = new Point(70, 40);

                #region Input view
                this.label1.Location = new System.Drawing.Point(12, 18);
                this.label2.Location = new System.Drawing.Point(60, 25);
                this.label2.Width = panelInput.Width - 70 - 30;
                this.panelInput.Controls.Add(this.label2);
                this.panelInput.Controls.Add(this.label1);
                foreach (KeyValuePair<string, UtrlIOStatus> item in dicInputSta)
                {
                    if ((point.X + item.Value.Width) > (panelInput.Width - 30) || (point.X + item.Value.Width) > 1200)
                    {
                        point.X = 70;
                        point.Y = point.Y + item.Value.Height + 3;
                    }
                    panelInput.Controls.Add(item.Value);
                    item.Value.Location = new Point(point.X, point.Y);
                    point.X = item.Value.Location.X + item.Value.Width + 28;
                }
                if (dicInputSta.Count <= 0)
                {
                    panelInput.Height = 70;
                }
                else
                {
                    panelInput.Height = point.Y + 50;
                }
                #endregion
                #region Output view 
                point = new Point(70, 40);
                this.label3.Location = new System.Drawing.Point(60, 25);
                this.label3.Width = panelOutput.Width - 70 - 30;
                this.label4.Location = new System.Drawing.Point(12, 18);
                this.panelOutput.Controls.Add(this.label3);
                this.panelOutput.Controls.Add(this.label4);

                foreach (KeyValuePair<string, UtrlIOStatus> item in dicOutputSta)
                {
                    if ((point.X + item.Value.Width) > (panelInput.Width - 30) || (point.X + item.Value.Width) > 1200)
                    {
                        point.X = 70;
                        point.Y = point.Y + item.Value.Height + 3;
                    }
                    panelOutput.Controls.Add(item.Value);
                    item.Value.Location = new Point(point.X, point.Y);
                    point.X = item.Value.Location.X + item.Value.Width + 28;

                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void RefreshDictionary()
        {
            try
            {
                if (null == IOManage.docIO)
                {
                    return;
                }

                dicInputSta.Clear();
                dicOutputSta.Clear();

                foreach (IOData item in IOManage.docIO.listInput)
                {
                    UtrlIOStatus utrlIOSta = new UtrlIOStatus(item.Name, item.Text, true, false);
                    utrlIOSta.UpdateSta(false);
                    dicInputSta.Add(item.Name, utrlIOSta);
                }

                foreach (IOData item in IOManage.docIO.listOutput)
                {
                    UtrlIOStatus utrlIOSta = new UtrlIOStatus(item.Name, item.Text, false, true);
                    utrlIOSta.UpdateSta(true);
                    dicOutputSta.Add(item.Name, utrlIOSta);
                }
            }
            catch //(Exception)
            {
            }
        }

        private void FormIoMonitor_Load(object sender, EventArgs e)
        {
            //panelInput.Height = this.Height / 2;
            dicInputSta = new Dictionary<string, UtrlIOStatus>();
            dicOutputSta = new Dictionary<string, UtrlIOStatus>();

            RefreshDictionary();
            RefreshView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDictionary();
            RefreshView();
        }
    }
}
