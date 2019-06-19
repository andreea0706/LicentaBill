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
        public string cui { get; set; }

        [Required]
        [Display(Name = "Nume")]
        public string Name { get; set; }

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

        [Required]
        [Display(Name = "Numar de telefon")]
        public string Phone { get; set; }

        [Display(Name = "Detalii")]
        public string Notes { get; set; }

    }
}
