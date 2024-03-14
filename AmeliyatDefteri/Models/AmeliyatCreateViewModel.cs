using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmeliyatDefteri.Entity;

namespace AmeliyatDefteri.Models
{
    public class AmeliyatCreateViewModel
    {

        public String Kullanici { get; set; } = null!;

        public String Doktor_Name { get; set; } = null!;

        //Ameliyat
        public String Ameliyat_Name { get; set; } = null!;
        public String Anestezi_Name { get; set; } = null!;

        //Hasta
        public String Hasta_Name { get; set; } = null!;
        public String Telefon { get; set; } = null!;
        public String Detay { get; set; } = null!;

        public DateTime AmeliyatGünü { get; set; }

    }






    public class AmeliyatChangeViewModel
    {

        public int id { get; set; }
        public String Kullanici { get; set; } = null!;
        //Doktor
        public String Doktor_Name { get; set; } = null!;

        //Ameliyat
        public String Ameliyat_Name { get; set; } = null!;
        public String Anestezi_Name { get; set; } = null!;

        //Hasta
        public String Hasta_Name { get; set; } = null!;
        public String Telefon { get; set; } = null!;
        public String Detay { get; set; } = null!;

        public DateTime AmeliyatGünü { get; set; }

        public bool isDelete { get; set; }

    }




    public class IndexViewModel
    {
        public DateTime time { get; set; }

    }




}