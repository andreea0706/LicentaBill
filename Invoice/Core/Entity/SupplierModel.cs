﻿using Invoice.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.Entity
{
    [Table("Supplier")]
    public class SupplierModel : BaseModel
    {
        [Required]
        [Display(Name = "Nume")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Numar de telefon")]
        public string Phone { get; set; }

        [Display(Name = "Adresa")]
        public string Address { get; set; }

        [Display(Name = "Detalii")]
        public string Notes { get; set; }
     
        [Display(Name = "Nume Firma")]
        public string FirmName { get; set; }

   

    }
}
