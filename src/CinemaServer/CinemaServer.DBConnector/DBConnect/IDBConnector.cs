using System;
using System.Data;

namespace CinemaServer.DBConnector.DBConnect
{
    public interface IDBConnector
    {
        public DataTable SELECT(String query);
        public void INSERT(String query);
        public void UPDATE(String query);
        public void DELETE(String query);
        public int COUNT(String query);
    }
}
