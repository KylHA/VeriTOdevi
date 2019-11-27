using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VeriTabaniOdevi
{
    public partial class Form2 : Form
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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

            String query = "select * from login where user_name = '"+textBox1.Text+"'";
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
                MessageBox.Show("This user name already exists");
                dbr.Close();
            }
            else
            {
                dbr.Close();
                query = "INSERT INTO login(user_name, user_password) VALUES("+"'"+textBox1.Text+"'"+","+textBox2.Text+")" ;
                cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
