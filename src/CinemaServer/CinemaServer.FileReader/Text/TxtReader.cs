using System;
using System.Collections.Generic;
using System.IO;

namespace CinemaServer.FileReader.Text
{
    
    public class TxtReader
    {
        static string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "config/abc.txt");
        //static readonly string textFile = "";


        public static List<string> getStringList(string filename)
        {

            string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, $"config/{filename}");


            List<string> list = new List<string>();
            if (File.Exists(startupPath))
            {
                // Read a text file line by line.  
                string[] lines = File.ReadAllLines(startupPath);
                foreach (string line in lines)
                    list.Add(line);
            }
            return list;
        }

        public static void readTxt()
        {
            if (File.Exists(startupPath))
            {
                // Read a text file line by line.  
                string[] lines = File.ReadAllLines(startupPath);
                foreach (string line in lines)
                    Console.WriteLine(line);
            }
        }
    }
}
