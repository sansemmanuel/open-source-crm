using Negocio;
using System;
using Dominio;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace TPFinalNivel2_Sansberro
{
    public partial class Billing : Form
    {
        private static DateTime? ultimaFechaAlerta = null;

        private CancellationTokenSource cancellationTokenSource;

        private List<Memberships> MembershipOverdueDataList;
        private List<Memberships> MembershipdataList;
        private List<Memberships> MembershipPendingDataList;

        public Billing()
        {
            InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            dgvLoad();
            TierPaymentReminder();
        }

        private void dgvLoad()
        {
            MemberBusiness memberBusiness = new MemberBusiness();
            try
            {
                memberBusiness.UpdateSubscriptionStatus();
                MembershipdataList = memberBusiness.LoadPaidMembershipData();
                dgvPaid.DataSource = null;
                dgvPaid.DataSource = MembershipdataList;

                MembershipOverdueDataList = memberBusiness.LoadOverdueMembershipData();
                dgvOverdue.DataSource = MembershipOverdueDataList;

                MembershipPendingDataList = memberBusiness.LoadPendingMembershipData();
                dgvPending.DataSource = MembershipPendingDataList;

                LoadDgvColumns();
                ManualPaymentAdd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadDgvColumns()
        {

            // Configuración de columnas para dgvPaid
            dgvPaid.Columns["nombreCliente"].HeaderText = "Name";
            dgvPaid.Columns["apellidoCliente"].HeaderText = "Last Name";
            dgvPaid.Columns["finMembresia"].HeaderText = "Membership End Date";
            dgvPaid.Columns["tipoMembresiaId"].Visible = false;

            dgvPaid.Columns["membresiaId"].Visible = false;
            dgvPaid.Columns["vencidoMembresia"].Visible = false;
            dgvPaid.Columns["clienteId"].Visible = false;
            dgvPaid.Columns["pagoMembresia"].Visible = false;
            dgvPaid.Columns["inicioMembresia"].Visible = false;
            dgvPaid.Columns["telefonoCliente"].Visible = false;
            dgvPaid.Columns["pagoProgramadoMembresia"].Visible = false;
            dgvPaid.Columns["pagoPendienteMembresia"].Visible = false;
            dgvPaid.Columns["pagoRechazadoMembresia"].Visible = false;

            // Configuración de columnas para dgvOverdue
            dgvOverdue.Columns["nombreCliente"].HeaderText = "Name";
            dgvOverdue.Columns["apellidoCliente"].HeaderText = "Last Name";
            dgvOverdue.Columns["finMembresia"].HeaderText = "Membership End Date";
            dgvOverdue.Columns["tipoMembresiaId"].Visible = false;

            dgvOverdue.Columns["membresiaId"].Visible = false;
            dgvOverdue.Columns["vencidoMembresia"].Visible = false;
            dgvOverdue.Columns["clienteId"].Visible = false;
            dgvOverdue.Columns["pagoMembresia"].Visible = false;
            dgvOverdue.Columns["inicioMembresia"].Visible = false;
            dgvOverdue.Columns["telefonoCliente"].Visible = false;
            dgvOverdue.Columns["pagoProgramadoMembresia"].Visible = false;
            dgvOverdue.Columns["pagoPendienteMembresia"].Visible = false;
            dgvOverdue.Columns["pagoRechazadoMembresia"].Visible = false;


            // Configuración de columnas para dgvPending
            dgvPending.Columns["nombreCliente"].HeaderText = "Name";
            dgvPending.Columns["apellidoCliente"].HeaderText = "Last Name";
            dgvPending.Columns["finMembresia"].HeaderText = "Membership End Date";
            dgvPending.Columns["tipoMembresiaId"].Visible = false;

            dgvPending.Columns["membresiaId"].Visible = false;
            dgvPending.Columns["vencidoMembresia"].Visible = false;
            dgvPending.Columns["clienteId"].Visible = false;
            dgvPending.Columns["pagoMembresia"].Visible = false;
            dgvPending.Columns["inicioMembresia"].Visible = false;
            dgvPending.Columns["telefonoCliente"].Visible = false;
            dgvPending.Columns["pagoProgramadoMembresia"].Visible = false;
            dgvPending.Columns["pagoPendienteMembresia"].Visible = false;
            dgvPending.Columns["pagoRechazadoMembresia"].Visible = false;


        }

        private void ManualPaymentAdd()
        {
            // Añadir columna de botón de pago manual a dgvOverdue
            DataGridViewButtonColumn btnManualPayOverdue = new DataGridViewButtonColumn();
            btnManualPayOverdue.HeaderText = "Payment";
            btnManualPayOverdue.Name = "btnManualPay";
            btnManualPayOverdue.Text = "Manual Payment";
            btnManualPayOverdue.UseColumnTextForButtonValue = true;
            dgvOverdue.Columns.Add(btnManualPayOverdue);

            DataGridViewButtonColumn btnManualPayPaid = new DataGridViewButtonColumn();
            btnManualPayPaid.HeaderText = "Payment";
            btnManualPayPaid.Name = "btnManualPay";
            btnManualPayPaid.Text = "Manual Payment";
            btnManualPayPaid.UseColumnTextForButtonValue = true;
            dgvPaid.Columns.Add(btnManualPayPaid);

            DataGridViewButtonColumn btnManualPayPending = new DataGridViewButtonColumn();
            btnManualPayPending.HeaderText = "Payment";
            btnManualPayPending.Name = "btnManualPay";
            btnManualPayPending.Text = "Manual Payment";
            btnManualPayPending.UseColumnTextForButtonValue = true;
            dgvPending.Columns.Add(btnManualPayPending);

            // Manejar el evento CellClick para el botón de pago manual
            dgvOverdue.CellClick += dgvOverdue_CellClick;
            dgvPending.CellClick += dgvPending_CellClick;
            dgvPaid.CellClick += dgvPaid_CellClick;
        }

        private void dgvOverdue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvOverdue.Columns["btnManualPay"].Index)
            {
                if (e.RowIndex >= 0)
                {
                    Memberships selectedMember = dgvOverdue.Rows[e.RowIndex].DataBoundItem as Memberships;

                    if (selectedMember != null)
                    {
                        RegisterPayment payment = new RegisterPayment(selectedMember);
                        payment.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Error: Contact the admin.");
                    }
                }
            }
        }

        private void dgvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPending.Columns["btnManualPay"].Index)
            {
                if (e.RowIndex >= 0)
                {
                    Memberships selectedMember = dgvPending.Rows[e.RowIndex].DataBoundItem as Memberships;

                    if (selectedMember != null)
                    {
                        RegisterPayment payment = new RegisterPayment(selectedMember);
                        payment.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Error:  Contact the admin.");
                    }
                }
            }
        }

        private void dgvPaid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPaid.Columns["btnManualPay"].Index)
            {
                if (e.RowIndex >= 0)
                {
                    Memberships selectedMember = dgvPaid.Rows[e.RowIndex].DataBoundItem as Memberships;

                    if (selectedMember != null)
                    {
                        RegisterPayment payment = new RegisterPayment(selectedMember);
                        payment.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Error: Contact the admin.");
                    }
                }
            }
        }


        private void TierPaymentReminder()
        {
            if (MembershipPendingDataList.Count > 0)
            {
                if (ultimaFechaAlerta == null || ultimaFechaAlerta.Value.Date != DateTime.Now.Date)
                {
                    DialogResult result = MessageBox.Show(
                        "A group of members have not yet paid their membership and their payment is in \"Pending\" status. Would you like to send them a reminder message?",
                        "Pending Payments",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        _ = SendWhatsappMessage(MembershipPendingDataList, cancellationTokenSource.Token);
                    }

                    // Set the alert date to ensure it only triggers once per day
                    ultimaFechaAlerta = DateTime.Now;
                }
            }
        }

        private async Task SendWhatsappMessage(List<Memberships> pendingMemberships, CancellationToken cancellationToken)
        {
            // Token
            string token = "";
            // Identificador de número de teléfono
            string idTelefono = "";

            HttpClient client = new HttpClient();
            try
            {
                foreach (var membership in pendingMemberships)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        // Cleanup, if needed
                        Console.WriteLine("Operation cancelled.");
                        break;
                    }
                    string telefono = membership.telefonoCliente;

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v15.0/" + idTelefono + "/messages");
                    request.Headers.Add("Authorization", "Bearer " + token);
                    request.Content = new StringContent("{ \"messaging_product\": \"whatsapp\", \"to\": \"" + telefono + "\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }");
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        // Log the error
                        MessageBox.Show($"Failed to send message to {telefono}. Status Code: {response.StatusCode}, Response: {responseBody}");
                    }
                    else
                    {
                        // Log the success
                        MessageBox.Show($"Successfully sent message to {telefono}. Response: {responseBody}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The WhatsApp message could not be sent", ex.ToString());
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string filtro = txtSearch.Text;
            List<Memberships> listaFiltradaPaid = MembershipdataList;
            List<Memberships> listaFiltradaOverdue = MembershipOverdueDataList;
            List<Memberships> listaFiltradaPending = MembershipPendingDataList;

            if (filtro.Length >= 3)
            {
                listaFiltradaPaid = MembershipdataList
                    .Where(A => A.apellidoCliente.ToUpper().Contains(filtro.ToUpper()) || A.nombreCliente.ToUpper().Contains(filtro.ToUpper()))
                    .ToList();

                listaFiltradaOverdue = MembershipOverdueDataList
                    .Where(A => A.apellidoCliente.ToUpper().Contains(filtro.ToUpper()) || A.nombreCliente.ToUpper().Contains(filtro.ToUpper()))
                    .ToList();

                listaFiltradaPending = MembershipPendingDataList
                    .Where(A => A.apellidoCliente.ToUpper().Contains(filtro.ToUpper()) || A.nombreCliente.ToUpper().Contains(filtro.ToUpper()))
                    .ToList();
            }

            // Clear the data sources before reassigning
            dgvPaid.DataSource = null;
            dgvOverdue.DataSource = null;
            dgvPending.DataSource = null;

            // Assign the filtered list to the respective DataGridView
            dgvPaid.DataSource = listaFiltradaPaid;
            dgvOverdue.DataSource = listaFiltradaOverdue;
            dgvPending.DataSource = listaFiltradaPending;

            LoadDgvColumns();
        }
    }
}
