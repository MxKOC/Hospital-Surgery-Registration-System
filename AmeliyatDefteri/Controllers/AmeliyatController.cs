using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AmeliyatDefteri.Entity;
using AmeliyatDefteri.Models;
using AmeliyatDefteri.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using SQLitePCL;

namespace AmeliyatDefteri.Controllers
{

    public class AmeliyatController : Controller
    {

        private readonly IStatisticsService _statisticsService;
        private readonly DataContext _context;
        private readonly IEmailSender _emailSender;

        public AmeliyatController(DataContext context, IStatisticsService statisticsService, IEmailSender emailSender)
        {
            _context = context;
            _statisticsService = statisticsService;
            _emailSender = emailSender;

        }




        [HttpGet]
        public async Task<IActionResult> IndirPdf()
        {
            var generator = new PdfGenerator();
            generator.GeneratePdf();
            return Ok("PDF başarıyla oluşturuldu.");
        }




        [HttpGet]
        public IActionResult Index(IstatistikDokAmeliyatViewModel model1, IstatistikDokAnesteziViewModel model2, IstatistikHastaViewModel model3)
        {

            var Sonuçlar = _context.Zamanlar.Include(x => x.Ameliyat).Include(x => x.Doktor).ToList();

            var gunler = _statisticsService.GetAllowedDays();
            var Baslangiczaman = DateTime.Today;
            var hedefGunTarihleri = _statisticsService.GetHedefGunTarihleri(gunler, Baslangiczaman, 360);
            var zipList = hedefGunTarihleri.Select(date => new { GunAdi = date.DayOfWeek.ToString(), Tarih = date }).ToList();

            ViewBag.AllowedDays = gunler;
            ViewBag.ZipListe2 = zipList;

            ViewBag.ameliyatListesi = _statisticsService.GetAmeliyatList();
            ViewBag.doktorListesi = _statisticsService.GetDoktorList();
            ViewBag.AnesteziListesi = _statisticsService.GetAnesteziList();

            ViewBag.ameliyatgunutopluliste = _statisticsService.GetAmeliyatGunTopluListe(hedefGunTarihleri, _context.Ameliyatlar.Select(x => x.Id).ToList());
            ViewBag.doktorgunutopluliste = _statisticsService.GetDoktorGunTopluListe(hedefGunTarihleri, _context.Doktorlar.Select(x => x.Id).ToList());
            ViewBag.doktoranestezigunutopluliste = _statisticsService.GetDoktorAnesteziGunTopluListe(hedefGunTarihleri, _context.Anesteziler.Select(x => x.Id).ToList());






            /////////////////////////////////
            ///                                                             İstatistik




            ViewBag.ameliyatListesi = _context.Ameliyatlar.Select(x => x.Name).ToList();

            if (model1.Ameliyat_Name != null)
            {


                var hedefGunTarihleri30 = _statisticsService.GetHedefGunTarihleri(gunler, Baslangiczaman, 360);

                var ZipList = hedefGunTarihleri30.Select(date => new { GunAdi = date.DayOfWeek.ToString(), Tarih = date }).ToList();
                var ameliyatgunuliste = _statisticsService.GetAmeliyatGunListe(model1, hedefGunTarihleri30);


                return Json(new { ZipList, ameliyatgunuliste });

            }



            /////////////////////////////// Model2





            if (model2.Anestezi_Name != null)
            {
                var hedefGunTarihleri30 = _statisticsService.GetHedefGunTarihleri(gunler, Baslangiczaman, 360);

                var ZipList = hedefGunTarihleri30.Select(date => new { GunAdi = date.DayOfWeek.ToString(), Tarih = date }).ToList();
                var ameliyatgunuliste = _statisticsService.GetDoktorAnesteziGunListe(model2, hedefGunTarihleri30);


                return Json(new { ZipList, ameliyatgunuliste });

            }




            /////////////////////////////// Model3




            if (model3.Hasta_Name != null)
            {
                ViewBag.hastatarih = _statisticsService.GetHastaTarih(model3);
            }


            ViewBag.DoktorListesi2 = new SelectList(_statisticsService.GetDoktorList());
            ViewBag.AnesteziListesi2 = new SelectList(_statisticsService.GetAnesteziList());
            ViewBag.AmeliyatListesi2 = new SelectList(_statisticsService.GetAmeliyatList());


            var model = new IstatistikViewModel()
            {
                IstatistikDokAmeliyatViewModel = model1,
                IstatistikDokAnesteziViewModel = model2,
                IstatistikHastaViewModel = model3,
            };


            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DateTime time)
        {




            return RedirectToAction("EkleYeniAmeliyat", new { controller = "Ameliyat", action = "EkleYeniAmeliyat", time = time });

        }

