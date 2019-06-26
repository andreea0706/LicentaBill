using Invoice.Core.Entity;
using Invoice.Core.Interfaces;
using Invoice.Data.AppDataContext;
using Invoice.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Data.Repository
{
    public class NirRepository : BaseRepository<NirModel>, INirRepository
    {
        public NirRepository(InvoiceDbContext context) : base(context)
        {
        }
    }
}
