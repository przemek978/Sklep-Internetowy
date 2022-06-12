using Microsoft.AspNetCore.Http;
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
    public class CartModel : Session
    {
        public List<Product> productC;
        public List<Product> productList;
        public int[] ilosci;
        public decimal suma=0;
        public void OnGet()
        {
            var cookieValue = Request.Cookies["Cart"];
            productList = DataBase.Read();
            int maxid = 0,i=0;
            for (i = 0; i < productList.Count; i++)
            {
                if (productList[i].id > maxid)
                    maxid = productList[i].id;
            }
            ilosci = new int[maxid + 1];
            if (cookieValue != null)
            {
                for (i = 0; i < cookieValue.Length; i++)
                {
                    ilosci[cookieValue[i] - 48]++;
                }
            }
            productC = new List<Product>();
            for (i = 1; i <= maxid; i++)
            {
                if (ilosci[i] != 0)
                {
                    for (int j = 0; j < productList.Count; j++)
                    {
                        if (productList[j].id == i)
                            productC.Add(productList[j]);
                    }
                }
            }
            foreach (var p in productC)
            {
                suma += p.price * ilosci[p.id];
            }
            SetSession();
        }
        public IActionResult OnPost()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append("Cart", "", cookieOptions);
            return RedirectToPage("Cart");
        }

    }
}
