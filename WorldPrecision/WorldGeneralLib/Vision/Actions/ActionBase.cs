using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using WorldGeneralLib.Vision;
using Emgu.CV;
using Emgu.CV.Structure;

namespace WorldGeneralLib.Vision.Actions
{
    public class ActionBase
    {
        public Form formAction;
        public ActionDataBase actionData;

        public ActionResponse actionRes;


        protected Image<Gray, Byte> _imageResult;//输出图像
        public Image<Gray, Byte> imageResult
        {
            get { return _imageResult; }
            set { _imageResult = value; }
        }
        protected Image<Gray, Byte> _imageInput;//图像输入

        public Image<Gray, Byte> imageInput
        {
            get { return _imageInput; }
            set
            {
                _imageInput = value;

            }
        }

        public virtual void Init()
        {

        }
        public virtual void ActionExcute()
        {

        }
    }
}
