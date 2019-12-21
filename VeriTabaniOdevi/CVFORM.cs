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

        List<WorkArea> workarealist = new List<WorkArea>();
        List<WorkPoz> workpozlist = new List<WorkPoz>();
        public int userid;
        cvlist cv_list = new cvlist();

        public CVFORM()
        {
            InitializeComponent();
            connection = Mysql_Connect(connection);
            WorkArea_Poz_List_to_Combo_box();
        }

        private void CVFORM_Load(object sender, EventArgs e)
        {
            UpdateCVForm();
        }

        void UpdateCVForm()
        {
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_All_Mezun_Query(), connection);
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

            textBox1.Text = cv_list.name;
            textBox2.Text = cv_list.surname;
            textBox3.Text = cv_list.phone_no;
            textBox4.Text = cv_list.email;
            comboBox1.Text = cv_list.work_area;
            comboBox2.Text = cv_list.work_pos;
            comboBox3.Text = cv_list.work;
            List<string> temp=new List<string>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                temp.Add(listView1.Items[i].ToString());
            }
        }
        
        void WorkArea_Poz_List_to_Combo_box()
        {
            UpdateWorkArea_Poz_List();
            foreach (var item in workarealist)
            {
                comboBox5.Items.Add(item.work_Area);
            }
            foreach (var item in workpozlist)
            {
                comboBox6.Items.Add(item.work_Poz);
            }
        }

        void UpdateWorkArea_Poz_List()
        {
            workarealist.Clear();
            workpozlist.Clear();
            Connection_user connect_to_DB = new Connection_user();
            connection = Mysql_Connect(connection);

            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_All_WorkArea_Query(), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();

            while (dbr.Read())
            {
                workarealist.Add(new WorkArea { work_Area = dbr[0].ToString() });
            }
            dbr.Close();

            cmd = new MySqlCommand(connect_to_DB.Return_All_WorkPoz_Query(), connection);
            dbr = cmd.ExecuteReader();

            while (dbr.Read())
            {
                workpozlist.Add(new WorkPoz { work_Poz = dbr[0].ToString() });
            }
            dbr.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateCVForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorkArea_Poz_List_to_Combo_box();
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
        public List<string> past_work;
        public List<string> past_work_area;
        public List<string> past_work_pos;
    }
}