        [HttpGet]
        public IActionResult Gecmis(string anestezi)
        {
            var anestezidate = DateTime.ParseExact(anestezi, "dd.MM.yyyy HH:mm", null);
            var gecmisler = _context.Gecmisler.Where(x => x.AmeliyatGünü == anestezidate).ToList();


            return View(gecmisler);
        }


        public IActionResult CreateModal(DateTime Azaman)
        {
            ViewBag.ameliyatListesi = _statisticsService.GetAmeliyatList();
            ViewBag.doktorListesi = _statisticsService.GetDoktorList();
            ViewBag.AnesteziListesi = _statisticsService.GetAnesteziList();

            var DuyuruListesi2 = _context.Duyurular.ToList();
            ViewBag.DuyuruListesi2 = DuyuruListesi2;

            Console.WriteLine("---------------------->" + Azaman);
            var GunlukAmeliyatlar = _context.Zamanlar.Where(x => x.AmeliyatGünü == Azaman).Include(x => x.Ameliyat).Include(x => x.Doktor).Include(x => x.Anestezi).ToList();
            ViewBag.GunlukAmeliyatlar = GunlukAmeliyatlar;


            return PartialView("_ModalView");
        }

        [HttpPost]
        public IActionResult CreateModal(AmeliyatCreateViewModel model)
        {
            // todo db operations etc...

            Console.WriteLine("Kullanici" + model.Kullanici);
            Console.WriteLine("Hasta_Name" + model.Hasta_Name);
            Console.WriteLine("Telefon" + model.Telefon);
            Console.WriteLine("Detay" + model.Detay);
            Console.WriteLine("Anestezi_Name" + model.Anestezi_Name);
            Console.WriteLine("Doktor_Name" + model.Doktor_Name);
            Console.WriteLine("Ameliyat_Name" + model.Ameliyat_Name);


            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Ameliyat");
            }

            var doktor = _context.Doktorlar.FirstOrDefault(x => x.Name == model.Doktor_Name);
            var ameliyat = _context.Ameliyatlar.FirstOrDefault(x => x.Name == model.Ameliyat_Name);
            var anestezi = _context.Anesteziler.FirstOrDefault(x => x.Name == model.Anestezi_Name);








            Zaman zaman = new Zaman
            {
                Name = model.Hasta_Name,
                Telefon = model.Telefon,
                Detay = model.Detay,
                AmeliyatGünü = model.AmeliyatGünü,
                AmeliyatId = ameliyat.Id,
                DoktorId = doktor.Id,
                AnesteziId = anestezi.Id
            };
            _context.Zamanlar.Add(zaman);
            _context.SaveChanges();

            Gecmis gecmis = new Gecmis
            {
                Kullanici = model.Kullanici,
                IslemTarihi = DateTime.Now,
                Olay = "Yeni Oluşturma",
                Name = model.Hasta_Name,
                Telefon = model.Telefon,
                Detay = model.Detay,
                AmeliyatGünü = model.AmeliyatGünü,
                Ameliyat = ameliyat.Name,
                Doktor = doktor.Name,
                Anestezi = anestezi.Name
            };
            _context.Gecmisler.Add(gecmis);
            _context.SaveChanges();

            return Json(" kaydı eklendi !");
        }


