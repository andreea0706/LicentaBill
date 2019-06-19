using Invoice.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.ViewModel
{
    public class ShowCustomerViewModel
    {
       public IEnumerable<CustomerModel> Customers { get; set; }
       public CustomerModel Customer { get; set; }
    }
}
