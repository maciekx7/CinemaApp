using System;
using System.IO;

namespace CinemaServer.FileReader
{
    
    public class DirectoryPath
    {
        public static string CONFIG_DIRECTORY(string filename)
        {
            if(Directory.GetParent(Directory.GetCurrentDirectory()).Name == "CinemaServer")
            {
                return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "config/" + filename);
            }
            return Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "config/"+filename);
        }


        public static string IMAGES_DIRECTORY(string filename)
        {
            if (Directory.GetParent(Directory.GetCurrentDirectory()).Name == "CinemaServer")
            {
                return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "extras/images/" + filename);
            }
            return Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "extras/images/" + filename);
        }

        public static string QRCODE_DIRECTORY(string filename)
        {
            if (Directory.GetParent(Directory.GetCurrentDirectory()).Name == "CinemaServer")
            {
                return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "extras/qr/" + filename);
            }
            return Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "extras/qr/" + filename);
        }

        public static string OCCASION_DIRECTORY(string filename)
        {
            if (Directory.GetParent(Directory.GetCurrentDirectory()).Name == "CinemaServer")
            {
                return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "extras/occasions/" + filename);
            }
            return Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "extras/occasions/" + filename);
        }
        

    }
    
}
