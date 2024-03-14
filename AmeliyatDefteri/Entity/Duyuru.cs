using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmeliyatDefteri.Entity
{
    public class Duyuru
    {
        public int Id { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public String? Icerik { get; set; }

    }
}