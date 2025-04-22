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

        public decimal? Quantity { get; set; }

        public DateTime DueDate { get; set; }
    }
}
