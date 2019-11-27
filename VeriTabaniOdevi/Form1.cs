using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace VeriTabaniOdevi
{
    
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public bool Flag = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            server = "localhost";
            database = "MySQL80";
            uid = "testuser";
            password = "Nanomek23271973";
            string connectionString;
            connectionString = "Server=" + server + ";" + "Database=" +
            database + ";" + "Uid=" + uid + ";" + "Pwd=" + password + ";";

            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            String query = "select * from data where username = '" + textBox1.Text + "'and password = '" + this.textBox2.Text + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dbr;
            
            //dbr = cmd.ExecuteReader();
            
            //int count = 0;
            //while (dbr.Read())
            //{
            //    count = count + 1;
            //}
            //if (count == 1)
            //{
            //    this.Visible = false;
            //    Form2 f2 = new Form2(); //this is the change, code for redirect  
            //    f2.ShowDialog();
            //}
            //else if (count > 1)
            //{
            //    MessageBox.Show("Duplicate username and password", "login page");
            //}
            //else
            //{
            //    MessageBox.Show(" username and password incorrect", "login page");
            //}
            //try
            //{
            //    if (!(textBox1.Text == string.Empty))
            //    {
            //        if (!(textBox2.Text == string.Empty))
            //        {
            //            String str = "Server=localhost;database=MySQL80;UID=root;password=Nanomek23271973";//Connect Hatası Veriyor Bakılacak
            //            String query = "select * from data where username = '" + textBox1.Text + "'and password = '" + this.textBox2.Text + "'";
            //            SqlConnection con = new SqlConnection(str);
            //            SqlCommand cmd = new SqlCommand(query, con);
            //            SqlDataReader dbr;
            //            con.Open();
            //            dbr = cmd.ExecuteReader();
            //            int count = 0;
            //            while (dbr.Read())
            //            {
            //                count = count + 1;
            //            }
            //            if (count == 1)
            //            {
            //                this.Visible = false;
            //                Form2 f2 = new Form2(); //this is the change, code for redirect  
            //                f2.ShowDialog();
            //            }
            //            else if (count > 1)
            //            {
            //                MessageBox.Show("Duplicate username and password", "login page");
            //            }
            //            else
            //            {
            //                MessageBox.Show(" username and password incorrect", "login page");
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show(" password empty", "login page");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(" username empty", "login page");
            //    }
            //    con.Close();
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
        public void ShowForm()
        { this.Visible = true; }
    }
}