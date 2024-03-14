using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmeliyatDefteri.Models
{
    public class DuzenleButunNesnelerViewModel
    {
        public DoktorViewModel DoktorViewModel { get; set; }
        public AmeliyatViewModel AmeliyatViewModel { get; set; }
        public AnesteziViewModel AnesteziViewModel { get; set; }
        public GunViewModel GunViewModel { get; set; }
    }

    public class DoktorViewModel
    {
        public string Doktor_Name { get; set; }
    }

    public class AmeliyatViewModel
    {
        public string Ameliyat_Name { get; set; }
    }

    public class AnesteziViewModel
    {
        public string Anestezi_Name { get; set; }
    }


    public class GunViewModel
    {
        public string Gun_Name { get; set; }

    }

}