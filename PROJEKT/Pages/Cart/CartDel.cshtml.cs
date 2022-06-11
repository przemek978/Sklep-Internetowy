using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PROJEKT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PROJEKT.Pages
{
    public class CartDelModel : Session
    {
        //////////////////////////////////////////////////////////////////////////////////////////
        public CartDelModel(IConfiguration configuration)
        {
            DataBase.configuration = configuration;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        [FromQuery(Name = "id")]
        public int id { get; set; }
        public List<Product> productList;
        public List<Product> productC;
        public Product product { get; set; }
        public int[] ilosci;
        public decimal suma = 0;

        public IActionResult OnGet()
        {
            var cookieValue = Request.Cookies["Cart"];
            string newcook = "";
            productList = DataBase.Read();
            int maxid = 0;
            for (int i = 0; i < productList.Count; i++)
            {
                if (productList[i].id > maxid)
                    maxid = productList[i].id;
            }
            ilosci = new int[maxid + 1];
            if (cookieValue != null)
            {
                for (int i = 0; i < cookieValue.Length; i++)
                {
                    ilosci[cookieValue[i] - 48]++;
                }
            }
            ilosci[id]--;
            productC = new List<Product>();
            for (int i = 1; i <= productList.Count; i++)
            {
                if (ilosci[i] != 0)
                    productC.Add(productList[i - 1]);
            }
            for (int i = 1; i <= maxid; i++)
            {
                for (int j = 0; j < productList.Count; j++)
                {
                    if (productList[j].id == i)
                        for (int z = 0; z < ilosci[i]; z++)
                        {
                            newcook += productList[j].id.ToString();
                        }
                }
            }
            Response.Cookies.Append("Cart", newcook);
            return RedirectToPage("Cart");
        }
    }
}
