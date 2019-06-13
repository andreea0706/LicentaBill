using Invoice.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.Entity
{
    [Table("Users")]
    public class UserModel : BaseModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
