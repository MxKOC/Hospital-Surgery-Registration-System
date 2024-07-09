using System;
using System.Collections.Generic;
using System.Linq;
using AmeliyatDefteri.Entity;
using AmeliyatDefteri.Models;
using Microsoft.EntityFrameworkCore;

namespace AmeliyatDefteri.Services{

public class StatisticsService : IStatisticsService
{
    private readonly DataContext _context;

    public StatisticsService(DataContext context)
    {
        _context = context;
    }

    public List<DayOfWeek> GetAllowedDays()
    {
        return _context.AmeliyatGunleri.Select(x => x.Gun).ToList();
    }

    public List<string> GetAmeliyatList()
    {
        return _context.Ameliyatlar.Select(x => x.Name).ToList();
    }

    public List<string> GetDoktorList()
    {
        return _context.Doktorlar.Select(x => x.Name).ToList();
    }

    public List<string> GetAnesteziList()
    {
        return _context.Anesteziler.Select(x => x.Name).ToList();
    }

    public List<DateTime> GetHedefGunTarihleri(List<DayOfWeek> gunler, DateTime startDate, int daysCount)
    {
        List<DateTime> hedefGunTarihleri = new List<DateTime>();

        for (int i = 0; i < daysCount; i++)
        {
            DateTime gunTarihi = startDate.AddDays(i);

            if (gunler.Contains(gunTarihi.DayOfWeek))
            {
                hedefGunTarihleri.Add(gunTarihi);
            }
        }

        return hedefGunTarihleri;
    }

    public List<List<int>> GetAmeliyatGunTopluListe(List<DateTime> hedefGunTarihleri, List<int> ameliyatListesiIdler)
    {
        var Sonuçlar = _context.Zamanlar.Include(x => x.Ameliyat).Include(x => x.Doktor).ToList();
        List<List<int>> ameliyatgunutopluliste = new List<List<int>>();

        foreach (var gun in hedefGunTarihleri)
        {
            List<int> ameliyatgunuliste = new List<int>();
            foreach (var item in ameliyatListesiIdler)
            {
                int count = Sonuçlar.Count(x => x.AmeliyatId == item && x.AmeliyatGünü == gun);
                ameliyatgunuliste.Add(count);
            }
            ameliyatgunutopluliste.Add(ameliyatgunuliste);
        }

        return ameliyatgunutopluliste;
    }

    public List<List<int>> GetDoktorGunTopluListe(List<DateTime> hedefGunTarihleri, List<int> doktorListesiIdler)
    {
        var Sonuçlar = _context.Zamanlar.Include(x => x.Ameliyat).Include(x => x.Doktor).ToList();
        List<List<int>> doktorgunutopluliste = new List<List<int>>();

        foreach (var gun in hedefGunTarihleri)
        {
            List<int> doktortgunuliste = new List<int>();
            foreach (var item in doktorListesiIdler)
            {
                int count = Sonuçlar.Count(x => x.DoktorId == item && x.AmeliyatGünü == gun);
                doktortgunuliste.Add(count);
            }
            doktorgunutopluliste.Add(doktortgunuliste);
        }

        return doktorgunutopluliste;
    }

    public List<List<int>> GetDoktorAnesteziGunTopluListe(List<DateTime> hedefGunTarihleri, List<int> anesteziListesibilgiler)
    {
        var Sonuçlar = _context.Zamanlar.Include(x => x.Ameliyat).Include(x => x.Doktor).Include(x => x.Anestezi).ToList();
        List<List<int>> doktoranestezigunutopluliste = new List<List<int>>();

        foreach (var gun in hedefGunTarihleri)
        {
            List<int> doktortanestezigunuliste = new List<int>();
            foreach (var item in anesteziListesibilgiler)
            {
                int count = Sonuçlar.Count(x => x.AnesteziId == item && x.AmeliyatGünü == gun);
                doktortanestezigunuliste.Add(count);
            }
            doktoranestezigunutopluliste.Add(doktortanestezigunuliste);
        }

        return doktoranestezigunutopluliste;
    }

    public List<int> GetAmeliyatGunListe(IstatistikDokAmeliyatViewModel model, List<DateTime> hedefGunTarihleri)
    {
        var Sonuçlar = _context.Zamanlar.Include(x => x.Ameliyat).Include(x => x.Doktor).ToList();
        List<int> ameliyatgunuliste = new List<int>();

        foreach (var gun in hedefGunTarihleri)
        {
            int count = Sonuçlar.Count(x => x.Ameliyat.Name == model.Ameliyat_Name && x.Doktor.Name == model.Doktor_Name && x.AmeliyatGünü == gun);
            ameliyatgunuliste.Add(count);
        }

        return ameliyatgunuliste;
    }

    public List<int> GetDoktorAnesteziGunListe(IstatistikDokAnesteziViewModel model, List<DateTime> hedefGunTarihleri)
    {
        var Sonuçlar = _context.Zamanlar.Include(x => x.Ameliyat).Include(x => x.Doktor).Include(x => x.Anestezi).ToList();
        List<int> ameliyatgunuliste = new List<int>();

        foreach (var gun in hedefGunTarihleri)
        {
            int count = Sonuçlar.Count(x => x.Anestezi.Name == model.Anestezi_Name && x.Doktor.Name == model.Doktor_Name && x.AmeliyatGünü == gun);
            ameliyatgunuliste.Add(count);
        }

        return ameliyatgunuliste;
    }

    public List<DateTime> GetHastaTarih(IstatistikHastaViewModel model)
    {
        var Sonuçlar = _context.Zamanlar.Include(x => x.Ameliyat).Include(x => x.Doktor).ToList();
        return Sonuçlar.Where(x => x.Name == model.Hasta_Name).Select(x => x.AmeliyatGünü).ToList();
    }

        
    }
}