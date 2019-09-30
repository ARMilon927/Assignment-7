using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShopWithLayerArchitecture.Repository;

namespace CoffeeShopWithLayerArchitecture.Manager
{
    class CustomerManager
    {
        CustomerRepository _customerRepository = new CustomerRepository();
        public bool ExistCustomer(string name, int id)
        {
            return _customerRepository.ExistCustomer(name, id);
        }

        public string InsertCustomer(string name, string contact, string address)
        {
            string message;
            message = _customerRepository.InsertCustomer(name, contact, address) > 0 ? "Customer is Saved" : "Customer is not saved";
            return message;
        }

        public DataTable ShowCustomer()
        {
            return _customerRepository.ShowCustomer();
        }

        public int DeleteCustomer(int id)
        {
            return _customerRepository.DeleteCustomer(id);
        }

        public int UpdateCustomer(string name, string contact, string address, int id)
        {
            return _customerRepository.UpdateCustomer(name, contact, address, id);

        }
        public DataTable SearchCustomer(string name, string contact, string address)
        {
            return _customerRepository.SearchCustomer(name, contact, address);
        }
    }
}
