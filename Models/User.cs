using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilisimProjesi.Models
{
    public class Kullanicilar
    {
        public int ID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Mail { get; set; }
        public int YetkiID { get; set; }
        public DateTime UyelikTarihi { get; set; }

    }
    public class Yetkiler
    {
        public int ID { get; set; }
        public string Yetki { get; set; }

    }
}
