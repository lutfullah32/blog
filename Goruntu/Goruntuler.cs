using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilisimProjesi.Models
{
    public class Icerikler
    {
        public int id { get; set; }
        public string baslik { get; set; }
        public string icerik { get; set; }
        public DateTime tarih { get; set; }
        public string yazar { get; set; }
        public string kategori { get; set; }
    }

    public class SadeceKullanici
    {
        public int id { get; set; }
        public string kadi { get; set; }
    }

    public class SadeceKategori
    {
        public int id { get; set; }
        public string kategori { get; set; }
    }

    public class Kullanicilary
    {
        public int id { get; set; }
        public string kadi { get; set; }
        public string mail { get; set; }
        public string yetki { get; set; }
        public DateTime uyeliktarihi { get; set; }
    }

    public class SadecePost
    {
        public int id { get; set; }
        public string baslik { get; set; }
        public string icerik { get; set; }
        public DateTime tarih { get; set; }
        public string yazar { get; set; }
        public string kategori { get; set; }
        public int kategoriid { get; set; }
    }
}
