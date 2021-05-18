using System;
using System.Drawing;
using CinemaServer.FileReader;
using static CinemaServer.FileReader.EImage;

namespace CinemaServer.Rest.Model.APIModels
{
    public class ImageData
    {
        public string Name { get; private set; }
        public byte[] Data { get; private set; }
        public int Data_lenght { get; private set; }
        public string Format { get; private set; }

        public ImageData(string image)
        {
            var path = DirectoryPath.IMAGES_DIRECTORY(image);
            this.Name = image;
            this.Format = FileExtension(image).ToString();
            this.Data = ImageToBytes.ImageToByteArray(
                path,
                FileExtension(this.Format));
            this.Data_lenght = this.Data.Length;
        }
    }
}
