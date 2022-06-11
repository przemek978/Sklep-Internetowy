using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PROJEKT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PROJEKT.Models
{
    public static class DataBase
    {
        public static IConfiguration configuration;
        static string myCompanyDB;

        public static List<Product> Read()
        {
            var productList = new List<Product>();
            Product product;
            myCompanyDB = configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB);
            SqlCommand cmd = new SqlCommand("sp_productList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                product = new Product(Int32.Parse(reader["Id"].ToString()), reader.GetString(1), Decimal.Parse(reader["Price"].ToString()), reader.GetString(3));
                productList.Add(product);
            }
            reader.Close();
            con.Close();
            return productList;
        }
        public static List<TypeUser> ReadTypes()
        {
            var types = new List<TypeUser>();
            TypeUser type;
            myCompanyDB = configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB);
            string sql = "SELECT * FROM TypeUser";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                type = new TypeUser(Int32.Parse(reader["Id"].ToString()), reader.GetString(1));
                types.Add(type);
            }
            reader.Close();
            con.Close();
            return types;
            //////////////////////////////////////////////////////////////////////////////
        }
        public static List<SiteUser> ReadUser()
        {
            var users = new List<SiteUser>();
            SiteUser user;
            string myCompanyDB = configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB);
            string sql = "SELECT * FROM Users";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               user= new SiteUser(Int32.Parse(reader["ID"].ToString()), reader.GetString(1).TrimEnd(' '), reader.GetString(2).TrimEnd(' '),reader.GetString(3), Int32.Parse(reader["typeID"].ToString()), (bool)(reader["active"]));
               users.Add(user);
            }
            reader.Close();
            con.Close();
            return users;
        }
        public static SiteUser GetUser(int id)
        {
            var users = ReadUser();
            foreach (var u in users)
            {
                if(u.id == id)
                {
                    return u;
                }
            }
            return new SiteUser();
        }
        public static SiteUser GetUser(string name)
        {
            var users = ReadUser();
            foreach (var u in users)
            {
                if (name == u.userName)
                {
                    return u;
                }
            }
            return new SiteUser();
        }
    }
}

