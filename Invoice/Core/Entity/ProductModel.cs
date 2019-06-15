using Invoice.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.Entity
{
    [Table("Product")]
    public class ProductModel : BaseModel
    {
        [Required]
        [DisplayName("Nume Produs")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Cod Produs")]
        public string Code { get; set; }

        [Required]
        [DisplayName("Pret")]
        public double Price { get; set; }

        [DisplayName("Descriere")]
        public string Description{ get; set; }

    }
}
