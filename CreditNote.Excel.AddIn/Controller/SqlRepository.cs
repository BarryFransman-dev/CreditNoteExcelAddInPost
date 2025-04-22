

using CreditNote.ExcelAddIn.Models;
using System;
using System.Linq;

namespace CreditNote.Repository
{
    public class SqlRepository
    {
        public string GetCompanyDB(string letter)
        {
            var comp = string.Empty;

            using (var context = new SysprodbContext())
            {
                comp = context.SysproAdmin.Where(x => x.Company.Trim() == letter).Select(y => y.DatabaseName.Trim()).FirstOrDefault();
            }
            return comp;
        }


    }
}
