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
        static string myCompanyDB;
        SqlConnection con;
        public ZamowienieSqlDB(IConfiguration configuration)
        {
            myCompanyDB = configuration.GetConnectionString("myCompanyDB");
            con = new SqlConnection(myCompanyDB);
            DataBase.configuration = configuration;
        }
        public List<Zamowienie> List()
        {
            var ZamowienieList = new List<Zamowienie>();
            Zamowienie order;
            SqlConnection con = new SqlConnection(myCompanyDB);
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
            var orders=List();
            Zamowienie z=new Zamowienie();
            foreach(var o in orders)
            {
                if(o.id==id)
                {
                    z.id = o.id;
                    z.UserID = o.UserID;
                    z.produkty = o.produkty;
                }
            }
            return z;
        }
        public void Update(Zamowienie z)
        {
            SqlConnection con = new SqlConnection(myCompanyDB);
            string sql = "UPDATE [Order] SET UserID=@USERID, products=@PRODUKTY WHERE id=@ID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", z.id);
            cmd.Parameters.AddWithValue("@USERID", z.UserID);
            cmd.Parameters.AddWithValue("@PRODUKTY", z.produkty);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Delete(int id)
        {
            SqlConnection con = new SqlConnection(myCompanyDB);
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;
            sql = "Delete from [Order] where id= @ID";
            cmd = new SqlCommand(sql, con);
            adapter.DeleteCommand = cmd;
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            adapter.DeleteCommand.ExecuteNonQuery();
            cmd.Dispose();
        }
        public void Add(Zamowienie z)
        {
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "Insert into [Order](UserID,products) values(@UserID,@products)";
            cmd = new SqlCommand(sql, con);
            adapter.InsertCommand = cmd;
            cmd.Parameters.AddWithValue("@UserID", z.UserID);
            cmd.Parameters.AddWithValue("@products", z.produkty);
            con.Open();
            adapter.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}
