using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmeliyatDefteri.Models;

namespace AmeliyatDefteri.Services
{
    public interface IStatisticsService
    {
        List<DayOfWeek> GetAllowedDays();
        List<string> GetAmeliyatList();
        List<string> GetDoktorList();
        List<string> GetAnesteziList();
        List<DateTime> GetHedefGunTarihleri(List<DayOfWeek> gunler, DateTime startDate, int daysCount);
        List<List<int>> GetAmeliyatGunTopluListe(List<DateTime> hedefGunTarihleri, List<int> ameliyatListesiIdler);
        List<List<int>> GetDoktorGunTopluListe(List<DateTime> hedefGunTarihleri, List<int> doktorListesiIdler);
        List<List<int>> GetDoktorAnesteziGunTopluListe(List<DateTime> hedefGunTarihleri, List<int> anesteziListesibilgiler);
        List<int> GetAmeliyatGunListe(IstatistikDokAmeliyatViewModel model, List<DateTime> hedefGunTarihleri);
        List<int> GetDoktorAnesteziGunListe(IstatistikDokAnesteziViewModel model, List<DateTime> hedefGunTarihleri);
        List<DateTime> GetHastaTarih(IstatistikHastaViewModel model);
    }
}