namespace CreditNote.ExcelAddIn.Models
{
    using CreditNote.Repository;
    using SigmaCape.Business.Syspro;
    using System.Data.Entity;

    /// <summary>
    /// The syspro context.
    /// </summary>
    public partial class SysproContext : DbContext
    {
        #region Constructors and Destructors

        static SysproContext()
        {
            Database.SetInitializer<SysproContext>(null);
        }

        public SysproContext(SysproIdentity sysIdentity)
            : base(new SqlRepository().GetCompanyDB(sysIdentity.Profile.Company))
        //: base(company == "A" ? "EncoreCompany" + company : "SysproCompany" + company)
        {
        }
        public SysproContext(string Company)
            : base(new SqlRepository().GetCompanyDB(Company))
        {
        }

        #endregion

        #region Public Properties



        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        #endregion
    }
}