using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace VeriTabaniOdevi
{
    public partial class ADMIN_Form : Form
    {

        private MySqlConnection connection;

        List<WorkArea> workarealist = new List<WorkArea>();
        List<WorkPoz> workpozlist = new List<WorkPoz>();

        public ADMIN_Form()
        {
            InitializeComponent();
            AddWorkArea_Pos_Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //webScrapeForDATA();
            executeSQLcommend_Insert_University();

        }
        void executeSQLcommend_Insert_University()
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
            MySqlCommand cmd;

            string University_fileName = @"C:\Users\KylHA\Documents\GitHub\VeriTOdevi\VeriTabaniOdevi\Üniversiteler.txt";
            string bolumler_fileName = @"C:\Users\KylHA\Documents\GitHub\VeriTOdevi\VeriTabaniOdevi\bolumler_edited.txt";
            string[] University_file = File.ReadAllLines(University_fileName);
            string[] Dep_file = File.ReadAllLines(bolumler_fileName);
            int k = 0;
            foreach (string item in University_file)
            {
                University_file[k] = item.Split('\t')[0];
                k++;
            }

            for (int i = 0; i < Dep_file.Length; i++)
            {
                if (i < University_file.Length)
                    cmd = new MySqlCommand(connect_to_db.Insert_University_name_and_dep_NN_Query(University_file[i], Dep_file[i]), connection);
                else
                    cmd = new MySqlCommand(connect_to_db.Insert_University_name_and_dep_NN_Query(null, Dep_file[i]), connection);

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
        void webScrapeForDATA()
        {
            string fileName = @"C:\Users\KylHA\Documents\GitHub\VeriTOdevi\VeriTabaniOdevi\bolumler_edited.txt";
            string[] separatingStrings = { "<", ">" };
            int i = 0;
            string[] University_file = File.ReadAllLines(@"C:\Users\KylHA\Documents\GitHub\VeriTOdevi\VeriTabaniOdevi\Üniversiteler.txt");
            string[] University_liste = new string[University_file.Length];

            foreach (string item in University_file)
            {
                University_liste[i] = item.Split('\t')[0];
                i++;
            }
            i = 0;
            string[] Dep_file = File.ReadAllLines(@"C:\Users\KylHA\Documents\GitHub\VeriTOdevi\VeriTabaniOdevi\bolumler.txt");
            string[] Dep_liste = new string[Dep_file.Length];

            foreach (var item in Dep_file)
            {
                Dep_liste[i] = item.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries)[0];
                i++;
            }
            File.WriteAllLines(@"C:\Users\KylHA\Documents\GitHub\VeriTOdevi\VeriTabaniOdevi\bolumler_edited.txt", Dep_liste);
            i = 0;
            var lines = File.ReadAllLines(fileName).Where(arg => !string.IsNullOrWhiteSpace(arg));
            File.WriteAllLines(fileName, lines);
            string[] separatingStrings_2 = { "                    " };
            string[] trim = File.ReadAllLines(fileName);

            foreach (var item in trim)
            {
                trim[i] = item.Split(separatingStrings_2, StringSplitOptions.RemoveEmptyEntries)[0];
                i++;
            }
            File.WriteAllLines(fileName, trim);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connection_user connect_to_db = new Connection_user();
            connection = Mysql_Connect(connection);

            MySqlCommand cmd = new MySqlCommand(connect_to_db.Delete_ALL_Table_Values("mezun_universite"), connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connection_user connect_to_db = new Connection_user();
            connection = Mysql_Connect(connection);
            MySqlCommand cmd = new MySqlCommand(connect_to_db.Delete_ALL_Table_Values("mezun"), connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void Delete_Login_Click(object sender, EventArgs e)
        {
            Connection_user connect_to_db = new Connection_user();
            connection = Mysql_Connect(connection);
            MySqlCommand cmd = new MySqlCommand(connect_to_db.Delete_ALL_Table_Values("login"), connection);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand(connect_to_db.Reset_ALL_Table_Values(), connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Connection_user connect_to_db = new Connection_user();
            connection = Mysql_Connect(connection);
            MySqlCommand cmd = new MySqlCommand(connect_to_db.Create_Mezun_Table_Query(), connection);
            cmd.ExecuteNonQuery();
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

        private void button4_Click(object sender, EventArgs e)
        {
            Connection_user connect_to_db = new Connection_user();
            connection = Mysql_Connect(connection);
            MySqlCommand cmd = new MySqlCommand(connect_to_db.Create_login_Table_Query(), connection);
            cmd.ExecuteNonQuery();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddWorkArea_Pos_Update();
        }

        void AddWorkArea_Pos_Update()
        {
            Connection_user connect_to_DB = new Connection_user();
            connection = Mysql_Connect(connection);
            MySqlCommand cmd;
            listView1.Clear();
            listView2.Clear();

            if (textBox1.Text != "")
            {
                cmd = new MySqlCommand(connect_to_DB.Insert_Work_Area_Query(textBox1.Text), connection);
                cmd.ExecuteNonQuery();
            }

            if (textBox2.Text != "")
            {
                cmd = new MySqlCommand(connect_to_DB.Insert_Work_Pos_Query(textBox2.Text), connection);
                cmd.ExecuteNonQuery();
            }

            UpdateWorkArea_Poz_List();

            foreach (var item in workarealist)
            {
                listView1.Items.Add(item.work_Area);
            }

            foreach (var item in workpozlist)
            {
                listView2.Items.Add(item.work_Poz);
            }

            textBox1.Text = "";
            textBox2.Text = "";
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
    }
    public class WorkArea
    {
        public string work_Area { get; set; }
        //public string work_Poz { get; set; }
    }
}