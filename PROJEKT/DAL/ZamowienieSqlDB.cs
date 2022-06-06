using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PROJEKT.Models;
using System.Data.SqlClient;

namespace PROJEKT.DAL
{
    public class ZamowienieSqlDB : IZamowienieDB
    {
        public IConfiguration _configuration { get; }
        static string myCompanyDBcs;
        SqlConnection con;
        public ZamowienieSqlDB(IConfiguration configuration)
        {
            myCompanyDBcs = configuration.GetConnectionString("myCompanyDB");
            con = new SqlConnection(myCompanyDBcs);
            _configuration = configuration;
        }
        public List<Zamowienie> List()
        {
            var ZamowienieList = new List<Zamowienie>();
            Zamowienie order;
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM [Order]";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                order = new Zamowienie(Int32.Parse(reader["Id"].ToString()),Int32.Parse(reader["UserID"].ToString()), reader.GetString(2));
                ZamowienieList.Add(order);
            }
            reader.Close();
            con.Close();
            return ZamowienieList;
        }
        public Zamowienie Get(int id)
        {
            return new Zamowienie();  
        }
        public void Update(Zamowienie z)
        {
           
        }
        public void Delete(int id)
        {
            
        }
        public void Add(Zamowienie z)
        {
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "Insert into [Order](UserID,products) values(@UserID,@products)";
            cmd = new SqlCommand(sql, con);
            adapter.InsertCommand = cmd;
            //cmd.Parameters.AddWithValue("@ID", LastID + 1);
            cmd.Parameters.AddWithValue("@UserID", z.UserID);
            cmd.Parameters.AddWithValue("@products", z.produkty);
            con.Open();
            adapter.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}
