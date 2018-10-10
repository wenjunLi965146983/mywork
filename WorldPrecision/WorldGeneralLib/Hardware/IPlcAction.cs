using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware
{
    public enum PlcResonse
    {
        SUCCESS,
        TIMEOUT,
        INITFAIL,
        ADDWRONG,
        ERROR,
        FCSERR,
        OTHERS
    }

    interface IPlcAction
    {
        /// <summary>
        /// Read coils from slave asynchronous. The result is given in the response function.
        /// </summary>
        /// <returns></returns>
        PlcResonse GetSingleBit(string strItemName);
        /// <summary>
        /// Read coils from slave synchronous. 
        /// </summary>
        /// <returns></returns>
        /// 
        PlcResonse GetMultiBits(string strItemName);
        PlcResonse SetSingleBit(string strItemName, bool bValue);
        PlcResonse SetMultiBits(string strItemName, int iNumBits, byte[] values);
        PlcResonse GetWord(string strItemName);
        PlcResonse SetWord(string strItemName, string strValue);

    }
}
