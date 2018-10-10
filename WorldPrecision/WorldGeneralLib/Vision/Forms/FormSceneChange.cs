using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.Vision.Forms
{
    public partial class FormSceneChange : Form
    {
        public FormSceneChange()
        {
            InitializeComponent();
        }

        private void FormSceneChange_Load(object sender, EventArgs e)
        {
            try
            {
                labCurrSceneIndex.Text = VisionManage.iCurrSceneIndex.ToString();
                cmbScene.Items.Clear();
                for(int index=0;index<VisionManage.MaxSceneCount;index++)
                {
                    cmbScene.Items.Add("Scene " + index.ToString());
                }
                cmbScene.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if(cmbScene.SelectedIndex > -1 && cmbScene.SelectedIndex<VisionManage.MaxSceneCount)
                {
                    VisionManage.iCurrSceneIndex = cmbScene.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
            this.Close();
        }
    }
}
