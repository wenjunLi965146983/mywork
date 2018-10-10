using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.TaskBase
{
    public class ListViewNF:ListView
    {
        public ListViewNF()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer,true);
        }
    }
}
