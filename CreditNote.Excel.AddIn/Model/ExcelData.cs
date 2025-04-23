using SigmaCape.Business.Syspro.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditNote.ExcelAddIn.Models
{
    public class ExcelData
    {
        public string StockCode { get; set; }

        public string Description { get; set; }

        public string CreditReason { get; set; }

        public string ProductClass { get; set; }

        public string TaxCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
