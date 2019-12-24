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
        List<worknames> worknameslist = new List<worknames>();

        public int userid;
        cvlist cv_list = new cvlist();
        string globalpastwork;
        
        public CVFORM()
        {
            InitializeComponent();
            connection = Mysql_Connect(connection);
            WorkArea_Poz_List_to_Combo_box();
            Work_Names_to_Combo_box();
        }

        private void CVFORM_Load(object sender, EventArgs e)
        {
            LoadCVFORM();
            UpdateCVForm();
        }

        private void ADD_PAST_WORK_BUTTON_Click(object sender, EventArgs e)
        {
            bool ekleyebilirFlag = true;
            if (comboBox4.Text == "" || comboBox5.Text == "" || comboBox6.Text == "")
            {
                MessageBox.Show("Seçili Firma/Alan/Pozisyon Boş olamaz");
            }
            else
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Text == comboBox4.Text)
                        if (listView2.Items[i].Text == comboBox5.Text)
                            if (listView3.Items[i].Text == comboBox6.Text)
                            {

                                ekleyebilirFlag = false;
                                break;
                            }
                }

                if (ekleyebilirFlag)
                {
                    listView1.Items.Add(comboBox4.Text);
                    listView2.Items.Add(comboBox5.Text);
                    listView3.Items.Add(comboBox6.Text);
                    cv_list.past_work_area_pos.Add("@" + comboBox4.Text + "/" + comboBox5.Text + "/" + comboBox6.Text + "&");
                }
                else
                    MessageBox.Show("Aynı değerler tekrar eklenemez!");

            }
        }

        private void SAVE_BUTTON_Click(object sender, EventArgs e)
        {
            int count = 0;

            UpdateCVForm();


            cv_list.website = textBox5.Text;

            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_CV_ID_Query(), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();

            while (dbr.Read())
            {
                if ((int)dbr[0] == userid)
                    count++;
            }

            dbr.Close();

            if (count == 1)
            {
                cmd = new MySqlCommand(connect_to_DB.Delete_FROM_CV_Query(userid), connection);
                cmd.ExecuteNonQuery();
                CVForm_to_DB();
            }

            else
                CVForm_to_DB();
            MessageBox.Show("CV KAYDEDILDI");
        }

        void LoadCVFORM()
        {
            string website = "";
            string allwork = "";
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_ALL_CV_Query(), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();
            while (dbr.Read())
            {
                if (userid == (int)dbr[0])
                {
                    allwork = dbr[9].ToString();
                    website = dbr[5].ToString();
                }
            }
            dbr.Close();
            while (allwork.Length != 0)
            {
                string work = allwork.Split('@', '&')[1];
                listView1.Items.Add(work.Split('/')[0]);
                listView2.Items.Add(work.Split('/')[1]);
                listView3.Items.Add(work.Split('/')[2]);
                allwork = allwork.Remove(0, work.Length + 2);
            }
            textBox5.Text = website;
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
            dbr.Close();
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

        void Update_Work_Names()
        {
            Connection_user connect_to_DB = new Connection_user();
            connection = Mysql_Connect(connection);

            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Return_Work_Name_Query(), connection);
            MySqlDataReader dbr = cmd.ExecuteReader();
            while (dbr.Read())
            {
                worknameslist.Add(new worknames { work_Name = dbr[0].ToString() });
            }
            dbr.Close();
        }

        void Work_Names_to_Combo_box()
        {
            Update_Work_Names();
            foreach (var item in worknameslist)
            {
                comboBox4.Items.Add(item.work_Name);
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

        void CVForm_to_DB()
        {

            Update_past_work_area_pos();
            Add_PastWorkTo_string();
            MySqlCommand cmd = new MySqlCommand(connect_to_DB.Insert_CV_Query(userid, cv_list.name, cv_list.surname, cv_list.phone_no, cv_list.email, cv_list.website, cv_list.work
                , cv_list.work_area, cv_list.work_pos, globalpastwork), connection);
            cmd.ExecuteNonQuery();
        }

        void Update_past_work_area_pos()
        {
            cv_list.past_work_area_pos.Clear();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                cv_list.past_work_area_pos.Add("@" + listView1.Items[i].Text + "/" + listView2.Items[i].Text + "/" + listView3.Items[i].Text + "&");
            }
        }

        void Add_PastWorkTo_string()
        {
            globalpastwork = "";
            for (int i = 0; i < cv_list.past_work_area_pos.Count; i++)
            {
                globalpastwork += cv_list.past_work_area_pos[i];
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = 0;

            if (listView1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.SelectedItems[0] != listView1.Items[i])
                    {
                        count++;
                    }
                    else
                        break;
                }
                listView2.Items[count].Selected = true;
                listView3.Items[count].Selected = true;
                count = 0;
                
            }
        }

        private void Delete_Work_Button_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                MessageBox.Show("No item selected");

            else
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                listView2.Items.Remove(listView2.SelectedItems[0]);
                listView3.Items.Remove(listView3.SelectedItems[0]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Kaydetmeden Çıkmak ? ", "Dikkat", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}