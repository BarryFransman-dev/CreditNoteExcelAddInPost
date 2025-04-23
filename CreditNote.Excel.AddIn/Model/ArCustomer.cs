using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditNote.Excel.AddIn.Model
{
    [Table("ArCustomer")]
    public partial class ArCustomer
    {
        [Key]
        public string Customer { get; set; }
        public string Name { get; set; }
    }
}
