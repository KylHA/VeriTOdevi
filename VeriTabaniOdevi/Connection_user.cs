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
            String query = "select * from login where user_name = '" + name + "'and user_password = '" + password + "'";
            return query;
        }
        public string Return_UserName_Query(string name)
        {
            String query = "select * from login where user_name = '" + name + "'"; 
            return query;
        }
        public string Insert_UserName_and_Password_Query(string name,string password)
        {
            String query = "INSERT INTO login(user_name, user_password) VALUES(" + "'" + name + "'" + "," + password+ ")";
            return query;
        }

        public string Return_ALL_University_name_and_dep_NN_Query()
        {
            String query = "select University_name_and_dep from okul where University_name_and_dep is not null";
            return query;
        }
    }
}