using System;
using System.Windows.Forms;
using Negocio;
using Dominio;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
namespace TPFinalNivel2_Sansberro
{
    public partial class MemberDetails : Form
    {
        private Member selectedMember;
        private List<Ingresos> PaymentMember;
        private Button currentButton;

        public MemberDetails(Member member)
        {
            InitializeComponent();
            selectedMember = member;

            //Default - Last 7 days
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select();
            SetBtnColor(btnLast7Days);
            this.pbxDetalle.Paint += new PaintEventHandler(this.pbxDetalle_Paint);

            if (selectedMember != null)
            {
                lblNombre.Text = selectedMember.nombreCliente + " " + selectedMember.apellidoCliente;
                lblContactDetails.Text = selectedMember.telefonoCliente;
                // lblBillingDetails.Text = selectedMember.;    
                lblBirthDay.Text = selectedMember.nacimientoCliente.ToShortDateString();
                LoadData();
                LoadImage(selectedMember.apellidoCliente);
            }
        }
        private void detalleProducto_Load(object sender, EventArgs e)
        {

        }
        #region Methods
        private void LoadData()
        {
            MemberBusiness negocio = new MemberBusiness();
            var clienteId = selectedMember.clienteId;

            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;
            Member Selected = selectedMember;
            int clienteIdVar = selectedMember.clienteId;

            List<Ingresos> PaymentMember = negocio.GetIncomesByCustomer(Selected);
            dgvPayments.DataSource = null;
            dgvPayments.DataSource = PaymentMember;


            // Get the attendance data for the selected member
            List<MemberAttendance> attendanceList = negocio.GetMemberAttendance(clienteIdVar, dtpStartDate.Value, dtpEndDate.Value);

            // Set the DataSource of the DataGridView to the attendance list
            dgvAttendance.DataSource = null;
            dgvAttendance.DataSource = attendanceList;

            // Optional: Format the DataGridView columns
            dgvAttendance.Columns["Date"].HeaderText = "Date";
            dgvAttendance.Columns["Quantity"].HeaderText = "Attendance Count";
            LoadPaymentDgv();
            chartAttendance.DataSource = negocio.GetMemberAttendance(clienteId, startDate, endDate);
            chartAttendance.Series[0].XValueMember = "Date";
            chartAttendance.Series[0].YValueMembers = "Quantity";
            chartAttendance.DataBind();
        }

        private void LoadPaymentDgv()
        {
            dgvPayments.Columns["fechaIngreso"].HeaderText = "Income Date";
            dgvPayments.Columns["monto"].HeaderText = "Income";
            dgvPayments.Columns["cantidad"].HeaderText = "Amount";


            dgvPayments.Columns["saldoEstado"].Visible = false;
            dgvPayments.Columns["clienteId"].Visible = false;
            dgvPayments.Columns["ingresoId"].Visible = false;
            dgvPayments.Columns["transaccionId"].Visible = false;
            dgvPayments.Columns["tipoMembresiaId"].Visible = false;
           
        }

        #endregion



        #region BTN EVENTS
        private void DisableDtp()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOkCustomDate.Visible = false;
        }
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

        public void LoadImage(string Image)
        {
            if (!string.IsNullOrEmpty(null))
            {
                pbxDetalle.ImageLocation = null;
            }
            else
            {
                pbxDetalle.Load("https://fcb-abj-pre.s3.amazonaws.com/img/jugadors/MESSI.jpg");
            }
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            Member Selected;
            Selected = selectedMember;
            int clienteIdVar = selectedMember.clienteId;

            AddMember modificar = new AddMember(Selected, clienteIdVar);
            modificar.ShowDialog();
        }
        #endregion


        private void pbxDetalle_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Radio del borde redondeado
            Rectangle rect = pbxDetalle.ClientRectangle;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - borderRadius, rect.Top, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
            path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
            path.AddArc(rect.Left, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
            path.CloseAllFigures();

            pbxDetalle.Region = new Region(path);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(31, 30, 68), 3)) // Ajustar el color y el grosor del borde si es necesario
            {
                e.Graphics.DrawPath(pen, path);
            }
        }




        private void dgvAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblBirthDay_Click(object sender, EventArgs e)
        {

        }

        private void lblBillingDetails_Click(object sender, EventArgs e)
        {

        }

        private void lblContactDetails_Click(object sender, EventArgs e)
        {

        }
    }
}
