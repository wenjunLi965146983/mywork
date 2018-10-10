using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Vision.Actions;
using HalconDotNet;
using WorldGeneralLib.Hardware.Camera.Basler.Aca500M;
using WorldGeneralLib.Hardware;
using System.Windows.Forms;
using Emgu.CV;
using Basler.Pylon;
using System.Drawing;
using PylonC.NETSupportLibrary;
using Emgu.CV.Structure;

namespace WorldGeneralLib.Vision.Actions.Gather
{
    public class ActionGather:ActionBase,IAction
    {
        /// <summary>
        /// 采集到的图像队列
        /// </summary>
        private Queue _queueImage;
        public Aca500M aca500M;
        private ActionGatherData _actionGatherData;

        /// <summary>
        /// 当前处理的图像
        /// </summary>
        public HObject hImage;
        public ActionGather(ActionGatherData actionGatherData)
        {
            _queueImage = new Queue();
            hImage = null;
            
            actionData = actionGatherData;
            actionData.Name = actionGatherData.Name;
            actionRes = ActionResponse.NonExecution;
            _actionGatherData = actionGatherData;
            if (null != _actionGatherData.strCameraName)
            {
                Init();
            }
            formAction = (FormActionGather)(new FormActionGather(actionGatherData,this));
          
           
        }
        public override void ActionExcute()
        {
            if (0 == actionData.imageSrc)
            {
                
                    OpenFileDialog lvse = new OpenFileDialog();
                    lvse.Title = "选择图片";
                    lvse.InitialDirectory = "";
                    lvse.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.gif;*png";
                    lvse.FilterIndex = 1;


                    if (lvse.ShowDialog() == DialogResult.OK)
                    {

                        Mat mat = CvInvoke.Imread(lvse.FileName, Emgu.CV.CvEnum.ImreadModes.AnyColor);


                        try
                        {
                        imageResult.Bitmap = mat.Bitmap;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                }
                else
                {
                    if(null== aca500M)
                    {
                        Init();
                    }
                    
                    aca500M.GrabImage(out IGrabResult grabResult);
                    byte[] b = grabResult.PixelData as byte[];
                    Bitmap bitmap = null;
                    BitmapFactory.CreateBitmap(out bitmap, grabResult.Width, grabResult.Height, false);
                    BitmapFactory.UpdateBitmap(bitmap, b, grabResult.Width, grabResult.Height, false);
                    Image<Gray, byte> image = new Image<Gray, byte>(bitmap);
                }
               
            }
        }
        public override void Init()
        {
            base.Init();
            aca500M = (Aca500M)HardwareManage.dicHardwareDriver[_actionGatherData.strCameraName];
            aca500M.Init(aca500M.cameraData);
        }

        //Action处理方法
        //TODO
    }
}
