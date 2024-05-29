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
    public partial class formReg : Form
    {
        public formReg()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_geekstore.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBUser.Text == "" && textBPass.Text == "" && textBPassAgain.Text == "")
            {
                MessageBox.Show("Nem adott meg felhasználónevet és jelszavat", "Regisztráció sikertelen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBPass.Text == textBPassAgain.Text)
            {
                con.Open();
                string register = "INSERT INTO tbl_users VALUES('" + textBUser.Text + "','" + textBPass.Text + "' , FALSE)";
                cmd = new OleDbCommand(register, con);
                cmd.ExecuteNonQuery();
                con.Close();

                textBUser.Text = "";
                textBPass.Text = "";
                textBPassAgain.Text = "";

                MessageBox.Show("Sikeresen létrehozta a fiókját", "Regisztráció sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Nem egyeznek a jelszavak, kérem próbálja meg újra", "Regisztráció sikertelen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBPass.Text = "";
                textBPassAgain.Text = "";
                textBPass.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBPass.PasswordChar = '\0';
                textBPassAgain.PasswordChar = '\0';
            }
            else
            {
                textBPass.PasswordChar = '*';
                textBPassAgain.PasswordChar = '*';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBUser.Text = "";
            textBPass.Text = "";
            textBPassAgain.Text = "";
            textBUser.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new formLogin().Show();
            this.Hide();
        }
    }
}
