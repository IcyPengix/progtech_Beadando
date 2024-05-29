using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekstore_Nyilvantartasi_Rendszere
{
    public class Product
    {
        public Product(int iD, string name, int quantity, int price)
        {
            ID = iD;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public int ID;
        public string Name;
        public int Quantity;
        public int Price;
    }
}
