using EKYC.DesktopTool.Sensors;
using SecuGen.FDxSDKPro.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKYC.DesktopTool.Sensors.Secugen
{
    internal class SecugenDriver : IFingerprintSensor
    {

        internal static SensorFingerprintModel GetFingerprintData()
        {
            
            SGFPMDeviceName device_name = SGFPMDeviceName.DEV_FDU05;
            var fingerprintManager = new SGFingerPrintManager();
            var code = fingerprintManager.Init(device_name);
            var errorCode = fingerprintManager.OpenDevice(0x255);
            if (errorCode != (int)SGFPMError.ERROR_NONE)
            {
                return new SensorFingerprintModel()
                {
                    Success = false                    
                };
            }
            fingerprintManager.SetLedOn(false);
            SGFPMDeviceInfoParam deviceInfo = new SGFPMDeviceInfoParam();
            deviceInfo = new SGFPMDeviceInfoParam();
            errorCode = fingerprintManager.GetDeviceInfo(deviceInfo);
            if (errorCode != (int)SGFPMError.ERROR_NONE)
            {
                return SensorFingerprintModel.Error;
            }
            fingerprintManager.SetLedOn(true);
            var m_ImageWidth = deviceInfo.ImageWidth;
            var m_ImageHeight = deviceInfo.ImageHeight;
            var fp_image = new byte[m_ImageWidth * m_ImageHeight];
            errorCode = fingerprintManager.GetImageEx(fp_image, 60000, 0, 80);
            if (errorCode != (int)SGFPMError.ERROR_NONE)
            {
                fingerprintManager.SetLedOn(false);
                return SensorFingerprintModel.Error;
            }
            var image = EncodeImage(fp_image, ImageFormat.Png);
            fingerprintManager.SetLedOn(false);
            fingerprintManager.CloseDevice();
            return new SensorFingerprintModel(){
                Template = ImageToTemplate(image),
                SerialNumber = BitConverter.ToString(deviceInfo.DeviceSN),
                Manufacturer= "Secugen Corporation"
            };
        }

        internal static SensorModel GetSensorData()
        {
            SGFPMDeviceName device_name = SGFPMDeviceName.DEV_FDU05;
            var fingerprintManager = new SGFingerPrintManager();
            var code = fingerprintManager.Init(device_name);
            var errorCode = fingerprintManager.OpenDevice(0x255);
            if (errorCode != (int)SGFPMError.ERROR_NONE)
            {
                return SensorModel.Error;
            }
            fingerprintManager.SetLedOn(false);
            SGFPMDeviceInfoParam deviceInfo = new SGFPMDeviceInfoParam();
            errorCode = fingerprintManager.GetDeviceInfo(deviceInfo);
            fingerprintManager.CloseDevice();
            return new SensorModel()
            {
                SerialNumber = BitConverter.ToString(deviceInfo.DeviceSN),
                Manufacturer = "Secugen Corporation"
            };
        }

        public static string ImageToTemplate(Image source)
        {
            byte[] imageBytes = m.ToArray();
            var options = new FingerprintImageOptions { Dpi = 500 };
            var template = new FingerprintTemplate(new FingerprintImage(300, 400, imageBytes, options));
            return template.ToString();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            Console.WriteLine(returnImage.RawFormat.Guid);
            return returnImage;
        }

        public static Bitmap EncodeImage(byte[] rawBytes, ImageFormat imageFormat)
        {
            int width = 300;
            int height = 400;
            int colorLevel = 256;
            float fDpi = 500;
            Bitmap bm;
            unsafe
            {
                fixed (byte* p = rawBytes)
                {
                    int stride = (width * 8 + 7) / 8;
                    IntPtr scan0 = new IntPtr(p);
                    bm = new Bitmap(width, height, stride, PixelFormat.Format8bppIndexed, scan0);
                    bm.SetResolution(fDpi, fDpi);
                    ColorPalette palette = bm.Palette;
                    Color[] entries = palette.Entries;
                    for (int i = 0; i < entries.Length; ++i)
                    {
                        int c = i * colorLevel / entries.Length;
                        entries[i] = Color.FromArgb(c, c, c);
                    }
                    bm.Palette = palette;
                    MemoryStream ms = new MemoryStream();
                    bm.Save(ms, imageFormat);
                    return bm;
                }
            }
        }
    }
}