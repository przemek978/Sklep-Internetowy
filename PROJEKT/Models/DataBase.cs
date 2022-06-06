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
        public static List<Product> Read(IConfiguration configuration)
        {
            int LastID;
            ///ODCZYT BAZY/////////////////////////////////////////////////////////////
            var productList = new List<Product>();
            Product product;
            string myCompanyDB_connection_string = configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
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
            //////////////////////////////////////////////////////////////////////////////
        }
        public static List<TypeUser> ReadTypes(IConfiguration configuration)
        {
            int LastID;
            ///ODCZYT BAZY/////////////////////////////////////////////////////////////
            var types = new List<TypeUser>();
            TypeUser type;
            string myCompanyDBcs = configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM TypeUser";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                type = new TypeUser(Int32.Parse(reader["Id"].ToString()), reader.GetString(1));
                types.Add(type);
                LastID = Int32.Parse(reader["Id"].ToString());
            }
            reader.Close();
            con.Close();
            return types;
            //////////////////////////////////////////////////////////////////////////////
        }
        /*public static List<Zamowienie> ReadOrder(IConfiguration configuration)
        {
            int LastID;
            ///ODCZYT BAZY/////////////////////////////////////////////////////////////
            var ZamowienieList = new List<Zamowienie>();
            Zamowienie order;
            string myCompanyDBcs = configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM [Order]";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                order = new Zamowienie(Int32.Parse(reader["Id"].ToString()), reader.GetString(1));
                ZamowienieList.Add(order);
            }
            reader.Close();
            con.Close();
            return ZamowienieList;
            //////////////////////////////////////////////////////////////////////////////
        }*/
        public static List<SiteUser> ReadUser(IConfiguration configuration)
        {
            ///ODCZYT BAZY/////////////////////////////////////////////////////////////
            var users = new List<SiteUser>();
            SiteUser user;
            string myCompanyDBcs = configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Users";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               user= new SiteUser(Int32.Parse(reader["ID"].ToString()), reader.GetString(1).TrimEnd(' '), reader.GetString(2).TrimEnd(' ') ,Int32.Parse(reader["typeID"].ToString()));
               users.Add(user);
            }
            reader.Close();
            con.Close();
            return users;
            //////////////////////////////////////////////////////////////////////////////
        }
    }
}