        [HttpGet]
        public IActionResult Duyuru()
        {

            var DuyuruListesi2 = _context.Duyurular.ToList();
            ViewBag.DuyuruListesi2 = DuyuruListesi2;

            var DuyuruListesi = _context.Duyurular.Select(x => x.Icerik).ToList();
            ViewBag.DuyuruListesi = new SelectList(DuyuruListesi);

            return View();
        }

        [HttpPost]
        public IActionResult Duyuru(EkleDuyuruViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Ameliyat");
            }

            model.Bitis = (DateTime)model.Bitis;
            model.Baslangic = (DateTime)model.Baslangic;

            var duyuru = new Duyuru
            {
                Baslangic = model.Baslangic,
                Bitis = model.Bitis,
                Icerik = model.Icerik
            };
            _context.Duyurular.Add(duyuru);
            _context.SaveChanges();
            return RedirectToAction("Duyuru", "Ameliyat");

        }



        [HttpPost]
        public IActionResult SilDuyuru(SilDuyuruViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Ameliyat");
            }
            var duyuru = _context.Duyurular.FirstOrDefault(x => x.Icerik == model.Icerik);
            _context.Duyurular.Remove(duyuru);
            _context.SaveChanges();
            return RedirectToAction("Duyuru", "Ameliyat");
        }






        [HttpPost]
        public IActionResult BulDokAnestezi(IstatistikDokAnesteziViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Ameliyat", model); ;
        }


        [HttpPost]
        public IActionResult BulDoktorAmeliyat(IstatistikDokAmeliyatViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Home");
            }



            return RedirectToAction("Index", "Ameliyat", model);

        }

        [HttpPost]
        public IActionResult BulHasta(IstatistikHastaViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Home");
            }



            return RedirectToAction("Index", "Ameliyat", model);

        }



        [HttpGet]
        public IActionResult EkleYeniAmeliyat(DateTime time)
        {
            AmeliyatCreateViewModel model = new AmeliyatCreateViewModel();

            var doktorListesi = _context.Doktorlar.Select(x => x.Name).ToList();
            ViewBag.doktorListesi = doktorListesi;

            var AnesteziListesi = _context.Anesteziler.Select(x => x.Name).ToList();
            ViewBag.AnesteziListesi = AnesteziListesi;

            var AmeliyatListesi = _context.Ameliyatlar.Select(x => x.Name).ToList();
            ViewBag.AmeliyatListesi = AmeliyatListesi;

            var DuyuruListesi = _context.Duyurular.Where(x => x.Baslangic <= time && x.Bitis >= time).Select(x => x.Icerik).ToList();
            ViewBag.DuyuruListesi = DuyuruListesi;

            var Baslangiczaman = DateTime.Today;
            var AnlikZaman = DateTime.Today;
            List<string> hedefGunIsimleri = new List<string>();
            List<DateTime> hedefGunTarihleri = new List<DateTime>();


            var gunler = _context.AmeliyatGunleri.Select(x => x.Gun).ToList();

            ViewBag.AllowedDays = gunler;

            for (int i = 0; i < 30; i++)
            {
                DateTime gunTarihi = AnlikZaman.AddDays(i);


                if (gunler.Contains(gunTarihi.DayOfWeek))
                {
                    hedefGunIsimleri.Add(gunTarihi.DayOfWeek.ToString());
                    hedefGunTarihleri.Add(gunTarihi);
                }
            }
            ViewBag.hedefGunTarihleri = hedefGunTarihleri;




            var GunlukAmeliyatlar = _context.Zamanlar.Where(x => x.AmeliyatGünü == time).Include(x => x.Ameliyat).Include(x => x.Doktor).Include(x => x.Anestezi).ToList();


            ViewBag.GunlukAmeliyatlar = GunlukAmeliyatlar;

            model.AmeliyatGünü = time;





            return View(model);
        }


        [HttpPost]
        public IActionResult EkleYeniAmeliyat(AmeliyatCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Ameliyat");
            }

            var doktor = _context.Doktorlar.FirstOrDefault(x => x.Name == model.Doktor_Name);
            var ameliyat = _context.Ameliyatlar.FirstOrDefault(x => x.Name == model.Ameliyat_Name);
            var anestezi = _context.Anesteziler.FirstOrDefault(x => x.Name == model.Anestezi_Name);








            Zaman zaman = new Zaman
            {
                Name = model.Hasta_Name,
                Telefon = model.Telefon,
                Detay = model.Detay,
                AmeliyatGünü = model.AmeliyatGünü,
                AmeliyatId = ameliyat.Id,
                DoktorId = doktor.Id,
                AnesteziId = anestezi.Id
            };
            _context.Zamanlar.Add(zaman);
            _context.SaveChanges();

            Gecmis gecmis = new Gecmis
            {
                Kullanici = model.Kullanici,
                IslemTarihi = DateTime.Now,
                Olay = "Yeni Oluşturma",
                Name = model.Hasta_Name,
                Telefon = model.Telefon,
                Detay = model.Detay,
                AmeliyatGünü = model.AmeliyatGünü,
                Ameliyat = ameliyat.Name,
                Doktor = doktor.Name,
                Anestezi = anestezi.Name
            };
            _context.Gecmisler.Add(gecmis);
            _context.SaveChanges();



            return RedirectToAction("Index", "Ameliyat");
        }







        [HttpGet]
        public IActionResult Degistir(int? id)
        {

            //AmeliyatCreateViewModel model = new AmeliyatCreateViewModel();





            var doktorListesi = _context.Doktorlar.Select(x => x.Name).ToList();
            ViewBag.doktorListesi = doktorListesi;

            var ameliyatListesi = _context.Ameliyatlar.Select(x => x.Name).ToList();
            ViewBag.ameliyatListesi = ameliyatListesi;

            var AnesteziListesi = _context.Anesteziler.Select(x => x.Name).ToList();
            ViewBag.AnesteziListesi = AnesteziListesi;



            var Baslangiczaman = DateTime.Today;
            var AnlikZaman = DateTime.Today;
            List<string> hedefGunIsimleri = new List<string>();
            List<DateTime> hedefGunTarihleri = new List<DateTime>();

            var gunler = _context.AmeliyatGunleri.Select(x => x.Gun).ToList();


            for (int i = 0; i < 30; i++)
            {
                DateTime gunTarihi = AnlikZaman.AddDays(i);


                if (gunler.Contains(gunTarihi.DayOfWeek))
                {

                    hedefGunIsimleri.Add(gunTarihi.DayOfWeek.ToString());
                    hedefGunTarihleri.Add(gunTarihi);
                }
            }
            ViewBag.hedefGunTarihleri = hedefGunTarihleri;


            var zipList = hedefGunIsimleri.Zip(hedefGunTarihleri, (gun, tarih) => new { GunAdi = gun, Tarih = tarih });
            ViewBag.ZipList = zipList;



            var ameliyat = _context.Zamanlar.Include(x => x.Ameliyat).Include(x => x.Doktor).Include(x => x.Anestezi).FirstOrDefault(x => x.Id == id);


            var newModel = new AmeliyatChangeViewModel();
            newModel.id = ameliyat.Id; /////////////////////////////////////////id değişiti
            newModel.Anestezi_Name = ameliyat.Anestezi.Name;
            newModel.Ameliyat_Name = ameliyat.Ameliyat.Name;
            newModel.Hasta_Name = ameliyat.Name;
            newModel.Doktor_Name = ameliyat.Name;
            newModel.Telefon = ameliyat.Telefon;
            newModel.Detay = ameliyat.Detay;
            newModel.AmeliyatGünü = ameliyat.AmeliyatGünü;



            return View(newModel);
        }



        [HttpPost]
        public IActionResult Degistir(AmeliyatChangeViewModel model)
        {




            Zaman zaman = _context.Zamanlar.FirstOrDefault(x => x.Id == model.id);   /////////////////////////////////////////id değişiti


            if (model.isDelete == true)
            {

                var doktor = _context.Doktorlar.FirstOrDefault(x => x.Name == model.Doktor_Name);
                var anestezi = _context.Anesteziler.FirstOrDefault(x => x.Name == model.Anestezi_Name);
                var ameliyat = _context.Ameliyatlar.FirstOrDefault(x => x.Name == model.Ameliyat_Name);



                _context.Zamanlar.Remove(zaman);
                _context.SaveChanges();

                Gecmis gecmis = new Gecmis
                {
                    Kullanici = model.Kullanici,
                    IslemTarihi = DateTime.Now,
                    Olay = "Silme",
                    Name = model.Hasta_Name,
                    Telefon = model.Telefon,
                    Detay = model.Detay,
                    AmeliyatGünü = model.AmeliyatGünü,
                    Ameliyat = ameliyat.Name,
                    Doktor = doktor.Name,
                    Anestezi = anestezi.Name
                };
                _context.Gecmisler.Add(gecmis);
                _context.SaveChanges();
            }


            else
            {




                var doktor = _context.Doktorlar.FirstOrDefault(x => x.Name == model.Doktor_Name);
                var anestezi = _context.Anesteziler.FirstOrDefault(x => x.Name == model.Anestezi_Name);
                var ameliyat = _context.Ameliyatlar.FirstOrDefault(x => x.Name == model.Ameliyat_Name);

                zaman.AmeliyatGünü = model.AmeliyatGünü;
                zaman.DoktorId = doktor.Id;
                zaman.AnesteziId = anestezi.Id;
                zaman.AmeliyatId = ameliyat.Id;
                zaman.Detay = model.Detay;
                zaman.Telefon = model.Telefon;
                zaman.Name = model.Hasta_Name;
                _context.SaveChanges();




                Gecmis gecmis = new Gecmis
                {
                    Kullanici = model.Kullanici,
                    IslemTarihi = DateTime.Now,
                    Olay = "Değitirme",
                    Name = model.Hasta_Name,
                    Telefon = model.Telefon,
                    Detay = model.Detay,
                    AmeliyatGünü = model.AmeliyatGünü,
                    Ameliyat = ameliyat.Name,
                    Doktor = doktor.Name,
                    Anestezi = anestezi.Name
                };
                _context.Gecmisler.Add(gecmis);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Ameliyat");
        }






























        [HttpGet]
        public IActionResult DuzenleNesneler()
        {

            var DoktorListesi = _context.Doktorlar.Select(x => x.Name).ToList();
            ViewBag.DoktorListesi = new SelectList(DoktorListesi);

            var AnesteziListesi = _context.Anesteziler.Select(x => x.Name).ToList();
            ViewBag.AnesteziListesi = new SelectList(AnesteziListesi);

            var AmeliyatListesi = _context.Ameliyatlar.Select(x => x.Name).ToList();
            ViewBag.AmeliyatListesi = new SelectList(AmeliyatListesi);

            var GunListesi = _context.AmeliyatGunleri.Select(x => x.GunTurkce).ToList();
            ViewBag.GunListesi = new SelectList(GunListesi);

            List<string> ToplamGunlerTurkce = new()
            {
                "Pazartesi",
                "Salı",
                "Çarşamba",
                "Perşembe",
                "Cuma"
            };
            ViewBag.ToplamGunlerTurkce = new SelectList(ToplamGunlerTurkce);
            foreach (var item in GunListesi)
            {
                if (ToplamGunlerTurkce.Contains(item))
                {
                    ToplamGunlerTurkce.Remove(item);
                }
            }
            ViewBag.ToplamGunlerTurkce = new SelectList(ToplamGunlerTurkce);

            return View();
        }





        [HttpPost]
        public IActionResult OlusturDoktor(DoktorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Ameliyat");
            }

            Doktor doktor = new Doktor();
            doktor.Name = model.Doktor_Name;
            _context.Doktorlar.Add(doktor);
            _context.SaveChanges();
            return RedirectToAction("DuzenleNesneler", "Ameliyat");

        }

        [HttpPost]
        public IActionResult SilDoktor(DoktorViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Ameliyat");
            }

            Doktor doktor = _context.Doktorlar.FirstOrDefault(x => x.Name == model.Doktor_Name);
            _context.Doktorlar.Remove(doktor);
            _context.SaveChanges();

            return RedirectToAction("DuzenleNesneler", "Ameliyat");

        }




        [HttpPost]
        public IActionResult OlusturAmeliyat(AmeliyatViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Ameliyat");
            }

            Ameliyat ameliyat = new Ameliyat();
            ameliyat.Name = model.Ameliyat_Name;
            _context.Ameliyatlar.Add(ameliyat);
            _context.SaveChanges();
            return RedirectToAction("DuzenleNesneler", "Ameliyat");

        }

        [HttpPost]
        public IActionResult SilAmeliyat(AmeliyatViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Ameliyat");
            }

            Ameliyat ameliyat = _context.Ameliyatlar.FirstOrDefault(x => x.Name == model.Ameliyat_Name);
            _context.Ameliyatlar.Remove(ameliyat);
            _context.SaveChanges();
            return RedirectToAction("DuzenleNesneler", "Ameliyat");

        }



        [HttpPost]
        public IActionResult OlusturAnestezi(AnesteziViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Ameliyat");
            }

            Anestezi anestezi = new Anestezi();
            anestezi.Name = model.Anestezi_Name;
            _context.Anesteziler.Add(anestezi);
            _context.SaveChanges();
            return RedirectToAction("DuzenleNesneler", "Ameliyat");

        }
        [HttpPost]
        public IActionResult SilAnestezi(AnesteziViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Ameliyat");
            }

            Anestezi anestezi = _context.Anesteziler.FirstOrDefault(x => x.Name == model.Anestezi_Name);
            _context.Anesteziler.Remove(anestezi);
            _context.SaveChanges();
            return RedirectToAction("DuzenleNesneler", "Ameliyat");

        }

        [HttpPost]
        public IActionResult EkleGun(GunViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Ameliyat");
            }


            DayOfWeek Gun = DayOfWeek.Monday;
            if (model.Gun_Name == "pazartesi")
            { Gun = DayOfWeek.Monday; }
            if (model.Gun_Name == "Salı")
            { Gun = DayOfWeek.Tuesday; }
            if (model.Gun_Name == "Çarşamba")
            { Gun = DayOfWeek.Wednesday; }
            if (model.Gun_Name == "Perşembe")
            { Gun = DayOfWeek.Thursday; }
            if (model.Gun_Name == "Cuma")
            { Gun = DayOfWeek.Friday; }


            AmeliyatGun ameliyatGun = new AmeliyatGun();
            ameliyatGun.Gun = Gun;
            ameliyatGun.GunTurkce = model.Gun_Name;
            _context.AmeliyatGunleri.Add(ameliyatGun);
            _context.SaveChanges();
            return RedirectToAction("DuzenleNesneler", "Ameliyat");

        }
        [HttpPost]
        public IActionResult SilGun(GunViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", "Ameliyat");
            }


            DayOfWeek Gun = DayOfWeek.Monday;
            if (model.Gun_Name == "pazartesi")
            { Gun = DayOfWeek.Monday; }
            if (model.Gun_Name == "Salı")
            { Gun = DayOfWeek.Tuesday; }
            if (model.Gun_Name == "Çarşamba")
            { Gun = DayOfWeek.Wednesday; }
            if (model.Gun_Name == "Perşembe")
            { Gun = DayOfWeek.Thursday; }
            if (model.Gun_Name == "Cuma")
            { Gun = DayOfWeek.Friday; }

            var silinecekameliyatlar = _context.Zamanlar.Where(x => x.AmeliyatGünü.DayOfWeek == Gun).ToList();

            foreach (var item in silinecekameliyatlar)
            {
                _context.Zamanlar.Remove(item);
            }
            _context.SaveChanges();



            AmeliyatGun ameliyatGun = _context.AmeliyatGunleri.FirstOrDefault(x => x.GunTurkce == model.Gun_Name);
            _context.AmeliyatGunleri.Remove(ameliyatGun);
            _context.SaveChanges();
            return RedirectToAction("DuzenleNesneler", "Ameliyat");

        }

    }
}