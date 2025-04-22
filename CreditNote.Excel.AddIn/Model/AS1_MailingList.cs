namespace B2B_PO_to_SO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AS1_MailingList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MailID { get; set; }

        [StringLength(50)]
        public string MailDept { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string ToEmail { get; set; }

        [StringLength(250)]
        public string CCEmail { get; set; }

        [StringLength(250)]
        public string BCCEmail { get; set; }
    }
}
