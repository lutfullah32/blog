using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BilisimProjesi.Context;
using BilisimProjesi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilisimProjesi.Controllers
{
    public class AnasayfaController : Controller
    {
        private readonly BlogContext db;
        public AnasayfaController(BlogContext context)
        {
            db = context;
        }

        [Route("/{sayfa:int?}")]
        public IActionResult Index(int? sayfa)
        {

            if (sayfa == null || sayfa == 1)
            {
                ViewBag.posts = db.Postlar.Take(6).ToList();
                ViewBag.sayfa = 1;
                ViewBag.sayfalar =(int)Math.Ceiling((double)db.Postlar.ToList().Count / (double)6);
            }
            else
            {
                ViewBag.posts = db.Postlar.Skip(6 * (int)sayfa - 6).Take(6).ToList();
                ViewBag.sayfalar = (int)Math.Ceiling((double)db.Postlar.ToList().Count / (double)6);
                ViewBag.sayfa = (int)sayfa;
            }

            return View();
        }

        [Route("/{seflink}")]
        public ActionResult Post(string seflink)
        {
            SadecePost post = (from p in db.Postlar
                               join k in db.Kategoriler on p.KategoriID equals k.ID
                               join y in db.Kullanicilar on p.YazarID equals y.ID
                               where p.SefLink == seflink
                               select new SadecePost { id = p.ID, baslik = p.Baslik, icerik = p.Icerik, tarih = p.Tarih, yazar = y.KullaniciAdi, kategori = k.Kategori,kategoriid=k.ID }).SingleOrDefault();

            return View(post);
        }

        [Route("/kategori/{id:int}")]
        public ActionResult KategoriPost(int id)
        {
            ViewBag.posts = db.Postlar.Where(x => x.KategoriID == id).ToList();
            return View();
        }

        [HttpGet("/ara")]
        public ActionResult Ara(string query)
        {
            ViewBag.query = query;
            var posts = db.Postlar.Where(x => x.Baslik.ToLower().Contains(query.ToLower()) || x.Icerik.ToLower().Contains(query.ToLower())).ToList();
            return View(posts);
        }
    }
}