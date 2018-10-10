using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using BP=Basler.Pylon;

namespace WorldGeneralLib.Hardware.Camera.Basler.Aca500M
{
    public class Aca500MData:HardwareData
    {
        private string _strIP;
        public string IP
        {
            get { return _strIP; }
            set { _strIP = value; }
        }
        private string _strSerialNumber;
        public string SerialNumber
        {
            get { return _strSerialNumber; }
            set { _strSerialNumber = value; }
        }

        private int _exposureTime;//曝光时间
        public int exposureTime
        {
            get { return _exposureTime; }
            set
            {
                int i = (int)value / 35;
                _exposureTime = 35 * i;
            }
        }
        private int _shotDelay;//触发延时
        public int shotDelay
        {
            get { return _shotDelay; }
            set { _shotDelay = value; }
        }
        private int _imageWidth;//图片宽度
        public int imageWidth
        {
            get { return _imageWidth; }
            set { _imageWidth = value; }
        }
        private int _imageHeight;//图片宽度
        public int imageHeight
        {
            get { return _imageHeight; }
            set { _imageHeight = value; }
        }
        private int _AOIWidth;
        public int AOIWidth
        {
            get { return _AOIWidth; }
            set { _AOIWidth = value; }
        }

        private String _gainAuto;//自动增益
        public String gainAuto
        {
            get { return _gainAuto; }
            set
            {
                if ("On" == value | "Off" == value)
                {
                    _gainAuto = value;
                }

            }
        }

        private int _gain;//自动增益
        public int gain
        {
            get { return _gain; }
            set { _gain = value; }
        }

        private int _maxPictureNumber;
        public int maxPictureNumber
        {
            get { return _maxPictureNumber; }
            set
            {
                if (value > 0)
                {
                    _maxPictureNumber = value;

                }
            }
        }
        private String _strPixelType;
        public string strPixelTypeNum
        {
            get { return _strPixelType; }
            set { _strPixelType = value; }
        }




        public int iTimeout;
      
        public Aca500MData()
        {
            _strIP = "192.168.0.1";
            _strSerialNumber = "00000001";
            _exposureTime = 35000;
            _strPixelType = "Mono8";
            _maxPictureNumber = 2;
            iTimeout = 500;
            Type = HardwareType.Camera;
            Name = "mycamera";
            Vender = HardwareVender.Basler;
            Text = "nothing";
            _gainAuto = "Off";

        }
    }
}
