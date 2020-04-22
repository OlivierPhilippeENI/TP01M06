using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;

namespace TP01M06.Models
{
    public class SamouraiViewModel
    {
        public Samourai Samourai { get; set; }
        public int? IdArmeDisponible { get; set; }
        public List<Arme> Armes { get; set; }

    }
}