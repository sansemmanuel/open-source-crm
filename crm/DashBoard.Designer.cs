namespace TPFinalNivel2_Sansberro
{
    partial class DashBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashBoard));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblActivemembers = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.btnPlans = new FontAwesome.Sharp.IconButton();
            this.btnEmployee = new FontAwesome.Sharp.IconButton();
            this.btnLeads = new FontAwesome.Sharp.IconButton();
            this.btnBilling = new FontAwesome.Sharp.IconButton();
            this.btnReport = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.PictureBox();
            this.btnMembers = new FontAwesome.Sharp.IconButton();
            this.panelDashboard = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.MintCream;
            this.groupBox1.Controls.Add(this.lblActivemembers);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(52, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 66);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Active Members";
            // 
            // lblActivemembers
            // 
            this.lblActivemembers.AutoSize = true;
            this.lblActivemembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.25F);
            this.lblActivemembers.Location = new System.Drawing.Point(20, 16);
            this.lblActivemembers.Name = "lblActivemembers";
            this.lblActivemembers.Size = new System.Drawing.Size(28, 39);
            this.lblActivemembers.TabIndex = 0;
            this.lblActivemembers.Text = "-";
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeft.AutoSize = true;
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelLeft.Controls.Add(this.btnPlans);
            this.panelLeft.Controls.Add(this.btnEmployee);
            this.panelLeft.Controls.Add(this.btnLeads);
            this.panelLeft.Controls.Add(this.btnBilling);
            this.panelLeft.Controls.Add(this.btnReport);
            this.panelLeft.Controls.Add(this.panelLogo);
            this.panelLeft.Controls.Add(this.btnMembers);
            this.panelLeft.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(203, 717);
            this.panelLeft.TabIndex = 7;
            // 
            // btnPlans
            // 
            this.btnPlans.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPlans.FlatAppearance.BorderSize = 0;
            this.btnPlans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlans.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlans.ForeColor = System.Drawing.Color.LightGray;
            this.btnPlans.IconChar = FontAwesome.Sharp.IconChar.Explosion;
            this.btnPlans.IconColor = System.Drawing.Color.Gainsboro;
            this.btnPlans.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPlans.IconSize = 55;
            this.btnPlans.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlans.Location = new System.Drawing.Point(0, 417);
            this.btnPlans.Name = "btnPlans";
            this.btnPlans.Size = new System.Drawing.Size(200, 55);
            this.btnPlans.TabIndex = 10;
            this.btnPlans.Text = "Memberships";
            this.btnPlans.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlans.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPlans.UseVisualStyleBackColor = true;
            this.btnPlans.Click += new System.EventHandler(this.btnPlans_Click);
            // 
            // btnEmployee
            // 
            this.btnEmployee.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEmployee.FlatAppearance.BorderSize = 0;
            this.btnEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmployee.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmployee.ForeColor = System.Drawing.Color.LightGray;
            this.btnEmployee.IconChar = FontAwesome.Sharp.IconChar.Hammer;
            this.btnEmployee.IconColor = System.Drawing.Color.Gainsboro;
            this.btnEmployee.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEmployee.IconSize = 55;
            this.btnEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmployee.Location = new System.Drawing.Point(0, 356);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(200, 55);
            this.btnEmployee.TabIndex = 9;
            this.btnEmployee.Text = "Employees";
            this.btnEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmployee.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEmployee.UseVisualStyleBackColor = true;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // btnLeads
            // 
            this.btnLeads.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLeads.FlatAppearance.BorderSize = 0;
            this.btnLeads.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeads.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeads.ForeColor = System.Drawing.Color.LightGray;
            this.btnLeads.IconChar = FontAwesome.Sharp.IconChar.CommentAlt;
            this.btnLeads.IconColor = System.Drawing.Color.Gainsboro;
            this.btnLeads.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLeads.IconSize = 55;
            this.btnLeads.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLeads.Location = new System.Drawing.Point(0, 295);
            this.btnLeads.Name = "btnLeads";
            this.btnLeads.Size = new System.Drawing.Size(200, 55);
            this.btnLeads.TabIndex = 8;
            this.btnLeads.Text = "Leads";
            this.btnLeads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLeads.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLeads.UseVisualStyleBackColor = true;
            this.btnLeads.Click += new System.EventHandler(this.btnLeads_Click);
            // 
            // btnBilling
            // 
            this.btnBilling.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBilling.FlatAppearance.BorderSize = 0;
            this.btnBilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBilling.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBilling.ForeColor = System.Drawing.Color.LightGray;
            this.btnBilling.IconChar = FontAwesome.Sharp.IconChar.MoneyBillTransfer;
            this.btnBilling.IconColor = System.Drawing.Color.Gainsboro;
            this.btnBilling.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBilling.IconSize = 55;
            this.btnBilling.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBilling.Location = new System.Drawing.Point(0, 173);
            this.btnBilling.Name = "btnBilling";
            this.btnBilling.Size = new System.Drawing.Size(200, 55);
            this.btnBilling.TabIndex = 7;
            this.btnBilling.Text = "Billing";
            this.btnBilling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBilling.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBilling.UseVisualStyleBackColor = true;
            this.btnBilling.Click += new System.EventHandler(this.btnBilling_Click);
            // 
            // btnReport
            // 
            this.btnReport.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.LightGray;
            this.btnReport.IconChar = FontAwesome.Sharp.IconChar.ChartBar;
            this.btnReport.IconColor = System.Drawing.Color.Gainsboro;
            this.btnReport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReport.IconSize = 55;
            this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Location = new System.Drawing.Point(0, 234);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(200, 55);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Reports";
            this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.btnHome);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(203, 100);
            this.panelLogo.TabIndex = 0;
            // 
            // btnHome
            // 
            this.btnHome.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.Image")));
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(198, 106);
            this.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHome.TabIndex = 0;
            this.btnHome.TabStop = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnMembers
            // 
            this.btnMembers.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMembers.FlatAppearance.BorderSize = 0;
            this.btnMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembers.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMembers.ForeColor = System.Drawing.Color.LightGray;
            this.btnMembers.IconChar = FontAwesome.Sharp.IconChar.PeopleRoof;
            this.btnMembers.IconColor = System.Drawing.Color.Gainsboro;
            this.btnMembers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMembers.IconSize = 50;
            this.btnMembers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembers.Location = new System.Drawing.Point(0, 112);
            this.btnMembers.Name = "btnMembers";
            this.btnMembers.Size = new System.Drawing.Size(200, 55);
            this.btnMembers.TabIndex = 5;
            this.btnMembers.Text = "Members";
            this.btnMembers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMembers.UseVisualStyleBackColor = true;
            this.btnMembers.Click += new System.EventHandler(this.btnMembers_Click);
            // 
            // panelDashboard
            // 
            this.panelDashboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDashboard.AutoSize = true;
            this.panelDashboard.Location = new System.Drawing.Point(209, 0);
            this.panelDashboard.Name = "panelDashboard";
            this.panelDashboard.Size = new System.Drawing.Size(1138, 717);
            this.panelDashboard.TabIndex = 13;
            // 
            // DashBoard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1350, 715);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelDashboard);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DashBoard";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DashBoard_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblActivemembers;
        private FontAwesome.Sharp.IconButton btnMembers;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelLogo;
        private FontAwesome.Sharp.IconButton btnPlans;
        private FontAwesome.Sharp.IconButton btnEmployee;
        private FontAwesome.Sharp.IconButton btnLeads;
        private FontAwesome.Sharp.IconButton btnBilling;
        private FontAwesome.Sharp.IconButton btnReport;
        private System.Windows.Forms.PictureBox btnHome;
        private System.Windows.Forms.Panel panelDashboard;
    }
}