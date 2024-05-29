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
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_geekstore.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string login = "SELECT * FROM tbl_users WHERE username= '" + textBUser.Text + "' and password= '" + textBPass.Text + "' ";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                new formMain().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó", "Sikertelen belépés", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBUser.Text = "";
                textBPass.Text = "";
                textBUser.Focus();
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBUser.Text = "";
            textBPass.Text = "";
            textBUser.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBPass.PasswordChar = '\0';
            }
            else
            {
                textBPass.PasswordChar = '*';
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new formReg().Show();
            this.Hide();
        }
    }
}
