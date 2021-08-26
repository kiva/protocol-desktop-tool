using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKYC.DesktopTool.Sensors
{
    internal class SensorFingerprintModel
    {
        internal bool Success { get; set; }
        internal string SerialNumber { get; set; }
        internal string Manufacturer { get; set; }
        internal string Image { get; set; }
        internal static SensorFingerprintModel Error
        {
            get
            {
                return new SensorFingerprintModel();
            }
        }
    }
}