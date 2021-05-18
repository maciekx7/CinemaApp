using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CinemaServer.DBConnector.DBInfo
{
    public class DBInfoData
    {
        private readonly static string filename = "json/database.json";

        public string Server { private set; get; }
        public string DatabaseName { get; private set; }
        public string UserName { get; private set; }
        public string Password { private set;  get; }


        private DBInfoData()
        {
            LoadConfig(FileReader.DirectoryPath.CONFIG_DIRECTORY(filename));
        }


        private static DBInfoData _instance = null;
        private static readonly object _lock = new object();

        public static DBInfoData GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DBInfoData();
                    }
                }
            }
            return _instance;
        }

        private void LoadConfig(string filenamee)
        {
            var jsonFile = File.ReadAllText(filenamee);
            DBInfoModel dbModel = JsonSerializer.Deserialize<DBInfoModel>(jsonFile);

            Server = dbModel.Server;
            UserName = dbModel.User;
            Password = dbModel.Password;
            DatabaseName = dbModel.Database;
        }

    }
}
