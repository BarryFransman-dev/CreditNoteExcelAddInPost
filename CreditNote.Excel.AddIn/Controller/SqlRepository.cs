

using CreditNote.ExcelAddIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public IEnumerable<T> GetInfo<T>() where T : class
        {
            var recs = new List<T>();
            using (var context = new SysproContext("FT"))
            {
                var item = context.Set<T>().ToList();
                if (item.Count() > 0)
                {
                    recs.AddRange(item);
                }
            }
            return recs;
        }

        public IEnumerable<T> Get<T>(string company, Expression<Func<T, bool>> filter = null) where T : class
        {
            IQueryable<T> query = new SysproContext(company).Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();

        }
    }
}
