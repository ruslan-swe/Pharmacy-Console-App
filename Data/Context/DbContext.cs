using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public static class DbContext
    {
        static DbContext()
        {
            Admins = new List<Admin>();
            Druggists = new List<Druggist>();
            Drugs = new List<Drug>();
            Owners = new List<Owner>();
            DrugStores = new List<DrugStore>();
        }
        public static List<Admin> Admins { get; set; }
        public static List<Druggist> Druggists { get; set; }
        public static List<Drug> Drugs { get; set; }
        public static List<Owner> Owners { get; set; }
        public static List<DrugStore> DrugStores { get; set; }
    }
}
