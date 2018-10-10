using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Hardware;

namespace WorldGeneralLib.IO
{
    public class OutputDriver
    {
        public string strDriverName;

        IOutputAction actionOutput;
        public string strRemark;
        public IOData outputData;

        public bool bready = false;

        public void Init(IOData data)
        {
            outputData = data;
            strDriverName = data.Name;
            try
            {
                if (HardwareManage.dicHardwareDriver[data.CardName] is IOutputAction)
                {

                    actionOutput = (IOutputAction)HardwareManage.dicHardwareDriver[data.CardName];
                    strRemark = data.Remark;
                    bready = true;
                }
            }
            catch
            {

            }
        }
        public bool SetOutBit(bool bOn)
        {
            try
            {
                if (outputData.Ignore)
                    return true;
                if (bready == false)
                {
                    return false;
                }
                return actionOutput.SetOutBit(outputData.Index, bOn);
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool GetOn()
        {
            try
            {
                if (outputData != null && IOManage.docIO.dicOutput.ContainsKey(outputData.Name))
                {
                    outputData = IOManage.docIO.dicOutput[outputData.Name];
                }
                else
                {
                    return false;
                }
                if (outputData.Ignore)
                    return true;
                if (bready == false)
                {
                    return true;
                }
                return actionOutput.GetOutBit(outputData.Index);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool GetOff()
        {
            try
            {
                if (outputData.Ignore)
                    return true;
                if (bready == false)
                {
                    return true;
                }
                return !actionOutput.GetOutBit(outputData.Index);
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
