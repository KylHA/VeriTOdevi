﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriTabaniOdevi
{
    public partial class MainFrame : Form
    {
        public string name, surname;
        public string id;
        
        public MainFrame()
        {
            InitializeComponent();
            UpdateMainFrame();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            label1.Text = name;
            
            label4.Text = id.ToString();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Form1 obj = (Form1)Application.OpenForms["Form1"];
            obj.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProfileEdit_Form peform = new ProfileEdit_Form();
            peform.id = id;
            peform.Show();
        }
        void UpdateMainFrame()
        {
            int coor_y = 450;

            int idCount = 2;
            const int size = 1;

            RichTextBox[] richTextBox = new RichTextBox[size];

            for (int i = 0; i < size; i++)
            {
                richTextBox[i] = new System.Windows.Forms.RichTextBox();
                richTextBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                richTextBox[i].Location = new System.Drawing.Point(600, coor_y);
                richTextBox[i].Name = "richTextBox" + idCount;
                richTextBox[i].ReadOnly = true;
                richTextBox[i].WordWrap = false;
                richTextBox[i].ScrollBars = RichTextBoxScrollBars.None;
                richTextBox[i].Size = new System.Drawing.Size(650, 100);
                richTextBox[i].TabIndex = 3;
                richTextBox[i].Enabled = false;
                Controls.Add(richTextBox[i]);
                coor_y += 120;
                idCount++;
            }
        }

        private void Exit_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            CVFORM cvform = new CVFORM();
            cvform.userid = int.Parse(id);
            cvform.Show();
        }
    }
}
