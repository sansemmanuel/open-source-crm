using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using FontAwesome.Sharp;
using System.Drawing.Drawing2D;

namespace TPFinalNivel2_Sansberro
{
    public partial class DashBoard : Form
    {
        //iu btn
        private Form currentChildForm;
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Button currentButton;


        //  private DashBoard model;


        #region structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        #endregion

        public DashBoard()
        {
            InitializeComponent();
            SetPanelBorderRad();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 55);
            panelLeft.Controls.Add(leftBorderBtn);
            //Default - Last 7 days
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select();
            SetBtnColor(btnLast7Days);
            LoadData();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {

        }

        #region methods

        //SIDE PANEL BUTTONS
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {

                DisableButton();


                // Activa el nuevo botón
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                // Configuración del borde izquierdo del botón
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // Configuración del ícono del formulario hijo actual (si es necesario)
                //iconCurrentChildForm.IconChar = currentBtn.IconChar;
                //iconCurrentChildForm.IconColor = color;


            }
            else
            {
                MessageBox.Show("Sender button is null");
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }




        //DASHBOARD DAYS BUTTONS
        private void SetBtnColor(object button)
        {
            var btn = (Button)button;

            DisableBtnColor();

            // Activa el nuevo botón
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;

            currentButton = btn;
        }

        private void DisableBtnColor()
        {
            if (currentButton != null)
            {
                // Desactiva el botón actual
                currentButton.BackColor = Color.FromArgb(31, 30, 68);
                currentButton.ForeColor = Color.White;
            }
        }




        private void OpenChildForm(Form childForm)
        {
            // close the current child form if it is open
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = childForm;
            // configure child form
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // add child form to the main panel and show it
             panelDashboard.Controls.Add(childForm);
            panelDashboard.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            //iconCurrentChildForm.IconChar = IconChar.Home;
            //iconCurrentChildForm.IconColor = Color.MediumPurple;
            //lblTitleChildForm.Text = "Home";
        }
        private void LoadData()
        {
            MemberBusiness negocio = new MemberBusiness();
            int activemembernum;
            activemembernum = negocio.GetActiveMembersNum();
            var refreshData = negocio.LoadData(dtpStartDate.Value, dtpEndDate.Value);
            if (refreshData == true)
            {
                lblActive.Text = activemembernum.ToString();
                lblIncome.Text = "$" + negocio.Income.ToString();
                lblVisitors.Text = negocio.TotalVisitors.ToString();

                //lblTotalProfit.Text = "$" + model.TotalProfit.ToString();
                lblTranNumber.Text = negocio.TranNumber.ToString();
                //lblNumSuppliers.Text = model.NumSuppliers.ToString();
                //lblNumProducts.Text = model.NumProducts.ToString();
                chartGrossRevenue.DataSource = negocio.GrossRevenueList;
                chartGrossRevenue.Series[0].XValueMember = "Date";
                chartGrossRevenue.Series[0].YValueMembers = "TotalAmount";
                chartGrossRevenue.DataBind();
                charTopHours.DataSource = negocio.TopProductsList;
                charTopHours.Series[0].XValueMember = "Key";
                charTopHours.Series[0].YValueMembers = "Value";
                charTopHours.DataBind();
                dataGridView1.DataSource = negocio.UnderstockList;
                dataGridView1.Columns[0].HeaderText = "Name";
                dataGridView1.Columns[1].HeaderText = "Hour";
                Console.WriteLine("Loaded view :)");
            }
            else Console.WriteLine("View not loaded, same query");
        }
        private void DisableDtp()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOkCustomDate.Visible = false;
        }



        #endregion

