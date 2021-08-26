using System;
using System.Web.Http;
using EKYC.DesktopTool;
using EKYC.DesktopTool.Api.Models;
using EKYC.DesktopTool.Sensors;
using EKYC.DesktopTool.Sensors.Secugen;
using Newtonsoft.Json.Linq;
using Sentry;

namespace EKYC.DesktopTool.Api
{
    public partial class EKYCController : ApiController
    {
        [HttpGet]
        public object Fingerprint()
        {
            try
            {
                var result = SecugenDriver.GetFingerprintData();
                return new FingerprintModel()
                {
                    Token = "",
                    AccessToken = "",
                    Template = result.Image,
                    SerialNumber = result.SerialNumber,
                    SensorVendor = result.Manufacturer,
                    ComputerUsername= Environment.UserName,
                    ComputerName = Environment.MachineName,
                    ComputerLocalTime = DateTime.Now.ToString(),
                    ComputerTickCount = Environment.TickCount.ToString(),
                    ComputerIP = CurrentComputer.GetLocalIPAddress(),
                    ComputerMAC = CurrentComputer.GetMacAddress(),
                };
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                return 
                 new SensorFingerprintModel()
                 {
                     Success = false,
                 };
            }
        }

        [HttpGet]
        public object Info()
        {
            try
            {
                var result = SecugenDriver.GetSensorData();
                return new StatusModel()
                {
                    Token = "",
                    AccessToken = "",
                    SensorSerialNumber = result.SerialNumber,
                    SensorVendor = result.Manufacturer,
                    ComputerName = System.Environment.MachineName,
                    ComputerLocalTime = DateTime.Now.ToString(),
                    ComputerTickCount = System.Environment.TickCount.ToString(),
                    ComputerIP = CurrentComputer.GetLocalIPAddress(),
                    ComputerMAC = CurrentComputer.GetMacAddress(),
                };
            }
            catch (Exception ex)
            {
                return new StatusModel() { Success = false };
            }
        }
    }
}
