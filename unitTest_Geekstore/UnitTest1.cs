using geekstore_Nyilvantartasi_Rendszere;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;

namespace unitTest_Geekstore
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd()
        {
            IDatabase db = new accessDatabase();
            string name = "teszt";
            int quantity = 20, price = 1000;
            db.addNewProduct(name, quantity, price);
            Product[] products = db.getProducts();
            bool benneVan = false;
            foreach (Product product in products)
            {
                if (product.Name == name && product.Quantity == quantity && product.Price == price)
                {
                    benneVan = true;
                    break;
                }
            }
            Assert.IsTrue(benneVan);
        }

        [TestMethod]
        public void TestUpdate()
        {
            IDatabase db = new accessDatabase();
            db.addNewProduct("teszt", 20, 2500);
            Product[] products = db.getProducts();
            Product p = null;
            bool benneVan = false;
            foreach (Product product in products)
            {
                if (product.Name == "teszt")
                {
                    p = product;
                    break;
                }
            }
            Assert.IsNotNull(p);
            db.updateProduct(p.ID, "teszt1", 10, 1500);
            products = db.getProducts();
            foreach (Product product in products)
            {
                if (product.Name == "teszt1" && product.Quantity == 10 && product.Price == 1500 && product.ID == p.ID)
                {
                    benneVan = true;
                    break;
                }
            }
            Assert.IsTrue(benneVan);
        }

        [TestMethod]
        public void TestDelete()
        {
            IDatabase db = new accessDatabase();
            db.addNewProduct("teszt", 20, 2500);
            Product[] products = db.getProducts();
            Product p = null;
            bool benneVan = false;
            foreach (Product product in products)
            {
                if (product.Name == "teszt")
                {
                    p = product;
                    break;
                }
            }
            db.deleteProduct(p.ID);
            products = db.getProducts();
            foreach (Product product in products)
            {
                if (product.Name == "teszt" && product.Quantity == 20 && product.Price == 2500 && product.ID == p.ID)
                {
                    benneVan = true;
                    break;
                }
            }
            Assert.IsFalse(benneVan);
        }

        [TestMethod]
        public void TestJoBejelentkezes()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_geekstore.mdb");
            OleDbCommand cmd = new OleDbCommand();

            con.Open();
            string login = "SELECT * FROM tbl_users WHERE username= 'user' and password= 'user123' ";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            Assert.IsTrue(dr.Read());
            con.Close();
        }

        [TestMethod]
        public void TestHibasBejelentkezes()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_geekstore.mdb");
            OleDbCommand cmd = new OleDbCommand();

            con.Open();
            string login = "SELECT * FROM tbl_users WHERE username= 'user123' and password= 'user' ";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            Assert.IsFalse(dr.Read());
            con.Close();
        }

        [TestMethod]

        public void TestRegisztracioLetrehozas()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_geekstore.mdb");
            OleDbCommand cmd = new OleDbCommand();
            string name = "testuser", password = "testuserpass";
            con.Open();
            string register = "INSERT INTO tbl_users VALUES('" + name + "','" + password + "' , FALSE)";
            cmd = new OleDbCommand(register, con);
            cmd.ExecuteNonQuery();

            string login = $"SELECT * FROM tbl_users WHERE username= '{name}' and password= '{password}' ";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            Assert.IsTrue(dr.Read());

            string delete = $"DELETE FROM tbl_users WHERE username= '{name}' and password= '{password}' ";
            con.Close();
        }
    }
}
