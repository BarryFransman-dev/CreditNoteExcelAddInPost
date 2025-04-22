

namespace CreditNote.Excel.AddIn
{
    partial class CreditNote : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CreditNote()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditNote));
            this.CredNote = this.Factory.CreateRibbonTab();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.button2 = this.Factory.CreateRibbonButton();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.button4 = this.Factory.CreateRibbonButton();
            this.lblUser = this.Factory.CreateRibbonLabel();
            this.lblCompany = this.Factory.CreateRibbonLabel();
            this.grpCustomForm = this.Factory.CreateRibbonGroup();
            this.btPostSyspro = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnComment = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btnRefresh = this.Factory.CreateRibbonButton();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.ebSO = this.Factory.CreateRibbonEditBox();
            this.CredNote.SuspendLayout();
            this.group3.SuspendLayout();
            this.grpCustomForm.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.group4.SuspendLayout();
            this.SuspendLayout();
            // 
            // CredNote
            // 
            this.CredNote.Groups.Add(this.group3);
            this.CredNote.Groups.Add(this.grpCustomForm);
            this.CredNote.Groups.Add(this.group1);
            this.CredNote.Groups.Add(this.group2);
            this.CredNote.Groups.Add(this.group4);
            this.CredNote.Label = "Credit Notes";
            this.CredNote.Name = "CredNote";
            // 
            // group3
            // 
            this.group3.Items.Add(this.button2);
            this.group3.Items.Add(this.separator2);
            this.group3.Items.Add(this.button4);
            this.group3.Items.Add(this.lblUser);
            this.group3.Items.Add(this.lblCompany);
            this.group3.Label = "Login";
            this.group3.Name = "group3";
            // 
            // button2
            // 
            this.button2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Label = "Syspro Log In";
            this.button2.Name = "button2";
            this.button2.ScreenTip = "Log in to Syspro";
            this.button2.ShowImage = true;
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLogin_Click);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Label = " ";
            this.button4.Name = "button4";
            this.button4.ShowImage = true;
            // 
            // lblUser
            // 
            this.lblUser.Label = "User: ";
            this.lblUser.Name = "lblUser";
            // 
            // lblCompany
            // 
            this.lblCompany.Label = "Company: ";
            this.lblCompany.Name = "lblCompany";
            // 
            // grpCustomForm
            // 
            this.grpCustomForm.Items.Add(this.btPostSyspro);
            this.grpCustomForm.Label = "Post";
            this.grpCustomForm.Name = "grpCustomForm";
            // 
            // btPostSyspro
            // 
            this.btPostSyspro.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btPostSyspro.Image = ((System.Drawing.Image)(resources.GetObject("btPostSyspro.Image")));
            this.btPostSyspro.Label = "Post Transactions";
            this.btPostSyspro.Name = "btPostSyspro";
            this.btPostSyspro.ShowImage = true;
            this.btPostSyspro.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPostSyspro_Click);
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnComment);
            this.group1.Label = "Instructions";
            this.group1.Name = "group1";
            // 
            // btnComment
            // 
            this.btnComment.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnComment.Image = ((System.Drawing.Image)(resources.GetObject("btnComment.Image")));
            this.btnComment.Label = "Create Credit Notes";
            this.btnComment.Name = "btnComment";
            this.btnComment.ShowImage = true;
            // 
            // group2
            // 
            this.group2.Items.Add(this.btnRefresh);
            this.group2.Label = "Data";
            this.group2.Name = "group2";
            // 
            // btnRefresh
            // 
            this.btnRefresh.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Label = "REFRESH DATA";
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ShowImage = true;
            this.btnRefresh.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRefresh_Click);
            // 
            // group4
            // 
            this.group4.Items.Add(this.ebSO);
            this.group4.Label = "Result";
            this.group4.Name = "group4";
            // 
            // ebSO
            // 
            this.ebSO.Label = "CN";
            this.ebSO.Name = "ebSO";
            this.ebSO.Text = null;
            // 
            // CreditNote
            // 
            this.Name = "CreditNote";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.CredNote);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.PO_SO_Load);
            this.CredNote.ResumeLayout(false);
            this.CredNote.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.grpCustomForm.ResumeLayout(false);
            this.grpCustomForm.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab CredNote;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        private Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        private Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        private Microsoft.Office.Tools.Ribbon.RibbonButton button4;
        private Microsoft.Office.Tools.Ribbon.RibbonLabel lblUser;
        private Microsoft.Office.Tools.Ribbon.RibbonLabel lblCompany;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpCustomForm;
        private Microsoft.Office.Tools.Ribbon.RibbonButton btPostSyspro;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnComment;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRefresh;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox ebSO;
    }

    partial class ThisRibbonCollection
    {
        internal CreditNote CreditNoteTab
        {
            get { return this.GetRibbon<CreditNote>(); }
        }
    }
}
