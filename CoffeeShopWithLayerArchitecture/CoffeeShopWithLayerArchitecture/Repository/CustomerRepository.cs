using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopWithLayerArchitecture.Repository
{
    class CustomerRepository
    {
        private SqlDataReader reader;
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        private SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        public bool ExistCustomer(string name, int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers WHERE Name = '" + name + "' AND Id <>" + id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            sqlConnection.Close();
            return isExist;
        }
        public int InsertCustomer(string name,string contact, string address)
        {
            
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"INSERT INTO Customers (Name, Contact, Address) Values ('" + name + "', " + contact + ", '" + address + "')";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return isExecuted;
        }
        public DataTable ShowCustomer()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        public int UpdateCustomer(string name, string contact, string address, int id)
        {
            
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"UPDATE Customers SET Name = '" + name + "', Contact = '" + contact +"', Address = '" + address + "' WHERE Id = " + id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int isExecuted = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return isExecuted;
        }

        public int DeleteCustomer(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"DELETE FROM Customers WHERE Id = " + id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return rowAffected;
        }
        public DataTable SearchCustomer(string name, string contact, string address)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers WHERE Name = '" + name + "' OR Contact = '" + contact + "' OR Address = '" + address + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
}
