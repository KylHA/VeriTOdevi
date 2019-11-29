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
        private MySqlConnection mysqlconnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection_user connect_to_DB = new Connection_user();
            
            mysqlconnection =new MySqlConnection(connect_to_DB.Connect_to_DB());

            try
            {
                mysqlconnection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_UserName_and_Password_Query(UserName_BOX.Text, Password_BOX.Text), mysqlconnection);

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