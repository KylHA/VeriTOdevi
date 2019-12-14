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
        Form1 login_form = new Form1();
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
            Connection_user connect_to_db = new Connection_user();

            connection = Mysql_Connect(connection);

            MySqlCommand cmd = new MySqlCommand(connect_to_db.Return_UserName_Query(U_Name.Text), connection);
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

                if (U_Name.Text == "" || Pass.Text == "")
                    MessageBox.Show("* This areas cannot be empty");
                else
                {
                    string u_id = "";
                    cmd = new MySqlCommand(connect_to_db.Insert_UserName_and_Password_Query(U_Name.Text, Pass.Text), connection);
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand(connect_to_db.Return_UserID_Query(U_Name.Text), connection);
                    MySqlDataReader new_dbr;
                    new_dbr = cmd.ExecuteReader();
                    while (new_dbr.Read())
                    {
                        u_id = new_dbr[0].ToString();
                    }
                    new_dbr.Close();
                    int id = Int32.Parse(u_id);
                    cmd = new MySqlCommand(connect_to_db.Insert_Mezun_Query(id, NAME.Text, SURNAME.Text, Id_Box.Text, Date_P.Value.ToString(), Email_Box.Text, Okul_box.Text,
                        Dep_Box.Text, Phone_Box.Text, Firma_Box.Text, Pos_Box.Text, Area_Box.Text, Lang_Box.Text, Cert_Box.Text), connection);
                    cmd.ExecuteNonQuery();
                    login_form.Update_user_login_list();
                    connection.Close();
                }
            }
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
    }
}
//List<string> Db_read_list = new List<string>();
//Connection_user connect_to_db = new Connection_user();
//connection = new MySqlConnection(connect_to_db.Connect_to_DB());

//try
//{
//    connection.Open();
//}
//catch (MySqlException ex)
//{
//    MessageBox.Show(ex.Message);
//}
//MySqlCommand cmd = new MySqlCommand(connect_to_db.Return_ALL_University_name_and_dep_NN_Query(), connection);
//MySqlDataReader dbr;

//dbr = cmd.ExecuteReader();
//while (dbr.Read())
//{
//    Db_read_list.Add(dbr[0].ToString());
//}

//string[] string_list = new string[Db_read_list.Count];

//string_list = Db_read_list.ToArray();

//comboBox1.Items.AddRange(string_list);