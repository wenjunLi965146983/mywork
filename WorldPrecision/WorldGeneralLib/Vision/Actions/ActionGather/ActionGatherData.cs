using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldGeneralLib.Vision.Actions.Gather
{
    public enum ImageSource:short
    {
        Camera,
        Local
    }

    public enum LedControl:short
    {
        IO,
        LedController
    }
    public class CameraParam
    {
        //相机设定
        public int iShutterSpeed;
        public int iGain;
        public LedControl ledCtrl;
        public string strControlCardName;
        public int iIoNum;
        public string strLedControllerName;
        public int iLuminance;
        public int iChannel;

        //相机绑定
        public string strCamBingding;

        //白平衡
        public int iWhiteBalanceR;
        public int iWhiteBalanceG;
        public int iWhiteBalanceB;

        public CameraParam()
        {
            iShutterSpeed = 2000;
            iGain = 10;
            ledCtrl = LedControl.IO;
            strControlCardName = string.Empty;
            iIoNum = 0;
            strLedControllerName = string.Empty;
            iLuminance = 50;
            iChannel = 0;
            strCamBingding = string.Empty;

            iWhiteBalanceR = 0;
            iWhiteBalanceG = 0;
            iWhiteBalanceB = 0;
    }
    }
    public class ActionGatherData : ActionDataBase
    {
        public int index = 0;
        public List<CameraParam> listCamParam = null;
        private String _strCameraName;
        public String strCameraName
        {
            set { _strCameraName = value; }
            get { return _strCameraName; }
        }
        //图像输入
        public ImageSource eimageSrc;
        public string strLocalImgSrcPath;
        public int iSrcCamIndex;

        public ActionGatherData()
        {
            Name = "图像输入";
        }

        public ActionGatherData(string strName):this()
        {
            eimageSrc = ImageSource.Local;

            if (null == listCamParam)
            {
                listCamParam = new List<CameraParam>();
                for (int index = 0; index < VisionManage.MaxCameraCount; index++)
                {
                    listCamParam.Add(new CameraParam());
                }
            }

            Name = strName;
        }
    }
}
