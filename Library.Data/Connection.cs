using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class Connection
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Chito\\OneDrive\\Escritorio\\FINAL PROJECT IDS (2)\\FINAL PROJECT IDS\\Library.Data\\LibraryDB.mdf\";Integrated Security=True";
        public SqlConnection connection;

        public Connection(){
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection() {
            if(connection.State != System.Data.ConnectionState.Open){
                connection.Open();
            }       
        }

        public void CloseConnection(){
            if (connection.State != System.Data.ConnectionState.Closed){ 
                connection.Close();
            }  
        }
    }   
}
