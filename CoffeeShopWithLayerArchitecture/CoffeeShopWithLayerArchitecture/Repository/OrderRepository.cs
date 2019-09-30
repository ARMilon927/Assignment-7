using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopWithLayerArchitecture.Repository
{
    class OrderRepository
    {
        private SqlDataReader reader;
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        private SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        public bool ValidCustomer(string name, string item)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM OrderInformations WHERE CustomerName = '" + name + "' AND ItemName = '" + item + "' ";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            if (dataTable.Rows.Count > 0)
                return true;
            return false;
        }
        public int InsertOrder(string name, string item, int quantity)
        {
            int price = GetPrice(item);
            int totalPrice = price * quantity;
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"INSERT INTO OrderInformations (CustomerName, ItemName, Quantity, TotalPrice) Values ('" + name + "', '" + item + "', " + quantity + ", " + totalPrice + ")";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int isExecuted = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return isExecuted; 
            
        }
        public DataTable ShowOrder()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM OrderInformations";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
            
        }
        public int UpdateOrder(string name, string item, int quantity, int id)
        {
            int totalPrice = GetPrice(item);
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"UPDATE OrderInformations SET CustomerName = '" + name + "', ItemName = '" + item + "' , Quantity = " + quantity + ", TotalPrice = " + totalPrice + " WHERE Id = " + id + "";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return isExecuted;
        }

        public int DeleteItem(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"DELETE FROM OrderInformations WHERE Id = " + id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return rowAffected;
        }

        public DataTable SearchOrder(string name, string item, string quantity)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM OrderInformations WHERE CustomerName = '" + name + "' OR ItemName = '" + item + "' OR Quantity = " + quantity + " ";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        private int GetPrice(string item)
        {
            object getPrice;
            int price = 0;
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT Price FROM Items WHERE Items.Name = '"+ item +"' ";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                getPrice = dataTable.Rows[0][0];
                price = Convert.ToInt16(getPrice);
            }
            return price;
        }
      
    }
}
