using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Hardware.CodeReader.Keyence.SR700;
using WorldPrecision.NameItems;
using Spire.Xls;

namespace WorldPrecision.Forms
{
    public partial class FormProductInfo : Form
    {
        public KeyenceSR700 driver1;
        public KeyenceSR700 driver2;

        public int iScanNgCount = 0;
        public int iScanOkCount = 0;

        public int iMesNgCount = 0;
        public int iMesOkCount = 0;
        public FormProductInfo()
        {
            InitializeComponent();
            driver1 = null;
            driver2 = null;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (null == driver1)
                    driver1 = (KeyenceSR700)HardwareManage.dicHardwareDriver[HardwareName.内线读码器];
                if (null == driver2)
                    driver2 = (KeyenceSR700)HardwareManage.dicHardwareDriver[HardwareName.外线读码器];
                if(null == driver1 || null == driver2)
                    throw new Exception();
                btnCodeReaderTrigger1.Enabled = driver1.IsConnected();
                btnCodeReaderTrigger2.Enabled = driver2.IsConnected();
            }
            catch (Exception)
            {
                btnCodeReaderTrigger1.Enabled = false;
                btnCodeReaderTrigger2.Enabled = false;
            }
        }

        private void ReadTest(int index)
        {
            try
            {
                string strData = "";
                CodeReaderRes res;
                if(1 == index)
                    res = driver1.Read(out strData);
                else
                    res = driver2.Read(out strData);
                if (res == CodeReaderRes.SUCCESS)
                {
                    if(strData.Contains("ERROR"))
                    {
                        tbReadRes.Text = "读码完成,解码失败！";
                        tbReadRes.ForeColor = Color.Red;
                        return;
                    }
                    tbReadRes.Text = "读码完成 ：" + strData;
                    tbReadRes.ForeColor = Color.Green;
                    return;
                }
                if(res == CodeReaderRes.TIMEOUT)
                {
                    tbReadRes.Text = "读码完成,读码超时！";
                    tbReadRes.ForeColor = Color.Red;
                    return;
                }
                tbReadRes.Text = "读码失败,读码异常！";
                tbReadRes.ForeColor = Color.Red;
            }
            catch (Exception)
            {
                tbReadRes.Text = "读码失败,读码超时！";
                tbReadRes.BackColor = Color.Red;
            }
        }
        private void btnCodeReaderTrigger1_Click(object sender, EventArgs e)
        {
            tbReadRes.Text = "";
            tbReadRes.ForeColor = Color.White;
            btnCodeReaderTrigger1.Enabled = false;
            ReadTest(1);
            btnCodeReaderTrigger1.Enabled = true;
        }

        private void btnCodeReaderTrigger2_Click(object sender, EventArgs e)
        {
            tbReadRes.Text = "";
            tbReadRes.ForeColor = Color.White;
            btnCodeReaderTrigger2.Enabled = false;
            ReadTest(2);
            btnCodeReaderTrigger2.Enabled = true;
        }

        private void btnSetUserInfo_Click(object sender, EventArgs e)
        {
            Program.formStart.SetUserInfoToPLC();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.formStart.bMesSwitch = checkBox1.Checked;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            iScanNgCount = 0;
            iScanOkCount = 0;
            iMesNgCount = 0;
            iMesOkCount = 0;

            tbScanRate.Text = "0 %";
            tbMesRate.Text = "0 %";
        }

        public void ResultRefresh()
        {
            if((iScanOkCount + iScanNgCount) < 1)
            {
                tbScanRate.Text = "0 %";
            }
            else
            {
                float fTemp = (float)((float)iScanOkCount / (float)(iScanOkCount + iScanNgCount));
                tbScanRate.Text = fTemp.ToString("0.0") + " %";
            }

            if ((iMesNgCount + iMesOkCount) < 1)
            {
                tbMesRate.Text = "0 %";
            }
            else
            {
                float fTemp = (float)((float)iMesOkCount / (float)(iMesOkCount + iMesNgCount));
                tbMesRate.Text = fTemp.ToString("0.0") + " %";
            }
        }

        private void FormProductInfo_Load(object sender, EventArgs e)
        {
            WorldGeneralLib.Login.LoginManage.eventUserChanged += new WorldGeneralLib.Login.LoginManage.EventUserChanged(this.EventUserChangedHandler);
        }

        private void EventUserChangedHandler()
        {
            try
            {
                Action action = () =>
                 {
                     if (WorldGeneralLib.Login.LoginManage.iCurrUserLevel < 1)
                         checkBox1.Enabled = false;
                     else
                         checkBox1.Enabled = true;
                 };
                this.Invoke(action);
            }
            catch (Exception)
            {
            }
        }

        private void btnExcelOperatorTest_Click(object sender, EventArgs e)
        {
            MESLocalData data = new MESLocalData();

            #region Set MES local data1
            data.strBarcode = "123456789012";
            data.strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace('/', '-');
            data.strResourceID = "ASECXX1005";
            if (!Program.formStart.bMesSwitch)
            {
                data.strMESResult = "Disable";
            }
            else
            {
                data.strMESResult = true ? "OK" : "NG";
            }
            data.strCleanResult =  "OK";
            data.strSprPre = "100.5";
            data.strSprPreMaxSettingVal = "150";
            data.strSprPreMinSettingVal = "50";
            data.strSprTime = "2";
            data.strSprTimeSettingVal = data.strSprTime;
            data.strDryPre = "200";
            data.strDryPreMaxSettingVal = "250";
            data.strDryPreMinSettingVal = "150";
            data.strDryTime = "3";
            data.strDryTimeSettingVal = "3";
            data.strBlowingTime = "60";
            data.strBlowingTimeSettingVal = data.strBlowingTime;
            data.strOilTemp = "130";
            data.strOilTempSettingVal = data.strOilTemp;
            #endregion

            WriteMesFile.WriteToCsvFile(data);
        }
    }
}
