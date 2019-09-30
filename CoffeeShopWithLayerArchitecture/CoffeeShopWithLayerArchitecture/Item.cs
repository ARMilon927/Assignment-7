using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeShopWithLayerArchitecture.Manager;

namespace CoffeeShopAppWithDb
{
    public partial class ItemUI : Form
    {
        ItemManager _itemManager = new ItemManager();
        DataTable dataTable = new DataTable();
        public ItemUI()
        {
            InitializeComponent();
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(itemNameTextBox.Text))
            {
                MessageBox.Show("Please enter Item name");
                return;
            }
            if (String.IsNullOrEmpty(priceTextBox.Text))
            {
                MessageBox.Show("Please enter price of the item");
                return;
            }
            if (_itemManager.ExistItem(itemNameTextBox.Text, Convert.ToInt16(idLabel.Text)))
                MessageBox.Show("This name already exists");
            else
                MessageBox.Show(_itemManager.InsertItem(itemNameTextBox.Text, Convert.ToInt16(priceTextBox.Text)));
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            dataTable = _itemManager.ShowItem();
            if (dataTable.Rows.Count < 1)
                MessageBox.Show("No Data Found");
            itemDataGridView.DataSource = dataTable;
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this item?", "Delete Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                _itemManager.DeleteItem(Convert.ToInt16(idLabel.Text));
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (_itemManager.ExistItem(itemNameTextBox.Text, Convert.ToInt16(idLabel.Text)))
                MessageBox.Show("This name already exists");
            else
                if(_itemManager.UpdateItem(itemNameTextBox.Text, Convert.ToInt16(priceTextBox.Text), Convert.ToInt16(idLabel.Text))>0)
                MessageBox.Show("Item is updated");
            _itemManager.ShowItem();
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(itemNameTextBox.Text))
                itemNameTextBox.Text = null;
            if (String.IsNullOrEmpty(priceTextBox.Text))
                priceTextBox.Text = "0";
            dataTable = _itemManager.SearchItem(itemNameTextBox.Text,Convert.ToInt16(priceTextBox.Text));
            if (dataTable.Rows.Count<1)
                MessageBox.Show("No data found");
            itemDataGridView.DataSource = dataTable;
        }
        private void itemDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.itemDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                itemNameTextBox.Text = row.Cells[1].Value.ToString();
                priceTextBox.Text = row.Cells[2].Value.ToString();
            }
        }

        private void itemNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetterOrDigit(e.KeyChar)||(e.KeyChar == (Char)Keys.Back)||Char.IsWhiteSpace(e.KeyChar)))
                e.Handled = true;
        }

        private void priceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back)))
                e.Handled = true;
        }
    }
}
