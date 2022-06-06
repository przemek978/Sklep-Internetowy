using PROJEKT.Models;
using System.Collections.Generic;

namespace PROJEKT.DAL
{
    public interface IZamowienieDB
    {
        public List<Zamowienie> List();
        public Zamowienie Get(int _id);
        public void Update(Zamowienie z);
        public void Delete(int _id);
        public void Add(Zamowienie z);
    }
}
