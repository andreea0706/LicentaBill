using Invoice.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.ViewModel
{
    public class NirReportViewModel
    {
        public StoreSettingModel company { get; set; }
        public NirModel Nirs { get; set; }
    }
}
