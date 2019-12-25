using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VeriTabaniOdevi
{
    class Connection_user
    {
        private string server;
        private string database;
        private string uid;
        private string password;

        public string Connect_to_DB()
        {
            server = "localhost";
            database = "veritabanidb";
            uid = "root";
            password = "Nanomek23271973";
            string connectionString;
            return connectionString = "Server=" + server + ";" + "Database=" +
            database + ";" + "Uid=" + uid + ";" + "Pwd=" + password + ";";
        }

        public string Return_UserName_and_Password_Query(string name, string password)
        {
            string query = "select * from login where user_name = '" + name + "'and user_password = '" + password + "'";
            return query;
        }

        public string Return_All_login_Query()
        {
            string query = "select * from login";
            return query;
        }

        public string Return_All_WorkArea_Query()
        {
            string query = "select Work_Area from workareaandpoz where Work_Area is not null";
            return query;
        }

        public string Return_All_WorkPoz_Query()
        {
            string query = "select Work_Pos from workareaandpoz where Work_Pos is not null";
            return query;
        }

        public string Insert_Work_Pos_Query(string pos)
        {
            string query = "Insert into workareaandpoz(Work_Pos) Values(" + "'"+ pos+"'"+")";
            return query;
        }

        public string Insert_Work_Area_Query(string area)
        {
            string query = "Insert into workareaandpoz(Work_Area) Values(" + "'" + area + "'" + ")";
            return query;
        }

        public string Return_UserName_Query(string name)
        {
            string query = "select * from login where user_name = '" + name + "'";
            return query;
        }

        public string Insert_UserName_and_Password_Query(string name, string password)
        {
            string query = "INSERT INTO login(user_name, user_password) VALUES(" + "'" + name + "'" + "," + "'" + password + "'" + ")";
            return query;
        }

        public string Return_ALL_University_name_and_dep_NN_Query()
        {
            string query = "select University_name_and_dep from okul where University_name_and_dep is not null";
            return query;
        }

        public string Return_ALL_CV_Query()
        {
            string query = "select * from cv";
            return query;
        }

        public string Return_CV_ID_Query()
        {
            string query = "select u_id from cv";
            return query;
        }

        public string Insert_University_name_and_dep_NN_Query(string uni_name, string dep_name)
        {
            string query = "INSERT INTO mezun_universite(Universite_Name,Universite_Bolum) VALUES(" + "'" + uni_name + "'" + "," + "'" + dep_name + "'" + ")";
            return query;
        }

        public string Insert_CV_Query(int uid,string name, string surname,string phone_no,string email,string websitesi,string work, string workarea, string workpos,string work_area_pos)
        {
            string query = "INSERT INTO cv(u_id,name,surname,telefon,email,websitesi,sirket,workarea,workpos,work_area_pos) VALUES ("+uid+","+"'"+name +"'"+","+"'" + surname + "'" + "," + "'" + phone_no +
                "'" + "," + "'" + email + "'" + "," + "'" + websitesi + "'" + "," +"'"+work+"'" +","+ "'" + workarea + "'" + "," + "'" + workpos + "'" + "," + "'" + work_area_pos + "'"+")";
            return query;
        }

        public string Delete_ALL_Table_Values(string table_name)
        {
            string query = "Delete from " + table_name;
            return query;
        }

        public string Delete_FROM_CV_Query(int uid)
        {
            string query = "Delete from cv where u_id =" +uid;
            return query;
        }

        public string Insert_Mezun_Query(int u_id, string name, string surname, string id, string date, string email, string okul,
            string dep, string phone_no, string w_firma, string w_pos, string w_area, string lang, string sertifikalar)
        {
            string query = "INSERT INTO mezun(user_id,name, surname, id_no, Date_of_birth,email,mezunOkul, mezunDep, phone_number, working_firma, working_pos, working_area, languages, sertifika)" +
                " VALUES(" + u_id + "," + "'" + name + "'" + "," + "'" + surname + "'" + "," + "'" + id + "'" + "," + "'" + date + "'" + "," +
                "'" + email + "'" + "," + "'" + okul + "'" + "," + "'" + dep + "'" + "," + "'" + phone_no + "'" + "," + "'" + w_firma + "'" + "," + "'" +
                w_pos + "'" + "," + "'" + w_area + "'" + "," + "'" + lang + "'" + "," + "'" + sertifikalar + "'" + ")";
            return query;
        }

        public string Reset_ALL_Table_Values()
        {
            string query = "ALTER TABLE login AUTO_INCREMENT = 1; ";
            return query;
        }

        public string Return_UserID_Query(string name)
        {
            string query = "select user_id from login where user_name = '" + name + "'";
            return query;
        }

        public string Return_All_Mezun_Query()
        {
            string query = "SELECT * FROM mezun";
            return query;
        }

        public string Create_Mezun_Table_Query()
        {
            string query = "CREATE TABLE IF NOT EXISTS mezun (user_id int(11) NOT NULL,name varchar(60) NOT NULL,surname varchar(30) NOT NULL,id_no int(11) NOT NULL," +
                "Date_of_birth date DEFAULT NULL,email varchar(100) NOT NULL,mezunOkul varchar(200) NOT NULL,mezunDep varchar(400) NOT NULL," +
                "phone_number varchar(50) DEFAULT NULL,working_firma varchar(100) DEFAULT NULL,working_pos varchar(100) DEFAULT NULL," +
                "working_area varchar(100) DEFAULT NULL,languages varchar(5000) NOT NULL,sertifika varchar(5535) DEFAULT NULL,PRIMARY KEY(id_no)," +
                "KEY user_id (user_id),CONSTRAINT mezun_id_1 FOREIGN KEY(user_id) REFERENCES login (user_id))";
            return query;
        }

        public string Create_login_Table_Query()
        {
            string query = "CREATE TABLE IF NOT EXISTS login (" +
                "user_id int(20) NOT NULL AUTO_INCREMENT," +
                "user_name varchar(256) NOT NULL," +
                "user_password varchar(256) NOT NULL," +
                "PRIMARY KEY(user_id)" +
                ")";
            return query;
        }

        public string Return_Work_Name_Query()
        {
            string query = "SELECT Firma_name FROM firma";
            return query;
        }

        public string Update_Mezun_Query(string u_id, string name, string surname, string id, string email, string okul,
            string dep, string phone_no, string w_firma, string w_pos, string w_area, string lang, string sertifikalar)
        {
            string query = "UPDATE mezun SET name = "+"'"+name+"'"+","+ " surname =" + "'" + surname + "'" + "," + " id_no = " + "'" + id + "'" + "," + " email = " + "'" + email + "'" + "," + "" +
                " mezunOkul = " + "'" + okul + "'" + "," + " mezunDep = " + "'" + dep + "'" + "," + " phone_number = " + "'" + phone_no + "'" + "," + " working_firma = " + "'" + w_firma + "'" + "," +
                " working_area = " + "'" + w_area + "'" + "," + " working_pos = " + "'" + w_pos + "'" + "," + " languages = " + "'" + lang + "'" + "," + " sertifika = " + "'" + sertifikalar + "'"+" WHERE user_id = " + u_id;
            return query;
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
        public List<string> past_work_area_pos = new List<string>();
    }

    public class worknames
    {
        public string work_Name { get; set; }
    }

    public class WorkPoz
    {
        public string work_Poz { get; set; }
    }

    public class user_login_list
    {
        public string user_id { get; set; }
        public string user_Name { get; set; }
        public string user_Password { get; set; }
    }
}