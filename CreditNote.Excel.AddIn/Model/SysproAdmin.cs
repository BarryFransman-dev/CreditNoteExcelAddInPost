namespace CreditNote.ExcelAddIn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysproAdmin")]
    public partial class SysproAdmin
    {
        [Key]
        [StringLength(4)]
        public string Company { get; set; }

        [Required]
        [StringLength(20)]
        public string DatabaseName { get; set; }

        [StringLength(255)]
        public string CollationName { get; set; }
    }
}
