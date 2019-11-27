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
            database = "veritabanidb";
            uid = "root";
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
            String query = "select * from login where user_name = '" + textBox1.Text + "'and user_password = '" + this.textBox2.Text + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dbr;

            dbr = cmd.ExecuteReader();

            int count = 0;
            while (dbr.Read())
            {
                count = count + 1;
            }
            if (count == 1)
            {
                //this.Visible = false;
                //Form2 f2 = new Form2(); //this is the change, code for redirect  
                //f2.ShowDialog();

                MessageBox.Show("Succesfully connected rerouting to app form");
            }
            else if (count > 1)
            {
                MessageBox.Show("Duplicate username and password", "login page");
            }
            else { MessageBox.Show("Username or password dont match"); }
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