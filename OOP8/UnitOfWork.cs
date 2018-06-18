using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP8 {
    public class UnitOfWork : IDisposable {
        public PhoneContext context = new PhoneContext();
        private GenericRepository<Phone> phoneRepository;
        private GenericRepository<Company> companyRepository;

        public GenericRepository<Phone> PhoneRepository {
            get {
                if (phoneRepository == null) {
                    phoneRepository = new GenericRepository<Phone>(context);
                }
                return phoneRepository;
            }
        }

        public GenericRepository<Company> CompanyRepository {
            get {
                if (companyRepository == null) {
                    companyRepository = new GenericRepository<Company>(context);
                }
                return companyRepository;
            }
        }

        public void Save() {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
