using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DrugStore : BaseEntity
    {
        public DrugStore()
        {
            Druggists = new List<Druggist>();
            Drugs = new List<Drug>();
            BuyList = new List<Drug>();
            BuyAmount  = new List<int>{};
        }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<Druggist> Druggists { get; set; }
        public List<Drug> Drugs { get; set; }
        public List<Drug> BuyList { get; set; }
        public List<int> BuyAmount { get; set; }
        public Owner Owner { get; set; }
    }
}
