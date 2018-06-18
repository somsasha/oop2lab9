using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP8 {
    public class PhoneContext : DbContext {
        public PhoneContext() : base("DbConnection") { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
    //class MyContextInitializer : DropCreateDatabaseAlways<PhoneContext> {
    //    protected override void Seed(PhoneContext db) {
    //       
    //        db.Companies.Add(c1);
    //        db.Companies.Add(c2);

    //        db.SaveChanges();

    //       

    //        db.Phones.AddRange(new List<Phone>() { p1, p2, p3, p4, p5 });
    //        db.SaveChanges();
    //    }
    //}
}
