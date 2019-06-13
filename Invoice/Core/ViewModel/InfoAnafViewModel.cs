using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.ViewModel
{
    public class InfoAnafViewModel
    {
        //Info needed for the request
        [Display(Name = "Cui")]
        public int cui { get; set; }

        [Display(Name = "Data")]
        public string data { get; set; }
    }
}
