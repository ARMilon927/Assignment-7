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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        CustomerManager _customerManager = new CustomerManager();
        DataTable dataTable = new DataTable();
        private int rowAffected;
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(customerNameTextBox.Text))
            {
                MessageBox.Show("Please enter Customer name");
                return;
            }
            if (String.IsNullOrEmpty(contactTextBox.Text))
            {
                MessageBox.Show("Please enter a contact number");
                return;
            }
            if (String.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("Please enter address");
                return;
            }
            if (_customerManager.ExistCustomer(customerNameTextBox.Text, Convert.ToInt16(idLabel.Text)))
                MessageBox.Show("This name already exists");
            else
            {
                MessageBox.Show(_customerManager.InsertCustomer(customerNameTextBox.Text, contactTextBox.Text, addressTextBox.Text));
                ClearInput();
            }
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            ClearInput();
            dataTable = _customerManager.ShowCustomer();
            if (dataTable.Rows.Count < 1)
                MessageBox.Show("No Data Found");
            customerDataGridView.DataSource = dataTable;
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (_customerManager.ExistCustomer(customerNameTextBox.Text, Convert.ToInt16(idLabel.Text)))
                MessageBox.Show("This name already exists");
            else
            {
                rowAffected = _customerManager.UpdateCustomer(customerNameTextBox.Text, contactTextBox.Text, addressTextBox.Text, Convert.ToInt16(idLabel.Text));
                if (rowAffected > 0)
                {
                    MessageBox.Show("Customer updated");
                    ClearInput();
                }
                customerDataGridView.DataSource = _customerManager.ShowCustomer();
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this customer?", "Delete Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                rowAffected = _customerManager.DeleteCustomer(Convert.ToInt16(idLabel.Text));
                if (rowAffected > 0)
                    MessageBox.Show("Customer is deleted successfully");
                customerDataGridView.DataSource = _customerManager.ShowCustomer();
            }
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(customerNameTextBox.Text))
                customerNameTextBox.Text = null;
            if (String.IsNullOrEmpty(addressTextBox.Text))
                addressTextBox.Text = null;
            if (String.IsNullOrEmpty(contactTextBox.Text))
                contactTextBox.Text = null;
            dataTable = _customerManager.SearchCustomer(customerNameTextBox.Text, contactTextBox.Text, addressTextBox.Text);
            if (dataTable.Rows.Count < 1)
                MessageBox.Show("No Data Found");
            customerDataGridView.DataSource = dataTable;
        }
        private void customerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            updateButton.Enabled = true;
            deleteButton.Enabled = true;
            searchButton.Enabled = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.customerDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                customerNameTextBox.Text = row.Cells[1].Value.ToString();
                contactTextBox.Text = row.Cells[2].Value.ToString();
                addressTextBox.Text = row.Cells[3].Value.ToString();
            }
        }
        private void ClearInput()
        {
            customerNameTextBox.Clear();
            contactTextBox.Clear();
            addressTextBox.Clear();
        }
        private void customerNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetterOrDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back) || Char.IsWhiteSpace(e.KeyChar)))
                e.Handled = true;
        }

        private void contactTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back)))
                e.Handled = true;
        }
    }
}







