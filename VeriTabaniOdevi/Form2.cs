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
       
        public Form2()
        {
            List<string> Db_read_list = new List<string>();
            
            
            InitializeComponent();

            Connection_user connect_to_db = new Connection_user();
            connection = new MySqlConnection(connect_to_db.Connect_to_DB());

            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            MySqlCommand cmd = new MySqlCommand(connect_to_db.Return_ALL_University_name_and_dep_NN_Query(), connection);
            MySqlDataReader dbr;

            dbr = cmd.ExecuteReader();
            while (dbr.Read())
            {
                Db_read_list.Add(dbr[0].ToString());
            }

            string[] string_list = new string[Db_read_list.Count];

            string_list = Db_read_list.ToArray();

            comboBox1.Items.AddRange(string_list);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connection_user connect_to_db = new Connection_user();
            connection = new MySqlConnection(connect_to_db.Connect_to_DB());

            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


            MySqlCommand cmd = new MySqlCommand(connect_to_db.Return_UserName_Query(textBox1.Text), connection);
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

                if (textBox1.Text == "" || textBox2.Text == "")
                    MessageBox.Show("* This areas cannot be empty");
                else
                {
                    cmd = new MySqlCommand(connect_to_db.Insert_UserName_and_Password_Query(textBox1.Text, textBox2.Text), connection);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
