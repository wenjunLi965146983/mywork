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
using WorldGeneralLib.TaskBase;
using WorldGeneralLib.Company.Catl.MES.G08;

namespace WorldGeneralLib.Forms
{
    public partial class FormTesting : Form
    {
        public FormTesting()
        {
            InitializeComponent();
        }

        private void FormTesting_SizeChanged(object sender, EventArgs e)
        {
            panelMES.Location = new Point((this.Width - panelMES.Width) / 2, panelMES.Location.Y);
        }

        #region MES
        private void btnCheckSfcSta_Click(object sender, EventArgs e)
        {
            tbMesRes.Clear();
            if (string.IsNullOrEmpty(tbBarcode1.Text))
                return;
            MESRes mesRes;
            mesRes = MainModule.formMain.mes.CheckSfcStatus(tbBarcode1.Text.Trim());
            if(mesRes.bSuccess)
            {
                if(mesRes.miCheckResponse.@return.code == 0) //进站校验通过
                {
                    tbMesRes.Text = "调用成功！\r\n电芯进站校验通过！\r\n" + "code : " + mesRes.miCheckResponse.@return.code.ToString() + "\r\n" +
                                               mesRes.miCheckResponse.@return.message;
                }
                else
                {
                    tbMesRes.Text = "调用成功！\r\n电芯进站校验未通过！\r\n" + "code : " + mesRes.miCheckResponse.@return.code.ToString() + "\r\n" +
                                               mesRes.miCheckResponse.@return.message;
                }  
            }
            else
            {
                tbMesRes.Text = "调用失败！";
            }
        }

        private void btnDcForSfcEx_Click(object sender, EventArgs e)
        {
            MESRes mesRes;
            SprayingInfo info = new SprayingInfo();
            info.strSprayingPressure = tbSprayingPressure.Text;
            info.strSprayingTime = tbSprayingTime.Text;
            info.strDryingPressure = tbDryingPressure.Text;
            info.strDryingTime = tbDryingTime.Text;
            info.strBlowingResidualFluidTime = tbBlowingTime.Text;
            info.strOilTemp = tbOilTemperature.Text;

            tbMesRes.Clear();
            mesRes = MainModule.formMain.mes.DataCollectForSfcEx(tbBarcode2.Text.Trim(), info);
            if (mesRes.bSuccess)
            {
                if (mesRes.dataCollectResponse.@return.code == 0) //进站校验通过
                {
                    tbMesRes.Text = "调用成功！\r\n电芯数据上载成功！\r\n" + "code : " + mesRes.dataCollectResponse.@return.code.ToString() + "\r\n" +
                           mesRes.dataCollectResponse.@return.message;
                }
                else
                {
                    tbMesRes.Text = "调用成功！\r\n电芯数据上载失败！\r\n" + "code : " + mesRes.dataCollectResponse.@return.code.ToString() + "\r\n" +
                                                mesRes.dataCollectResponse.@return.message;
                }
            }
            else
            {
                tbMesRes.Text = "调用失败！";
            }
        }

        #endregion
    }
}