        #region events
        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            SetBtnColor(sender);
            DisableDtp();
        }
        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            SetBtnColor(sender);
            DisableDtp();
        }
        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            SetBtnColor(sender);
            DisableDtp();
        }
        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            SetBtnColor(sender);
            DisableDtp();
        }
        private void btnCustomDate_Click(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOkCustomDate.Visible = true;
            SetBtnColor(sender);

        }
        private void btnOkCustomDate_Click(object sender, EventArgs e)
        {
            LoadData();
        }





        private void btnMembers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new MemberList());
        }
        private void btnBilling_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new Billing());
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            //   OpenChildForm(new FormProducts());
        }
        private void btnLeads_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            // OpenChildForm(new FormCustomers());
        }
        private void btnEmployee_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            // OpenChildForm(new FormMarketing());
        }
        private void btnPlans_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new AddMembership());
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCustomDate_Click_1(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOkCustomDate.Visible = true;
        }

        private void lblStartDate_Click(object sender, EventArgs e)
        {
            if (currentBtn == btnCustomDate)
            {
                MessageBox.Show("lol");
                dtpStartDate.Select();
                SendKeys.Send("%{DOWN}");

            }
        }

        private void lblEndDate_Click(object sender, EventArgs e)
        {
            if (currentBtn == btnCustomDate)
            {
                dtpEndDate.Show();
                SendKeys.Send("%{DOWN}");

            }
        }

        #endregion

        #region Border radius panels
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Radio del borde redondeado
            Rectangle rect = panel1.ClientRectangle;

            // Crear un GraphicsPath para definir el rectángulo redondeado
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - borderRadius, rect.Top, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
            path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
            path.AddArc(rect.Left, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
            path.CloseAllFigures();

            // Establecer la región del panel con los bordes redondeados
            panel1.Region = new Region(path);

            // Habilitar antialiasing para un renderizado más suave
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Opcional: Dibujar un borde alrededor del panel para visualizar los bordes redondeados
            using (Pen pen = new Pen(Color.Gainsboro, 3)) // Ajustar el color y el grosor del borde si es necesario
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void chartGrossRevenue_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Radio del borde redondeado
            Rectangle rect = chartGrossRevenue.ClientRectangle;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - borderRadius, rect.Top, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
            path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
            path.AddArc(rect.Left, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
            path.CloseAllFigures();

            chartGrossRevenue.Region = new Region(path);

            // Habilitar antialiasing para suavizar el renderizado
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.Gainsboro, 3)) // Ajustar el color y el grosor del borde si es necesario
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Radio del borde redondeado
            Rectangle rect = panel2.ClientRectangle;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - borderRadius, rect.Top, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
            path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
            path.AddArc(rect.Left, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
            path.CloseAllFigures();

            panel2.Region = new Region(path);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.Gainsboro, 3)) // Ajustar el color y el grosor del borde si es necesario
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Radio del borde redondeado
            Rectangle rect = panel3.ClientRectangle;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - borderRadius, rect.Top, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
            path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
            path.AddArc(rect.Left, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
            path.CloseAllFigures();

            panel3.Region = new Region(path);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.Gainsboro, 3)) // Ajustar el color y el grosor del borde si es necesario
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Radio del borde redondeado
            Rectangle rect = panel4.ClientRectangle;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - borderRadius, rect.Top, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
            path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
            path.AddArc(rect.Left, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
            path.CloseAllFigures();

            panel4.Region = new Region(path);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.Gainsboro, 3)) // Ajustar el color y el grosor del borde si es necesario
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void charTopHours_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Radio del borde redondeado
            Rectangle rect = charTopHours.ClientRectangle;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - borderRadius, rect.Top, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
            path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
            path.AddArc(rect.Left, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
            path.CloseAllFigures();

            charTopHours.Region = new Region(path);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.Gainsboro, 3)) // Ajustar el color y el grosor del borde si es necesario
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        #endregion


        private void SetPanelBorderRad()
        {
            this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
            this.panel2.Paint += new PaintEventHandler(this.panel2_Paint);
            this.panel3.Paint += new PaintEventHandler(this.panel2_Paint);
            this.panel4.Paint += new PaintEventHandler(this.panel2_Paint);
            this.charTopHours.Paint += new PaintEventHandler(this.charTopHours_Paint);
            this.chartGrossRevenue.Paint += new PaintEventHandler(this.chartGrossRevenue_Paint);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
