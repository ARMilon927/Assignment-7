using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopWithLayerArchitecture.Repository
{
    class ItemRepository
    {
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private string commandString;
        private SqlDataAdapter sqlDataAdapter;
        private SqlDataReader reader;
        public bool ExistItem(string name, int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items WHERE Name = '" + name + "' AND Id <>" + id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            sqlConnection.Close();
            return isExist;
        }
        public int InsertItem(string name, int price)
        {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"INSERT INTO Items (Name, Price) Values ('" + name + "', " + price + ")";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return isExecuted;
        }
        public DataTable ShowItem()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        public int DeleteItem(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"DELETE FROM Items WHERE Id = " + id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return rowAffected;
        }
        public int UpdateItem(string name, int price, int id)
        {
            
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"UPDATE Items SET Name = '" + name + "', Price = " + price + " WHERE Id = " + id + "";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return isExecuted;
        }
        public DataTable SearchItem(string name, int price)
        {
            
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items WHERE Name = '" + name + "' OR Price = " + price + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;

        }
    }
}
