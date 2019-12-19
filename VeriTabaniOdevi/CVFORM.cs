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
using System.IO;

namespace VeriTabaniOdevi
{
    public partial class CVFORM : Form
    {
        private MySqlConnection connection;
        Connection_user connect_to_DB = new Connection_user();

        public int userid;
        cvlist cv_list = new cvlist();
        
        public CVFORM()
        {
            InitializeComponent();
            connection = Mysql_Connect(connection);
        }

        private void CVFORM_Load(object sender, EventArgs e)
        {

        }

        void UpdateCVForm()
        {
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_All_login_Query(), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();

            while (dbr.Read())
            {
                if ((int)dbr[0] == userid) 
                {
                    cv_list.name = dbr[1].ToString();
                    cv_list.surname = dbr[2].ToString();
                    cv_list.phone_no = dbr[8].ToString();
                    cv_list.email = dbr[5].ToString();
                    cv_list.work = dbr[9].ToString();
                    cv_list.work_area = dbr[11].ToString();
                    cv_list.work_pos = dbr[10].ToString();
                    break;
                }
            }
            dbr.Close();
            foreach
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
    public class cvlist
    {
        public string name;
        public string surname;
        public string phone_no;
        public string email;
        public string website;
        public string work;
        public string work_area;
        public string work_pos;
        public string[] past_work;
        public string[] past_work_area;
        public string[] past_work_pos;
    }
}
