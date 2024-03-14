using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmeliyatDefteri.Models
{

public class DuyuruViewModel
    {
        public EkleDuyuruViewModel EkleDuyuruViewModel { get; set; }
        public SilDuyuruViewModel SilDuyuruViewModel { get; set; }
        
    }

    public class EkleDuyuruViewModel
    {
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public String? Icerik { get; set; }
    }

    public class SilDuyuruViewModel
    {
        public String? Icerik { get; set; }
    }

}