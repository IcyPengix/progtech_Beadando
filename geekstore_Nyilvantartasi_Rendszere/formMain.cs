using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace geekstore_Nyilvantartasi_Rendszere
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
            database = new accessDatabase();
        }

        IDatabase database;

        private void formMain_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int quantity, price;
            if (!int.TryParse(tbQuantity.Text, out quantity) || !int.TryParse(tbPrice.Text, out price))
            {
                MessageBox.Show("Hát ez nem szám");
                return;
            }
            database.addNewProduct(tbName.Text, quantity, price);
            refreshTable();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int ID;
            if (!int.TryParse(tbID.Text, out ID))
            {
                MessageBox.Show("Hát ez nem nyert!");
                return;
            }
            database.deleteProduct(ID);
            refreshTable();
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            int id, quantity, price;
            if (!int.TryParse(tbID.Text, out id) || !int.TryParse(tbQuantity.Text, out quantity) || !int.TryParse(tbPrice.Text, out price))
            {
                MessageBox.Show("Hát nem jó");
                return;
            }
            database.updateProduct(id, tbName.Text, quantity, price);
            refreshTable();
        }

        private void formMain_Shown(object sender, EventArgs e)
        {
            refreshTable();
        }

        private void refreshTable()
        {
            int row = 0;
            if (dgvProducts.SelectedRows.Count > 0)
            {
                row = dgvProducts.SelectedRows[0].Index;
            }
            database.refreshDataGrid(dgvProducts);
            if (dgvProducts.Rows.Count > 0)
            {
                dgvProducts.Rows[0].Selected = false;
                dgvProducts.Rows[row].Selected = true;
            }
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count < 1)
            {
                return;
            }
            DataGridViewRow selectedRow = dgvProducts.SelectedRows[0];
            tbID.Text = selectedRow.Cells["ID"].Value.ToString();
            tbName.Text = selectedRow.Cells["Name"].Value.ToString();
            tbQuantity.Text = selectedRow.Cells["Quantity"].Value.ToString();
            tbPrice.Text = selectedRow.Cells["price"].Value.ToString();
        }
    }
}
