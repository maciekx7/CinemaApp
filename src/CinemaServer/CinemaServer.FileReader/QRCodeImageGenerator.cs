using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace CinemaServer.FileReader
{
    public class QRCodeImageGenerator
    {
        public static void saveQRToDirectory(string qrData, string filename)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            qrCodeImage.Save(DirectoryPath.QRCODE_DIRECTORY(filename), ImageFormat.Png);
            Console.WriteLine($"[QRCODE] {filename}");
        }

        public static void deleteQRFromDirectory(string filename)
        {
            if (File.Exists(DirectoryPath.QRCODE_DIRECTORY(filename)))
            {
                try
                {
                    File.Delete(DirectoryPath.QRCODE_DIRECTORY(filename));
                }
                catch (Exception ex)
                {
                    //Do something
                }
            }
        }

        
    }
}
