﻿using System;
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
                count = count + 1;
            
            if (count == 1)
                MessageBox.Show("Succesfully connected rerouting to app form");
            

            else { MessageBox.Show("Username or password dont match"); }

            if (UserName_BOX.Text == "" && Password_BOX.Text == "")
            {
                ADMIN_Form admin_form = new ADMIN_Form();
                this.Visible = false;
                admin_form.ShowDialog();
            }
            //string[] file = File.ReadAllLines(@"C:\Users\KylHA\Documents\GitHub\VeriTOdevi\VeriTabaniOdevi\Üniversiteler.txt");
            //string output_1 = file[0].Split('\t')[0];
            
            //string output_2 = file[0].Split('\t')[1];
            //MessageBox.Show("first char is :" + output_1+ " / "+output_2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}