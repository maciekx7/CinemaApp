using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CinemaServer.FileReader
{
    public class EImage
    {
        public enum EImageFormat
        {
            jpeg,
            png,
            jpg
        }
        private static readonly Dictionary<string, EImageFormat> ImageExtensionDic = new Dictionary<string, EImageFormat>
        {
            { "jpg", EImageFormat.jpg },
            { "jpeg", EImageFormat.jpeg },
            { "png", EImageFormat.png }
        };

        public static EImageFormat FileExtension(string image)
        {
            string[] newExt;
            try
            {
                var ext = Path.GetExtension(image);
                newExt = ext.Split(".");
                return ImageExtensionDic[newExt[1]];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return EImageFormat.jpeg;
                
            }
        }

        public static string FileExtension(EImageFormat format)
        {
            var key = ImageExtensionDic.FirstOrDefault(x => x.Value == format).Key;

            return key;
        }

    }

}
