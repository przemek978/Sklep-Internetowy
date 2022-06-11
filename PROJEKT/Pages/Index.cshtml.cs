using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PROJEKT.DAL;
using PROJEKT.Models;
using System.Collections.Generic;
using System.Xml;

namespace PROJEKT.Pages
{
    public class IndexModel : Session
    {
        public void OnGet()
        {
            SetSession();
            //return RedirectToPage("/Products/Index");
        }

    }
}