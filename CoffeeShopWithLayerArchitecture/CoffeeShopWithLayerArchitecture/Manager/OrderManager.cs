using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShopWithLayerArchitecture.Repository;

namespace CoffeeShopWithLayerArchitecture.Manager
{
    class OrderManager
    {
        OrderRepository _orderRepository = new OrderRepository();
        public bool ValidCustomer(string name, string item)
        {
            return _orderRepository.ValidCustomer(name, item);
        }

        public string InsertOrder(string name, string item, int quantity)
        {
            string message;
            message = _orderRepository.InsertOrder(name, item, quantity) > 0 ? "Order is Saved" : "Order is not Saved";
            return message;
        }

        public DataTable ShowOrder()
        {
            return _orderRepository.ShowOrder();
        }
        public DataTable SearchOrder(string name, string item, string quantity)
        {
            return _orderRepository.SearchOrder(name, item, quantity);
        }

        public int DeleteItem(int id)
        {
            return _orderRepository.DeleteItem(id);
        }

        public int UpdateOrder(string name, string item, int quantity, int id)
        {
            return _orderRepository.UpdateOrder(name, item, quantity, id);
        }
        //if (dataTable.Rows.Count > 0)
        //{
        //    customerId = dataTable.Rows[0][0];
        //    itemId = dataTable.Rows[0][1];
        //    price = dataTable.Rows[0][2];
        //    
        //    return true;
        //}
        //MessageBox.Show("Customer does not exist");
    }
}
