using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware.Camera.ImagingSource.GigE
{
    public class ImagingSourceGigE:HardwareBase,ICamera
    {
        private ImagingSourceGigEData _camData;

        public ImagingSourceGigE(ImagingSourceGigEData camData)
        {
            this._camData = camData;
        }

        public override bool Init(HardwareData hardeareData)
        {
            return base.Init(hardeareData);
        }
    }
}
