using System;
using System.Data;
using MySqlConnector;


namespace CinemaServer.DBConnector.DBConnect
{
    public class DBConnectorAPI : IDBConnector
    {
        #region PRIVATE
        #region atributes
        private DBInfo.DBInfoData dbInfo = DBInfo.DBInfoData.GetInstance();

        private MySqlConnection Connection { get; set; }
        #endregion

        #region Constructor
        private DBConnectorAPI()
        {
            InitializeConnection();
        }
        #endregion

        #region Singletone
        private static DBConnectorAPI _instance = null;
        private static readonly object _lock = new object();

        public static DBConnectorAPI GetInstance()
        {
            if (_instance == null)
            {
                lock(_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DBConnectorAPI();
                    }
                }
            }
            return _instance;
        }
        #endregion

        #region InitializeConnection
        private void InitializeConnection()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = dbInfo.Server,
                Database = dbInfo.DatabaseName,
                UserID = dbInfo.UserName,
                Password = dbInfo.Password,
            };

            Connection = new MySqlConnection(builder.ConnectionString);
        }
        #endregion

        #region Open and Close connection
        private bool OpenConnection()
        {
            try
            {
                Connection.Open();
                //Console.WriteLine("Connection opened");
                return true;
            }
            catch (MySqlException ex)
            {
                switch(ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                    default:
                        Console.WriteLine(ex.Message);
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                Connection.Close();
                //Console.WriteLine("Connection closed");

                return true;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #endregion

        #region PUBLIC
        #region SQL query commands - Interface implementation
        public DataTable SELECT(string Query)
        {
            string query = Query;

            DataTable data = new DataTable();

            if(this.OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, Connection);

                MySqlDataReader dataReader = command.ExecuteReader();

                data.Load(dataReader);

                dataReader.Close();

                this.CloseConnection();
            }
            return data;
        }



        public void INSERT(string query)
        {
            if(this.OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, Connection);

                command.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void UPDATE(string query)
        {
            if(this.OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand();

                command.CommandText = query;

                command.Connection = Connection;

                command.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void DELETE(string query)
        {
            if(this.OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, Connection);

                command.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public int COUNT(string query)
        {
            int count = -1;

            if(this.OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, Connection);

                count = int.Parse(command.ExecuteScalar() + "");

                this.CloseConnection();
            }

            return count;
        }

        #endregion
        #endregion
    }
}
