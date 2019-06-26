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
    [Table("NirItems")]
    public class NirItemsModel : BaseModel
    {
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Amount { get; set; }

        public int QuantityReceived { get; set; }

        [DisplayName("Nir")]
        [ForeignKey("NirModel")]
        public int NirId { get; set; }
        public NirModel NirModel { get; set; }
    }
}
