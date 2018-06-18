using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP8 {
    [Table("Company")]
    public class Company {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public ICollection<Phone> Phones { get; set; }
        public Company() {
            Phones = new List<Phone>();
        }
    }
    [Table("Phone")]
    public class Phone {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }

        public int CompanyId { get; set; }
        public Company CompanyObj { get; set; }
    }
}
