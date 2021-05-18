using System;
using System.Drawing;
using System.IO;
using CinemaServer.FileReader;
namespace CinemaServer.FileReader
{
    public class ImageToBytes
    {
        public ImageToBytes()
        {

        }

        public static byte[] ImageToByteArray(string filePath, EImage.EImageFormat format)
        {
            //Console.WriteLine("converting");

            try
            {
                Image img = Image.FromFile(filePath);

                using (MemoryStream ms = new MemoryStream())
                {
                    switch(format)
                    {
                        case EImage.EImageFormat.jpeg:
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case EImage.EImageFormat.jpg:
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case EImage.EImageFormat.png:
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                    var bytes = ms.ToArray();
                    //Console.WriteLine("converted");
                    
                    return bytes;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new byte[1];
            }

        }
    }
}
