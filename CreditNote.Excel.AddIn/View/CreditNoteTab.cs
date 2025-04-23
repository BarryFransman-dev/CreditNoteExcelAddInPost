using CreditNote.Excel.AddIn;
using CreditNote.Excel.AddIn.Model;
using CreditNote.ExcelAddIn.Models;
using CreditNote.Repository;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using SigmaCape.Business.Syspro;
using SigmaCape.Business.Syspro.UI;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace CreditNote.Excel.AddIn

{
    public partial class CreditNote
    {
        private SysproRepository sysproRepository;
        private SqlRepository sqlRepository;
        private SysproIdentity sysId;

        private Application application;

        private void PO_SO_Load(object sender, RibbonUIEventArgs e)
        {
            application = Globals.ThisWorkbook.Application;
            sysproRepository = new SysproRepository();
            sqlRepository = new SqlRepository();
            group3.Label = "Ver:" + System.Windows.Forms.Application.ProductVersion.ToString();
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var ad = ApplicationDeployment.CurrentDeployment;
                group3.Label = "Ver:" + ad.CurrentVersion.ToString();
            }
        }

        private void btnRefresh_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var wb = Globals.ThisWorkbook;
                var Sh = Globals.ThisWorkbook.ActiveSheet;
                var activeSheet = (Worksheet)Sh;
                var sName = activeSheet.Name;
                activeSheet.Name = "Refreshing...";

                if (MessageBox.Show("This will refresh the data for the whole worksheeet. Continue?", "REFRESH", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                SetCursor("Refreshing data...");

                wb.RefreshAll();

                application.CalculateUntilAsyncQueriesDone();

                activeSheet.Name = sName;

            }
            catch (Exception ex)
            {
                ResetCursor();
                MessageBox.Show(@"Refresh: " + ex.Message);
            }
            ResetCursor();
        }

        private void btnPostSyspro_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var exData = ReadRows();
                if (!(exData.Count() > 0))
                {
                    MessageBox.Show("All records were not valid", "POST", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (sysId == null || !(sysId.IsAuthenticated))
                {
                    MessageBox.Show("Please log in first", "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("OK to post transactions. Continue?", "POST", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                SetCursor("Updating...");
                application.Cursor = XlMousePointer.xlWait;
                application.StatusBar = "Posting transactions...";
                application.ScreenUpdating = false;

                if (exData.Count() > 0)
                {
                    //Create credit note
                    var cnResult = sysproRepository.PostCreditNote(ebCustCode.Text, ebCustPO.Text, exData, sysId);
                    if (cnResult.Item1 != null && cnResult.Item1.HasErrors)
                    {
                        ResetCursor();
                        MessageBox.Show(cnResult.Item1.Errors, "Post Credit Note Header");
                        return;
                    }

                    if (cnResult.Item2.HasErrors)
                    {
                        ResetCursor();
                        MessageBox.Show(cnResult.Item2.Errors, "Post Credit Note Lines");
                        return;
                    }

                    ebSO.Text = cnResult.Item3;
                }

                ResetCursor();
                MessageBox.Show("Posting Complete - Credit Note created.", "POST", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ResetCursor();
                MessageBox.Show(@"PostClick: " + ex.Message);
            }
            finally
            {
                ResetCursor();
            }
        }

        private List<ExcelData> ReadRows()
        {
            var activeSheet = Globals.ThisWorkbook.Application.ActiveSheet;
            if (activeSheet.Name != "CreditNotes")
            {
                MessageBox.Show("Incorrect Sheet. Please select CreditNotes");
                return new List<ExcelData>();
            }
            var ssRange = activeSheet.UsedRange;//.SpecialCells(XlCellType.xlCellTypeVisible);
            //activeSheet.UsedRange.Calculate();
            var leData = new List<ExcelData>();
            var eData = new ExcelData();

            try
            {
                foreach (var area in ssRange.Areas)
                {
                    foreach (Range row in area.Rows)
                    {
                        if (((Range)area[row.Row, 1]).Value2 == null | ((Range)area[row.Row, 1]).Value2 == "Non-Stocked Code")
                        {
                            continue;
                        }
                        if ((area[row.Row, 1].Value).ToString() != "" && ((decimal)area[row.Row, 6].Value) > 0)
                        {
                            eData.StockCode = area[row.Row, 1].Value;
                            eData.Description = area[row.Row, 2].Value;
                            eData.Quantity = (decimal)area[row.Row, 6].Value;
                            eData.Price = (decimal)area[row.Row, 7].Value;
                            eData.CreditReason = area[row.Row, 8].Value;
                            eData.ProductClass = area[row.Row, 9].Value;
                            eData.TaxCode = area[row.Row, 10].Value;

                            leData.Add(eData);
                            eData = new ExcelData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"ReadRows: " + ex.Message);
                leData.Clear();
            }
            return leData;
        }

        private object GetColVal(string colName, Range area, int row)
        {
            var val = area.Find(colName, Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            var retval = val != null ? (object)area[row, val.Column].Value : null;
            return retval;
        }

        private void SetCursor(string displayText)
        {
            application.Cursor = XlMousePointer.xlWait;
            application.StatusBar = displayText;
            application.ScreenUpdating = true;
        }

        private void ResetCursor()
        {
            application.Cursor = XlMousePointer.xlDefault;
            application.StatusBar = null;
            application.ScreenUpdating = true;
        }

        private void btnLogin_Click(object sender, RibbonControlEventArgs e)
        {
            this.SetCursor("Logging in...");
            try
            {
                if (Syspro.ShowLoginDialog() != true)
                {
                    return;
                }

                sysId = SysproIdentity.Current;
                lblUser.Label = "User: " + SysproIdentity.Current.Profile.OperatorName;
                lblCompany.Label = "Company: " + SysproIdentity.Current.Profile.Company;

                var ds = sqlRepository.Get<ArCustomer>(sysId.Profile.Company);

                cbCustomer.Items.Clear();
                cbCustomer.Text = "Loading...";

                foreach (var customer in ds)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    item.Label = $"{customer.Name} | {customer.Customer}";
                    item.Tag = customer.Customer; // Optional: keep Code separate for logic
                    cbCustomer.Items.Add(item);
                }

                cbCustomer.Text = string.Empty;
                ebCustCode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ResetCursor();
                MessageBox.Show(@"LoginClick: " + ex.Message);
            }
            finally
            {
                ResetCursor();
            }
        }

        private void SendEMail(int mailID, string message, string routine, string[,] att = null)
        {
            //CustomEmail.SendMail(mailID, routine + Environment.NewLine + "Detail:" + Environment.NewLine + message);
        }

        private void cbCustomer_TextChanged(object sender, RibbonControlEventArgs e)
        {
            var splitText = cbCustomer.Text.Split('|');
            cbCustomer.Text = splitText[0].Trim();
            ebCustCode.Text = splitText[1].Trim();
        }
    }
}
