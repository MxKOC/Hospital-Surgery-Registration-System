using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmeliyatDefteri.Entity
{
    public class AmeliyatGun
    {
        public int Id { get; set; }
        public string GunTurkce { get; set; }
        public DayOfWeek Gun { get; set; }
    }
}