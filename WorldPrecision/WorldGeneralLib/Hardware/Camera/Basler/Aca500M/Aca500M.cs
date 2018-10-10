using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using WorldGeneralLib.TaskBase;
using WorldGeneralLib.Network.TCPIP.TCPClient;
using BP = Basler.Pylon;
using System.Drawing;
using Basler.Pylon;
using PylonC.NETSupportLibrary;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace WorldGeneralLib.Hardware.Camera.Basler.Aca500M
{
    public class Aca500M:HardwareBase,ICamera
    {
        public Bitmap bitmap;
        public bool empty;
        public Aca500MData cameraData;
        private object _objLock;
        private BP.Camera _aca500M_Camera;
        private bool bAsync;
        public BP.Camera aca500M_Camera
        {
            get { return _aca500M_Camera; }
            set { _aca500M_Camera = value; }
        }
        private BP.PixelType _pixeType;//像素格式
        public BP.PixelType pixelType
        {
            get { return _pixeType; }
            set { _pixeType = value; }
        }


        public Aca500M(Aca500MData data)
        {
            cameraData = data;
            _objLock = new object();
            if(data.SerialNumber != "00000001")
            {
                Init(cameraData);
            }
            
        }
       
        public override bool Init(HardwareData hardeareData)
        {
            bInitOk = false;     
            #region TCP/IP
            if(!(null == _aca500M_Camera))
            {
                _aca500M_Camera.Dispose();
            }
            try
            {
                aca500M_Camera = new BP.Camera(cameraData.SerialNumber);
                GetParameter();
                if (!_aca500M_Camera.IsOpen)
                {
                    _aca500M_Camera.Open();
                }
                if (!_aca500M_Camera.StreamGrabber.IsGrabbing)
                {
                    _aca500M_Camera.StreamGrabber.Start(cameraData.maxPictureNumber, GrabStrategy.OneByOne, GrabLoop.ProvidedByUser);
                    bAsync = false;
                }


                bInitOk = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);          
            }       
            #endregion
            return bInitOk;
        }
   
      

        public override bool Close()
        {
            if(null!= _aca500M_Camera)
            {
                if (_aca500M_Camera.StreamGrabber.IsGrabbing)
                {
                    _aca500M_Camera.StreamGrabber.Stop();
                }
                if (_aca500M_Camera.IsOpen)
                {
                    _aca500M_Camera.Close();
                }
            }
            bInitOk = false;
            return bInitOk;
        }

        public void GetParameter()
        {
            if (null != this.aca500M_Camera)
            {
                if (!this.aca500M_Camera.IsOpen)
                {
                    this.aca500M_Camera.Open();
                }
                this.cameraData.SerialNumber= this.aca500M_Camera.Parameters[BP.PLCamera.DeviceID].GetValue();
                this.cameraData.exposureTime = (int)this.aca500M_Camera.Parameters[BP.PLCamera.ExposureTimeRaw].GetValue();
                this.cameraData.gainAuto = this.aca500M_Camera.Parameters[BP.PLCamera.GainAuto].GetValue();
                this.cameraData.imageHeight = (int)this.aca500M_Camera.Parameters[BP.PLCamera.Height].GetValue();
                this.cameraData.imageWidth = (int)this.aca500M_Camera.Parameters[BP.PLCamera.Width].GetValue();
                this.cameraData.gain= (int)this.aca500M_Camera.Parameters[BP.PLCamera.GainRaw].GetValue();
            }
        }
        public void SetParameter()
        {
            if (null != this.aca500M_Camera)
            {
                if (!this.aca500M_Camera.IsOpen)
                {
                    this.aca500M_Camera.Open();
                }
                this.aca500M_Camera.Parameters[BP.PLCamera.ExposureTimeRaw].SetValue(this.cameraData.exposureTime);
                this.aca500M_Camera.Parameters[BP.PLCamera.GainAuto].SetValue(this.cameraData.gainAuto);
                this.aca500M_Camera.Parameters[BP.PLCamera.GainRaw].SetValue(this.cameraData.gain);
            }
        }
        /// <summary>
        /// 同步方式抓图
        /// </summary>
        /// <param name="strBarCode">读码结果</param>
        /// <param name="iTimeout">超时时间</param>
        /// <returns></returns>
        public bool GrabImage(out IGrabResult grabResult)//同步单次拍照
        {

            Stopwatch sw= new Stopwatch();

            Exception ex = new Exception("Please open the camera");
           
         
            if (!_aca500M_Camera.IsOpen)
            {
               MessageBox.Show(ex.Message);
            }
            if (!_aca500M_Camera.StreamGrabber.IsGrabbing)
            {

                _aca500M_Camera.StreamGrabber.Start(cameraData.maxPictureNumber, GrabStrategy.OneByOne, GrabLoop.ProvidedByUser);
                bAsync = false;

            }
            else
            {
                if (bAsync)
                {
                    _aca500M_Camera.StreamGrabber.Stop();
                    _aca500M_Camera.StreamGrabber.Start(cameraData.maxPictureNumber, GrabStrategy.OneByOne, GrabLoop.ProvidedByUser);
                    bAsync = false;
                }

            }

            try
            {
                sw.Start();
                _aca500M_Camera.WaitForFrameTriggerReady(1000, TimeoutHandling.ThrowException);
                _aca500M_Camera.ExecuteSoftwareTrigger();
                grabResult = _aca500M_Camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                sw.Stop();
              
            }
            catch (Exception th_ex)
            {
                MessageBox.Show(th_ex.Message);
                grabResult = null;
                return false;
            }          
            return true;
            
        }


        /// <summary>
        /// 异步方式读取抓图
        /// </summary>
        /// <returns></returns>


        public bool GrabImage()
        {
            
            Exception ex = new Exception("Please open the camera");

            if (!_aca500M_Camera.IsOpen)
            {
                MessageBox.Show(ex.Message);
            }
            if (!_aca500M_Camera.StreamGrabber.IsGrabbing)
            {
               
                _aca500M_Camera.StreamGrabber.Start(cameraData.maxPictureNumber, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                bAsync = true;

            }
            else
            {
                if (!bAsync)
                {
                    _aca500M_Camera.StreamGrabber.Stop();
                    _aca500M_Camera.StreamGrabber.Start(cameraData.maxPictureNumber, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                    bAsync = true;
                }

            }
            

            try
            {
               
                _aca500M_Camera.WaitForFrameTriggerReady(1000, TimeoutHandling.ThrowException);
                _aca500M_Camera.ExecuteSoftwareTrigger();
           

            }
            catch (Exception th_ex)
            {
                MessageBox.Show(th_ex.Message);
             
                return false;
            }
            return true;

        }


    }
}
