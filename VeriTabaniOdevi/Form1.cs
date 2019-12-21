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
using System.IO;

namespace VeriTabaniOdevi
{
    //Use (ctrl+k) + (ctrl+f) to fix formmatting
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        List<user_login_list> user_list = new List<user_login_list>();
        MainFrame mainFrame = new MainFrame();
        
        public Form1()
        {
            InitializeComponent();

            Update_user_login_list();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;

            foreach (var item in user_list)
            {
                if (UserName_BOX.Text == item.user_Name && Password_BOX.Text == item.user_Password)
                {
                    
                    this.Hide();
                    flag = true;

                    mainFrame.id = item.user_id;
                    mainFrame.name = item.user_Name;
                    mainFrame.surname = item.user_Password;

                    mainFrame.Show();
                    break;
                } 
            }

            if (!flag)
            {
                MessageBox.Show("User name or password don't match");
            }

            if (UserName_BOX.Text == "" && Password_BOX.Text == "")
            {
                ADMIN_Form admin_form = new ADMIN_Form();

                admin_form.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
        MySqlConnection Mysql_Connect(MySqlConnection connect)
        {
            Connection_user connect_to_db = new Connection_user();
            connect = new MySqlConnection(connect_to_db.Connect_to_DB());
            try
            {
                connect.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return connect;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Update_user_login_list();
        }

        public void Update_user_login_list()
        {
            user_list.Clear();
            Connection_user connect_to_DB = new Connection_user();
            connection = Mysql_Connect(connection);
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_All_login_Query(), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();

            while (dbr.Read())
            {
                user_list.Add(new user_login_list { user_id = dbr[0].ToString(), user_Name = dbr[1].ToString(), user_Password = dbr[2].ToString() });
            }
            dbr.Close();
        }
    }

    public class user_login_list
    {
        public string user_id { get; set; }
        public string user_Name { get; set; }
        public string user_Password { get; set; }
    }

}

//string[] file = File.ReadAllLines(@"C:\Users\KylHA\Documents\GitHub\VeriTOdevi\VeriTabaniOdevi\Üniversiteler.txt");
//string output_1 = file[0].Split('\t')[0];

//string output_2 = file[0].Split('\t')[1];
//MessageBox.Show("first char is :" + output_1+ " / "+output_2);