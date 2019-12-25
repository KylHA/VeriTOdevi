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
    public partial class ProfileEdit_Form : Form
    {
        private MySqlConnection connection;
        Connection_user connect_to_DB = new Connection_user();

        List<WorkArea> workarealist = new List<WorkArea>();
        List<WorkPoz> workpozlist = new List<WorkPoz>();
        List<worknames> worknameslist = new List<worknames>();

        public string id;


        public ProfileEdit_Form()
        {
            InitializeComponent();

            connection = Mysql_Connect(connection);
        }

        private void ProfileEdit_Form_Load(object sender, EventArgs e)
        {
            FillProfile();
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Update_Mezun_Query(id,textBox1.Text,textBox2.Text,textBox3.Text,textBox5.Text,textBox6.Text,textBox4.Text,
                textBox13.Text,comboBox1.Text,comboBox2.Text,comboBox3.Text,textBox10.Text,textBox11.Text), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();
            dbr.Close();
           
            MessageBox.Show("Profil Güncellendi");
        }

        void FillProfile()
        {
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_All_Mezun_Query(), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();
            while (dbr.Read()) 
            {
                if (dbr[0].ToString() == id) 
                {
                    textBox1.Text = dbr[1].ToString();
                    textBox2.Text = dbr[2].ToString();
                    textBox3.Text = dbr[3].ToString();
                    textBox4.Text = dbr[7].ToString();
                    textBox5.Text = dbr[5].ToString();
                    textBox6.Text = dbr[6].ToString();
                    textBox10.Text = dbr[12].ToString();
                    textBox11.Text = dbr[13].ToString();
                    textBox13.Text = dbr[8].ToString();
                    comboBox1.Text= dbr[9].ToString();
                    comboBox2.Text= dbr[11].ToString();
                    comboBox3.Text= dbr[10].ToString();
                }
            }
            dbr.Close();

            Liststo_ComboBOX();
        }

        void Update_Work_Names()
        {
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_Work_Name_Query(), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();
            while (dbr.Read())
            {
                worknameslist.Add(new worknames { work_Name = dbr[0].ToString() });
            }
            dbr.Close();
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

        void Liststo_ComboBOX() 
        {
            Update_Work_Names();
            UpdateWorkArea_Poz_List();

            foreach (var item in worknameslist)
            {
                comboBox1.Items.Add(item.work_Name);
            }
            foreach (var item in workarealist)
            {
                comboBox2.Items.Add(item.work_Area);
            }
            foreach (var item in workpozlist)
            {
                comboBox3.Items.Add(item.work_Poz);
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

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Kaydetmeden Çıkmak ? ", "Dikkat", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
