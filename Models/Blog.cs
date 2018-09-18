using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilisimProjesi.Models
{
    public class Postlar
    {
        public int ID { get; set; }
        [Display(Name ="Başlık")]
        public string Baslik { get; set; }
        public string SefLink { get; set; }
        [Display(Name = "İçerik")]
        public string Icerik { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Tarih")]
        public DateTime Tarih { get; set; }
        [Display(Name = "Yazar")]
        public int YazarID { get; set; }
        [Display(Name = "Kategori")]
        public int KategoriID { get; set; }
        public string ResimYolu { get; set; }


    }

    public class Yorumlar
    {
        public int ID { get; set; }
        public string YorumMetni { get; set; }
        public int YorumYapanID { get; set; }
        public DateTime Tarih { get; set; }

    }
    public class Kategoriler
    {
        public int ID { get; set; }
        public string Kategori { get; set; }


    }
}
