using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware.CodeReader
{
    public interface ICodeReader
    {
        CodeReaderRes Read(out string strBarCode);
        CodeReaderRes Read();
    }
}
