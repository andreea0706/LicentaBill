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
    [Table("Nir")]
    public class NirModel : BaseModel
    {
        [Required]
        [DisplayName("Data")]
        public DateTime NirDate { get; set; }
        public string NirCode { get; set; }
        public string Notes { get; set; }
        [Required]
        public Double Total { get; set; }
        public string Status { get; set; }
        public double? Discount { get; set; }
        public double GrandTotal { get; set; }

        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; }

        [DisplayName("SupplierId")]
        [ForeignKey("SupplierModel")]
        public int SupplierId { get; set; }
        public SupplierModel SupplierModel { get; set; }

        public ICollection<NirItemsModel> Items { get; set; }
        public NirModel()
        {
            Items = new List<NirItemsModel>();
        }
    }
}
