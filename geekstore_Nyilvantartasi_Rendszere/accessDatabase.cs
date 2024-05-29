using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geekstore_Nyilvantartasi_Rendszere
{
    public class accessDatabase : IDatabase
    {
        string strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_geekstore.mdb";
        public void addNewProduct(string name, int quantity, int price)
        {
            using (OleDbConnection con = new OleDbConnection(strConnect))
            {
                con.Open();
                using (OleDbCommand cmd = new OleDbCommand($"INSERT INTO tbl_products(Name,Quantity,Price) VALUES('{name}',{quantity}, {price});", con))
                {
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public void deleteProduct(int ID)
        {
            using (OleDbConnection con = new OleDbConnection(strConnect))
            {
                con.Open();
                using (OleDbCommand cmd = new OleDbCommand($"DELETE FROM tbl_products WHERE ID={ID};", con))
                {
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            
        }

        public Product getProduct(int ID)
        {
            using (OleDbConnection con = new OleDbConnection(strConnect))
            {
                con.Open();
                using (OleDbCommand cmd = new OleDbCommand($"SELECT * FROM tbl_products WHERE ID={ID}", con))
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataRow[] dataRows = dt.Select();
                    if (dataRows.Length == 0 )
                    {
                        return null;
                    }
                    return new Product(
                        (int)dataRows[0].ItemArray[0],
                        (string)dataRows[0].ItemArray[1],
                        (int)dataRows[0].ItemArray[2],
                        (int)dataRows[0].ItemArray[3]); 
                }
                con.Close();
            }
        }

        public Product[] getProducts()
        {
            List<Product> products = new List<Product>();
            using (OleDbConnection con = new OleDbConnection(strConnect))
            {
                con.Open();
                using (OleDbCommand cmd = new OleDbCommand($"SELECT * FROM tbl_products", con))
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataRow[] dataRows = dt.Select();
                    foreach (DataRow dataRow in dataRows)
                    {
                        Product p = new Product(
                        (int)dataRow.ItemArray[0],
                        (string)dataRow.ItemArray[1],
                        (int)dataRow.ItemArray[2],
                        (int)dataRow.ItemArray[3]);
                        products.Add(p);
                    }
                }
                con.Close();
            }
            return products.ToArray();
        }

        public void refreshDataGrid(DataGridView dataGridView)
        {
            using (OleDbConnection con = new OleDbConnection(strConnect))
            {
                con.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM tbl_products", con))
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView.DataSource = dt;
                    dataGridView.Invalidate();
                }
                con.Close();
            }
        }

        public void updateProduct(int ID, string name, int quantity, int price)
        {
            using (OleDbConnection con = new OleDbConnection(strConnect))
            {
                con.Open();
                using (OleDbCommand cmd = new OleDbCommand($"UPDATE tbl_products SET Name='{name}', Quantity={quantity}, Price={price} WHERE ID={ID};", con))
                {
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
