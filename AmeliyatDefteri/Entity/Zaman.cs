using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmeliyatDefteri.Entity
{
    public class Zaman
    {
        public int Id { get; set; }


        public String Name { get; set; } = null!;
        public String? Telefon { get; set; }
        public String? Detay { get; set; }
        public DateTime AmeliyatGünü { get; set; }



        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; } = null!;

        public int AmeliyatId { get; set; }
        public Ameliyat Ameliyat { get; set; } = null!;

        public int AnesteziId { get; set; }
        public Anestezi Anestezi { get; set; } = null!;


    }
}