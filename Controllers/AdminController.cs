using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BilisimProjesi.Context;
using BilisimProjesi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilisimProjesi.Controllers
{
    public class AdminController : Controller
    {
        BlogContext db;
        public AdminController(BlogContext context)
        {
            db = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }

        /* İÇERİK İŞLERİMLERİ BAŞLANGICI */

        [Route("admin/icerikler")]
        public IActionResult Icerikler()
        {
            var ps = (from x in db.Postlar
                      join y in db.Kullanicilar on x.YazarID equals y.ID
                      join s in db.Kategoriler on x.KategoriID equals s.ID
                      select new Icerikler { id = x.ID, baslik = x.Baslik, icerik = x.Icerik, tarih = x.Tarih, yazar = y.KullaniciAdi, kategori = s.Kategori });
            return View(ps.ToList());
        }

        [Route("admin/icerik/ekle")]
        public IActionResult IcerikEkle()
        {
            ViewBag.yazarlar = (from x in db.Kullanicilar
                                select new SadeceKullanici { id = x.ID, kadi = x.KullaniciAdi }).ToList();
            ViewBag.kategoriler = (from x in db.Kategoriler
                                   select new SadeceKategori { id = x.ID, kategori = x.Kategori }).ToList();
            return View();
        }

        [HttpPost("admin/icerik/ekle")]
        public IActionResult IcerikEkle(Postlar postlar)
        {
            postlar.SefLink = seflink(postlar.Baslik);
            db.Postlar.Add(postlar);
            db.SaveChanges();
            return RedirectToAction("Icerikler");
        }

        [Route("admin/icerik/sil/{id:int}")]
        public IActionResult IcerikSil(int id)
        {
            db.Postlar.Remove(db.Postlar.Where(x => x.ID == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Icerikler");
        }

        [Route("admin/icerik/duzenle/{id:int}")]
        public ActionResult IcerikDuzenle(int id)
        {
            ViewBag.yazarlar = (from x in db.Kullanicilar
                                select new SadeceKullanici { id = x.ID, kadi = x.KullaniciAdi }).ToList();
            ViewBag.kategoriler = (from x in db.Kategoriler
                                   select new SadeceKategori { id = x.ID, kategori = x.Kategori }).ToList();
            ViewBag.icerik = db.Postlar.Where(x => x.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost("admin/icerik/duzenle")]
        public ActionResult IcerikDuzenle(Postlar yeni)
        {
            var eski = db.Postlar.Where(x => x.ID == yeni.ID).FirstOrDefault();
            eski.SefLink = seflink(yeni.Baslik);
            eski.Baslik = yeni.Baslik;
            eski.Icerik = yeni.Icerik;
            eski.Tarih = yeni.Tarih;
            eski.YazarID = yeni.YazarID;
            eski.KategoriID = yeni.KategoriID;
            db.SaveChanges();
            return RedirectToAction("Icerikler");

        }

        /* İÇERİK İŞLEMLERİ SONU */


        /* KATEGORİ İŞLEMLERİ BAŞLANGICI*/

        [Route("admin/kategoriler")]
        public IActionResult Kategoriler()
        {
            var kategoriler = db.Kategoriler.ToList();
            return View(kategoriler);
        }

        [Route("admin/kategori/sil/{id:int}")]
        public IActionResult KategoriSil(int id)
        {
            db.Kategoriler.Remove(db.Kategoriler.Where(x=>x.ID==id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("Kategoriler");
        }

        [HttpPost("admin/kategori/ekle")]
        public ActionResult KategoriEkle(string Kategori)
        {
            Kategoriler kategori = new Kategoriler();
            kategori.Kategori = Kategori;
            kategori.ID = 0;
            db.Kategoriler.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Kategoriler");
        }
        
        [Route("admin/kategori/duzenle/{id:int}")]
        public ActionResult KategoriDuzenle(int id)
        {
            var k = db.Kategoriler.Where(x => x.ID == id).FirstOrDefault();
            return View(k);
        }

        [HttpPost("admin/kategori/duzenle")]
        public ActionResult KategoriDuzenle(Kategoriler kategoriler)
        {
            var d = db.Kategoriler.Where(x => x.ID == kategoriler.ID).SingleOrDefault();
            d.Kategori = kategoriler.Kategori;
            db.SaveChanges();
            return RedirectToAction("Kategoriler");
        }

        /* KATEGORİ İŞLEMLERİ SONU */


        /* YETKİ İŞLEMLERİ BAŞLANGICI */

        [Route("admin/yetkiler")]
        public ActionResult Yetkiler()
        {
            
            return View(db.Yetkiler.ToList());
        }

        [HttpPost("admin/yetki/ekle")]
        public ActionResult YetkiEkle(string Yetki)
        {
            Yetkiler yetki = new Yetkiler();
            yetki.Yetki = Yetki;
            yetki.ID = 0;
            db.Yetkiler.Add(yetki);
            db.SaveChanges();
            return RedirectToAction("Yetkiler");
        }

        [Route("admin/yetki/sil/{id:int}")]
        public IActionResult YetkiSil(int id)
        {
            db.Yetkiler.Remove(db.Yetkiler.Where(x => x.ID == id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("Yetkiler");
        }

        [Route("admin/yetki/duzenle/{id:int}")]
        public ActionResult YetkiDuzenle(int id)
        {
            var y = db.Yetkiler.Where(x => x.ID == id).FirstOrDefault();
            return View(y);
        }

        [HttpPost("admin/yetki/duzenle")]
        public ActionResult YetkiDuzenle(Yetkiler yetkiler)
        {
            var d = db.Yetkiler.Where(x => x.ID == yetkiler.ID).SingleOrDefault();
            d.Yetki = yetkiler.Yetki;
            db.SaveChanges();
            return RedirectToAction("Yetkiler");
        }

        /* YETKİ İŞLEMLERİ SONU */

        
        /* KULLANICI İŞLEMLERİ BAŞLANGICI */

        [Route("admin/kullanicilar")]
        public ActionResult Kullanicilar()
        {
            var liste = (from k in db.Kullanicilar
                         join y in db.Yetkiler on k.YetkiID equals y.ID
                         select new Kullanicilary { id = k.ID, kadi = k.KullaniciAdi, mail = k.Mail, uyeliktarihi = k.UyelikTarihi, yetki = y.Yetki });
            return View(liste.ToList());
        }

        [Route("admin/kullanici/ekle")]
        public ActionResult KullaniciEkle()
        {
            return View(db.Yetkiler.ToList());
        }

        [HttpPost("admin/kullanici/ekle")]
        public ActionResult KullaniciEkle(Kullanicilar kullanicilar)
        {
            db.Kullanicilar.Add(kullanicilar);
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }

        [Route("/admin/kullanici/sil/{id:int}")]
        public ActionResult KullaniciSil(int id)
        {
            db.Kullanicilar.Remove(db.Kullanicilar.Where(x=>x.ID==id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }

        [Route("/admin/kullanici/duzenle/{id:int}")]
        public ActionResult KullaniciDuzenle(int id)
        {
            var k = (from x in db.Kullanicilar
                     where x.ID == id
                     select x).SingleOrDefault();
            ViewBag.Yetkiler = db.Yetkiler.ToList();
            return View(k);
        }

        [HttpPost("/admin/kullanici/duzenle")]
        public ActionResult KullaniciDuzenle(Kullanicilar kullanicilar)
        {
            var k = (from x in db.Kullanicilar
                     where x.ID == kullanicilar.ID
                     select x).SingleOrDefault();
            k.KullaniciAdi = kullanicilar.KullaniciAdi;
            k.Mail = kullanicilar.Mail;
            k.Sifre = kullanicilar.Sifre;
            k.YetkiID = kullanicilar.YetkiID;
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }

        /* KULLANICI İŞLEMLERİ SONU */

        public string seflink(string a)
        {
            a=a.ToLower();
            a=a.Replace('ğ', 'g');
            a=a.Replace('ü', 'u');
            a=a.Replace('ı', 'i');
            a=a.Replace('ş', 's');
            a=a.Replace('ş', 's');
            a=a.Replace('ç', 'c');
            a=a.Replace('ö', 'o');
            a = Regex.Replace(a, @"[^a-z0-9\s-]", "");
            a = Regex.Replace(a, @"\s+", " ").Trim();
            a = a.Substring(0, a.Length <= 45 ? a.Length : 45).Trim();
            a = Regex.Replace(a, @"\s", "-");
            return a;
        }
    }

}