using CreditNote.ExcelAddIn.Models;
using SigmaCape.Business.Syspro;
using SigmaCape.Business.Syspro.Ar;
using SigmaCape.Business.Syspro.Po;
using SigmaCape.Business.Syspro.Results;
using SigmaCape.Business.Syspro.Shared;
using SigmaCape.Business.Syspro.So;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditNote.Repository
{

    public class SysproRepository
    {
        #region Credit Note Routines
        public (CreditNoteHeaderResult, CreditNoteLineResult, string)  PostCreditNote(string Customer, string CustomerPO, List<ExcelData> GroupedData, SysproIdentity identity)
        {
            //SORTCH
            var cnHeader = new CreditNoteHeader();
            var cnHeaderItem = new CrOrderHeader();
            var cnDetail = new CreditNoteLine();
            var cnDetailItem = new CrStockLine();
            var SOCredit = string.Empty;

            cnHeader = new CreditNoteHeader();
            cnHeaderItem = new CrOrderHeader();

            cnHeaderItem.Customer = Customer;
            cnHeaderItem.CustomerPoNumber = CustomerPO;
            var fdt = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            cnHeaderItem.CreditNoteDate = Convert.ToDateTime(fdt);

            cnHeader.Items.Add(cnHeaderItem);

            var cnHeadeSerial = cnHeader.Serialize();
            var crHeadResult = cnHeader.Post(GetCreditNoteParam(), identity);

            if (crHeadResult.HasErrors)
            {
                return (crHeadResult, null, string.Empty);
            }

            SOCredit = crHeadResult.Items.FirstOrDefault().CreditNote;
            //SORTCL
            foreach (var item in GroupedData)
            {
                cnDetailItem = new CrStockLine();
                cnDetailItem.CreditNote = crHeadResult.Items.FirstOrDefault().CreditNote;
                cnDetailItem.NonStockedLine = SysproBoolean.Y;
                cnDetailItem.StockCode = item.StockCode;
                cnDetailItem.StockDescription = item.Description;
                cnDetailItem.OrderUom = "EA";
                cnDetailItem.PriceUom = "EA";
                cnDetailItem.CreditReason = item.CreditReason;
                cnDetailItem.NsProductClass = item.ProductClass;
                cnDetailItem.TaxCode = item.TaxCode;
                cnDetailItem.OrderQty = item.Quantity;
                cnDetailItem.Price = item.Price;
                cnDetailItem.OverrideCalculatedDiscount = SysproBoolean.Y;
                cnDetailItem.DiscValFlag = DiscValFlag.U;
                cnDetail.Items.Add(cnDetailItem);
            }

            var cnDetSerial = cnDetail.Serialize();
            var crDetResult = cnDetail.Post(GetCreditNoteParam(), identity);

            return (null, crDetResult, SOCredit);
        }

        public CreditNoteParameters GetCreditNoteParam()
        {
            return new CreditNoteParameters()
            {
                IgnoreWarnings = SysproBoolean.Y,
                ValidateOnly = SysproBoolean.N,
                ApplyIfEntireDocumentValid = SysproBoolean.Y
            };
        }

        #endregion
    }
}
