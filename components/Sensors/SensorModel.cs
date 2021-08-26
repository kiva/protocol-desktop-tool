namespace EKYC.DesktopTool.Sensors.Secugen
{
    internal class SensorModel
    { 
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }

        internal static SensorModel Error
        {
            get
            {
                return new SensorModel();
            }
        }
    }
}