using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmeliyatDefteri.Entity
{
    public class Gecmis
    {
        public int Id { get; set; }
        public string Kullanici { get; set; } = null!;
        public DateTime IslemTarihi { get; set; }
        public string Olay { get; set; } = null!;

        public String Name { get; set; } = null!;
        public String? Telefon { get; set; }
        public String? Detay { get; set; }
        public DateTime AmeliyatGünü { get; set; }

        public string Ameliyat { get; set; } = null!;
        public string Doktor { get; set; } = null!;
        public string Anestezi { get; set; } = null!;



    }
}