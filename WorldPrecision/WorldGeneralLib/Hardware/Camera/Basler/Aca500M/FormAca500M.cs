using BP=Basler.Pylon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.TaskBase;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using System.Diagnostics;
using PylonC.NETSupportLibrary;
using Basler.Pylon;

namespace WorldGeneralLib.Hardware.Camera.Basler.Aca500M
{
    public partial class FormAca500M : Form
    {
        public Aca500MData cameraData;
        public Aca500M driver;
        private List<BP.ICameraInfo> cameraInfo = new List<BP.ICameraInfo>();//相机list
        private bool keepRun;
    


        public FormAca500M(Aca500MData data)
        {
            InitializeComponent();
            cameraData = data;
            panelMain.Text = data.Name;
            driver = null;
            labelSerialNumber.Text = cameraData.SerialNumber;
        }

        #region Events
        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(!SetValues())
                {
                    throw new Exception();
                }
                if (HardwareManage.docHardware.SaveDoc())
                {
                    MessageBox.Show("Save successful.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Save failed.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(btnOpen.Text.Equals("Open"))
            {
                try
                {
                    if(null == driver)
                    {
                        throw new Exception();
                    }
                    driver.cameraData = this.cameraData;
                    driver.Init(this.cameraData);
               
                    btnOpen.Text = "Close";
                  

                }
                catch (Exception)
                {
                    MessageBox.Show("Failure to connected to code reader " + cameraData.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
            else
            {
                driver.Close();
                btnOpen.Text = "Open";
                driver.Close();
            }
        }//打开相机
        private void btnSingleShot_Click(object sender, EventArgs e)
        {

            #region
            //if (btnOpen.Text.Equals("Close"))
            //{
            //    if (true == keepRun)
            //    {
            //         MessageBox.Show("Camera is running");
            //         return;
            //     }
            //    BP.IGrabResult grabResult;
            //    Bitmap bitmap=null;
            //    byte[] b;


            //    bool succesed=driver.GrabImage(out grabResult);
            //    if (succesed)
            //    {
            //        b = grabResult.PixelData as byte[];
            //    }
            //    else
            //    {
            //        MessageBox.Show("抓取图失败");
            //        return;

            //    }


            //    BitmapFactory.CreateBitmap(out bitmap, grabResult.Width, grabResult.Height, false);
            //    BitmapFactory.UpdateBitmap(bitmap, b, grabResult.Width, grabResult.Height, false);
            //    pictureBox1.Image = bitmap;

            //}
            #endregion

            if (btnOpen.Text.Equals("Close"))
            {
                if (true == keepRun)
                {
                    MessageBox.Show("Camera is running");
                    return;
                }
            
                bool succesed = driver.GrabImage();
                if (succesed)
                {
                    driver.aca500M_Camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;

                }
                else
                {
                    MessageBox.Show("触发失败");
                    return;

                }


              

            }
            else
            {
                MessageBox.Show("please Open the Camera");
            }


        }//单次拍照
       private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            if (!this.driver.aca500M_Camera.IsOpen)
            {
                MessageBox.Show("Please Open the Camera");
                return;
            }
                IGrabResult grabResult = e.GrabResult;
            byte[] b;
            Bitmap bitmap = null;
            // Image grabbed successfully?
            if (grabResult.GrabSucceeded)
            {

                b = grabResult.PixelData as byte[];
            }
            else
            {
                MessageBox.Show("抓取图失败");
                return;

            }

           
            BitmapFactory.CreateBitmap(out bitmap, grabResult.Width, grabResult.Height, false);
            BitmapFactory.UpdateBitmap(bitmap, b, grabResult.Width, grabResult.Height, false);
           
           
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);
            imageBox1.Image = image;
           
          
        }
        private void MultiShot_Click(object sender, EventArgs e)
        {
            keepRun= true;
            var task = new Task(() =>
            {
               
             
                while (keepRun)
                {

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    BP.IGrabResult grabResult;
                    Bitmap bitmap = null;
                    byte[] b;


                    bool succesed = driver.GrabImage(out grabResult);
                    if (succesed)
                    {
                     
                        b = grabResult.PixelData as byte[];
                    }
                    else
                    {
                        MessageBox.Show("抓取图失败");
                        return;

                    }
                 

                    BitmapFactory.CreateBitmap(out bitmap, grabResult.Width, grabResult.Height, false);
                    BitmapFactory.UpdateBitmap(bitmap, b, grabResult.Width, grabResult.Height, false);
                    Image<Gray, byte> image = new Image<Gray, byte>(bitmap);
                    imageBox1.Image = image;
                    sw.Stop();
                    Thread.Sleep(10);
                }
              



            });

            if ("Multi"== MultiShot.Text)
            {
                task.Start();
                MultiShot.Text = "Stop";
            }
            else
            {
                keepRun = false;
                MultiShot.Text = "Multi";
                Thread.Sleep(1000);
                
            }

        }

        private void FormAca500M_Load(object sender, EventArgs e)
        {
            Init();
            ShowValues();
            timer1.Start();
        }
        private void btnSearchCamera_Click(object sender, EventArgs e)//枚举相机
        {
            cameraInfo = BP.CameraFinder.Enumerate();
           
            if (cameraInfo == null)
            {
                MessageBox.Show("Can not find any camera");
                return;
            }
            int i = 0;
            DataTable dtCameraInfo = new DataTable();
            dtCameraInfo.Columns.Add(new DataColumn("name"));
            dtCameraInfo.Columns.Add(new DataColumn("value"));

            foreach (var item in cameraInfo)
            {
                DataRow row = dtCameraInfo.NewRow();
                BP.Camera myCmera = new BP.Camera(item);
                try
                {
                    myCmera.Open();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
              
                String strSerialNum = myCmera.Parameters[BP.PLCamera.DeviceID].GetValue();
               
               
                String strCameraInfo = String.Format("Camera{0}:serialNumber:{1}",i,strSerialNum);

                row["name"] = strCameraInfo;
                row["value"] = strSerialNum;
                myCmera.Close();
                dtCameraInfo.Rows.Add(row);


            }
            cmbCamera.DisplayMember = "name";
            cmbCamera.ValueMember = "value";

            cmbCamera.DataSource = dtCameraInfo;


        }
        private void cmbCamera_SelectedIndexChanged(object sender, EventArgs e)//选择相机
        {
            int cmbSelectIndex = cmbCamera.SelectedIndex;
            cameraData.SerialNumber = (String)cmbCamera.SelectedValue;
            labelSerialNumber.Text = cameraData.SerialNumber;


        }
 
     
        #endregion

        #region Method
        private void Init()
        {
           
           
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
           
        }
        private void ShowValues()
        {
            if (null == cameraData)
            {
                MessageBox.Show("Load configuration failed!");
                return;
            }
            try
            {
               
            }
            catch (Exception)
            {
            }
        }
        private bool SetValues()
        {
            if (null == cameraData)
            {
                return false;
            }

            try
            {
              
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (null == cameraData)
                {
                    throw new Exception();
                }
                if (null == driver)
                {
                    if (!HardwareManage.dicHardwareDriver.ContainsKey(cameraData.Name))
                    {
                        throw new Exception();
                    }
                    driver = (Aca500M)HardwareManage.dicHardwareDriver[cameraData.Name];
                }
                if(driver.IsConnected())
                {
                    labConnSta.Text = "Connect successfully.";
                    labConnSta.BackColor = Color.Green;
                   // btnOpen.Text = "Disconnect";
                }
                else
                {
                    labConnSta.Text = "Connect failed.";
                    labConnSta.BackColor = Color.Red;
                   // btnOpen.Text = "Connect";
                }
            }
            catch (Exception)
            {
                driver = null;
                labConnSta.Text = "Connect failed.";
                labConnSta.BackColor = Color.Red;
            } 
        }

        #endregion

       
    }
}
