using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Hardware;

namespace WorldGeneralLib.IO
{
    public class InputDriver
    {
        public string strDriverName;
        IInputAction actionInput;
        public string strRemark;
        public IOData inputData;
        public bool bPreStatus;
        public bool bready = false;
        public void Init(IOData data)
        {
            inputData = data;
            strDriverName = data.Name;
            try
            {
                if (HardwareManage.dicHardwareDriver[data.CardName] is IInputAction)
                {

                    actionInput = (IInputAction)HardwareManage.dicHardwareDriver[data.CardName];
                    strRemark = data.Remark;
                    bready = true;
                }
            }
            catch
            {

            }
        }
        public bool GetOn()
        {
            try
            {
                if (inputData.Ignore)
                {
                    return true;
                }
                if (bready == false)
                {
                    return false;
                }

                return inputData.Inversion ? !actionInput.GetInputBit(inputData.Index) : actionInput.GetInputBit(inputData.Index);
            }
            catch (Exception)
            {
              
            }
            return false;
        }
        public bool GetOff()
        {
            try
            {
                if (inputData.Ignore)
                {
                    return true;
                }
                if (bready == false)
                {
                    return false;
                }
                return inputData.Inversion ? actionInput.GetInputBit(inputData.Index) : !actionInput.GetInputBit(inputData.Index);
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool On
        {
            get
            {
                try
                {
                    if (inputData.Ignore)
                    {
                        return true;
                    }
                    if (bready == false)
                    {
                        return false;
                    }

                    return inputData.Inversion ? !actionInput.GetInputBit(inputData.Index) : actionInput.GetInputBit(inputData.Index);
                }
                catch (Exception)
                {

                }
                return false;
            }
        }
        public bool Off
        {
            get
            {
                try
                {
                    if (inputData.Ignore)
                    {
                        return true;
                    }
                    if (bready == false)
                    {
                        return false;
                    }
                    return inputData.Inversion ? actionInput.GetInputBit(inputData.Index) : !actionInput.GetInputBit(inputData.Index);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

    }
}
