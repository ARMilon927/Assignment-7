using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeShopWithLayerArchitecture.Manager;

namespace CoffeeShopAppWithDb
{
    public partial class Orders : Form
    {
        OrderManager _orderManager = new OrderManager();
        private DataTable dataTable;
        public Orders()
        {
            InitializeComponent();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(customerNameTextBox.Text))
            {
                MessageBox.Show("Please enter Customer name");
                return;
            }
            if (String.IsNullOrEmpty(itemNameTextBox.Text))
            {
                MessageBox.Show("Please enter item name");
                return;
            }
            if (String.IsNullOrEmpty(quantityTextBox.Text))
            {
                MessageBox.Show("Please enter quantity");
                return;
            }
            if (_orderManager.ValidCustomer(customerNameTextBox.Text, itemNameTextBox.Text))
            {
                MessageBox.Show("This customer exists") ;
            }
            else
                MessageBox.Show(_orderManager.InsertOrder(customerNameTextBox.Text, itemNameTextBox.Text, Convert.ToInt16(quantityTextBox.Text)));
            ClearInput();
        }

        private void ClearInput()
        {
            customerNameTextBox.Clear();
            itemNameTextBox.Clear();
            quantityTextBox.Clear();
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            ClearInput();
            dataTable = _orderManager.ShowOrder();
            if (dataTable.Rows.Count< 1)
                MessageBox.Show("No Data Found");
            orderDataGridView.DataSource = dataTable;
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if(_orderManager.ValidCustomer(customerNameTextBox.Text, itemNameTextBox.Text))
            {
                MessageBox.Show("This Customer exists");
            }
            else
            {
                int rowAffected = _orderManager.UpdateOrder(customerNameTextBox.Text, itemNameTextBox.Text, Convert.ToInt16(quantityTextBox.Text), Convert.ToInt16(idLabel.Text));
                if (rowAffected > 0)
                {
                    MessageBox.Show("Customer updated");
                    _orderManager.ShowOrder();
                }
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this customer?", "Delete Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
               int rowAffected = _orderManager.DeleteItem(Convert.ToInt16(idLabel.Text));
                if (rowAffected > 0)
                {
                    MessageBox.Show("Order is deleted successfully");
                    _orderManager.ShowOrder();
                }
            }
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(customerNameTextBox.Text))
                customerNameTextBox.Text = null;
            if (String.IsNullOrEmpty(itemNameTextBox.Text))
                itemNameTextBox.Text = null;
            if (String.IsNullOrEmpty(quantityTextBox.Text))
                quantityTextBox.Text = "0";
            dataTable = _orderManager.SearchOrder(customerNameTextBox.Text, itemNameTextBox.Text, quantityTextBox.Text);
            if (dataTable.Rows.Count < 1)
            {
                MessageBox.Show("No Data Found"); 
            }
            orderDataGridView.DataSource = dataTable;
        }
        private void orderDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            updateButton.Enabled = true;
            deleteButton.Enabled = true;
            searchButton.Enabled = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.orderDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                customerNameTextBox.Text = row.Cells[1].Value.ToString();
                itemNameTextBox.Text = row.Cells[2].Value.ToString();
                quantityTextBox.Text = row.Cells[3].Value.ToString();
            }
        }
        
    }
}
