using Microsoft.Extensions.Configuration;
using PROJEKT.Models;
using System.Collections.Generic;
using System.Xml;

namespace PROJEKT.DAL
{
    public class ZamowienieXmlDB : IZamowienieDB
    {
        XmlDocument db = new XmlDocument();
        string xmlDB_path;
        public ZamowienieXmlDB(IConfiguration _configuration)
        {
            xmlDB_path = _configuration.GetValue<string>("AppSettings:XmlDB_path");
            LoadDB();
        }
        private void LoadDB()
        {
            db.Load(xmlDB_path);
        }
        public List<Zamowienie> List()
        {
            var orderList = new List<Zamowienie>();
            XmlNodeList orderXmlNodeList = db.SelectNodes("/store/order");

            foreach (XmlNode orderXmlNode in orderXmlNodeList)
            {
                orderList.Add(XmlNodeorder2order(orderXmlNode));
            }
            return orderList;
        }
        private Zamowienie XmlNodeorder2order(XmlNode node)
        {
            var z = new Zamowienie();
            z.id = int.Parse(node.Attributes["id"].Value);
            z.produkty = node["produkty"].InnerText;
            return z;
        }
        private void OpenXmlBase()
        {
            db.Load("DATA/store.xml");
        }
        private void SaveXmlBase()
        {
            db.Save("DATA/store.xml");
        }
        public Zamowienie Get(int id)
        {
            XmlNode node = null;
            XmlNodeList list = db.SelectNodes("/store/order[@id=" + id.ToString() + "]");
            node = list[0];
            return XmlNodeorder2order(node);
        }
        public void Update(Zamowienie z)
        {
            XmlNode node = null;
            XmlNodeList list = db.SelectNodes("/store/order[@id=" + z.id.ToString() + "]");
            node = list[0];
            node["produkty"].InnerText = z.produkty.ToString();
            SaveXmlBase();
        }
        public void Delete(int id)
        {
            XmlNode node = null;
            XmlNodeList list = db.SelectNodes("/store/order[@id=" + id.ToString() + "]");
            node = list[0];
            list[0].ParentNode.RemoveChild(node);
            SaveXmlBase();
        }
        public void Add(Zamowienie z)
        {
            XmlNodeList list = db.SelectNodes("/store/order");
            XmlElement el = db.CreateElement("order");
            if (list.Count == 0) 
                el.SetAttribute("id", 1.ToString());
            else 
                el.SetAttribute("id", (int.Parse(list[list.Count - 1].Attributes["id"].Value) + 1).ToString());
            XmlNode produkty = db.CreateElement("produkty");
            produkty.InnerText = z.produkty;
            el.AppendChild(produkty);
            db.DocumentElement.AppendChild(el);
            SaveXmlBase();
        }
    }
}
