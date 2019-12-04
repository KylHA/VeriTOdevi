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
        public string Insert_University_name_and_dep_NN_Query(string uni_name, string dep_name)
        {
            string query = "INSERT INTO mezun_universite(Universite_Name,Universite_Bolum) VALUES(" + "'" + uni_name + "'" + "," + "'" + dep_name + "'" + ")";
            return query;
        }
        public string Delete_ALL_Table_Values(string table_name)
        {
            string query = "Delete from " + table_name;
            return query;
        }
        public string Insert_Mezun_Query(int u_id, string name, string surname, string id, string date, string email, string okul, string dep, string phone_no, string w_firma, string w_pos, string w_area, string lang, string sertifikalar)
        {
            string query = "INSERT INTO mezun(user_id,name, surname, id_no, Date_of_birth,email,mezunOkul, mezunDep, phone_number, working_firma, working_pos, working_area, languages, sertifika)" +
                " VALUES(" + u_id + "," + "'" + name + "'" + "," + "'" + surname + "'" + "," + "'" + id + "'" + "," + "'" + date + "'" + "," + "'" + email + "'" + "," + "'" + okul + "'" + "," + "'" + dep + "'" + "," + "'" + phone_no + "'" + "," + "'" + w_firma + "'" + "," + "'" +
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
    }
}