using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.ViewModel
{
    public class ReturnedInfoAnafViewModel
    {
       
        [Display(Name = "CUI")]
        public int cui { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirma parola")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Adresa")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Nume Firma")]
        public string FirmName { get; set; }

        [Display(Name = "Platitor in scopuri TVA")]
        public Boolean scpTVA { get; set; }

        [Display(Name = "Mesaj Anaf")]
        public string mesaj_ScpTVA { get; set; }

        [Display(Name = "Tip activitate TVA la incasare")]
        public string tipActTvaInc { get; set; }

        [Display(Name = "Platitor TVA la incasare")]
        public Boolean statusTvaIncasare { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Contribuabil inactiv?")]
        public Boolean statusInactivi { get; set; }

        [Display(Name = "Status split TVA")]
        public Boolean statusSplitTVA { get; set; }
    }
}
