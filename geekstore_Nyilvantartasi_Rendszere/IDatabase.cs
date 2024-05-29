using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geekstore_Nyilvantartasi_Rendszere
{
    public interface IDatabase
    {
        // ID, Name, Quantity, Price - tbl_products

        void addNewProduct(string name, int quantity, int price);
        void updateProduct(int ID, string name, int quantity, int price);
        void deleteProduct(int ID);
        Product getProduct(int ID);
        Product[] getProducts();

        void refreshDataGrid(DataGridView dataGridView);
    }
}
