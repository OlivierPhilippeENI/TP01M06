using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TP01M06.Persistance
{
    public class Context : DbContext
    {
        public Context() : base("name=Context"){}

        public System.Data.Entity.DbSet<BO.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<BO.Arme> Armes {
            get; set;

        }
    }