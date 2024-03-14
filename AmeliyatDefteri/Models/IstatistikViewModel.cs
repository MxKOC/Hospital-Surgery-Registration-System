using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmeliyatDefteri.Models
{
    public class IstatistikViewModel
    {
         public IstatistikDokAmeliyatViewModel IstatistikDokAmeliyatViewModel { get; set; }
        public IstatistikDokAnesteziViewModel IstatistikDokAnesteziViewModel { get; set; }
        public IstatistikHastaViewModel IstatistikHastaViewModel { get; set; }

    }




public class IstatistikDokAmeliyatViewModel
    {
        
        public string Ameliyat_Name { get; set; }
        public string Doktor_Name { get; set; }
    }

    public class IstatistikDokAnesteziViewModel
    {
        
        public string Doktor_Name { get; set; }
        public string Anestezi_Name { get; set; }

    }

    public class IstatistikHastaViewModel
    {
        
        public string Hasta_Name { get; set; }
        
    }
}