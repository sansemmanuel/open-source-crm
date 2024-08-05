using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dominio;
using Negocio;
using System.Linq;


namespace TPFinalNivel2_Sansberro
{
    public partial class RegisterPayment : Form
    {
        private Memberships SelectedMember;
        private int tipomembresiaId;
        private decimal TotalWTaxes;
        private int tranId;
        private List<MembershipType> membershipTypes;

        public RegisterPayment(Memberships member)
        {
            InitializeComponent();
            SelectedMember = member;
            //  tipomembresiaId = SelectedMember.tipoMembresiaId;
        }

        private void frmBillingRegisterPayment_Load(object sender, EventArgs e)
        {
            if (SelectedMember != null)
            {
                LoadMemberDetails();
            }
            LoadComboBoxes();
        }


        #region events
        private void LoadMemberDetails()
        {
            txtName.Text = SelectedMember.nombreCliente;
            txtLastN.Text = SelectedMember.apellidoCliente;
            //txtDni.Text = SelectedMember.dniCliente; // Consider uncommenting if needed
        }

        private void LoadComboBoxes()
        {
            LoadMembershipComboBox();
            LoadPaymentMethodComboBox();
        }

        private void LoadMembershipComboBox()
        {
            cboxType.Items.Clear();
            cboxType.Items.Add("Product");
            cboxType.Items.Add("Membership");
        }

        private void LoadPaymentMethodComboBox()
        {
            cboxPaymentMethod.Items.Clear();
            cboxPaymentMethod.Items.Add("Cash");
            cboxPaymentMethod.Items.Add("Card");
        }

        private void cboxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemberBusiness negocio = new MemberBusiness();
            membershipTypes = negocio.LoadMemberships();

            if (cboxType.SelectedItem.ToString() == "Membership")
            {
                cboxMembresia.Enabled = true;
                cboxMembresia.DataSource = membershipTypes;
                cboxMembresia.DisplayMember = "membresiaTipo";
                cboxMembresia.ValueMember = "tipoMembresiaId";

            }
            else if (cboxType.SelectedItem.ToString() == "Product")
            {
                cboxMembresia.DataSource = null;
                cboxMembresia.Items.Clear();
                cboxMembresia.Enabled = false;
                txtSubTotal.Enabled = true;
            }
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            InsertTran();
            InsertIncome();
            UpdateWPayment();
        }

        private void cboxMembresia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxMembresia.SelectedValue != null)
            {

                if (cboxMembresia.SelectedValue is int intValue)
                {
                    tipomembresiaId = intValue;
                }
                else
                {
                    //    MessageBox.Show("Selected item is not a DataRowView or an integer.");
                }
            }
            else
            {
                // Handle the case where SelectedValue is null
                //  MessageBox.Show("No item selected.");
            }


            LoadSubTotal();
            CalculateTotalWithTaxes();

        }

        #endregion


        #region Inser & Create Income               
        private void InsertIncome()
        {
            try
            {
                Ingresos ingresos = CreateNewIncome();
                MemberBusiness negocio = new MemberBusiness();

                if (ingresos != null)
                {
                    negocio.AddNewIncome(ingresos);
                    MessageBox.Show("Income added successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting income: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Ingresos CreateNewIncome()
        {
            Ingresos ingresos = new Ingresos();
            MemberBusiness negocio = new MemberBusiness();

            if (!TrySetClienteId(ingresos))
                return null;

            if (!TrySetFechaIngreso(ingresos))
                return null;

            if (!TrySetTransaccionId(ingresos, negocio))
                return null;

            if (!TrySetSaldoEstado(ingresos))
                return null;
            if (!TrySetCantidad(ingresos))
                return null;

            TrySetAmount(ingresos);
            TrySetMembershipId(ingresos);
            return ingresos;
        }

        private bool TrySetClienteId(Ingresos ingresos)
        {
            try
            {
                ingresos.clienteId = SelectedMember.clienteId;
                MessageBox.Show($"clienteId set to {ingresos.clienteId}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting clienteId: {ex.Message}");
                return false;
            }
        }


        private bool TrySetFechaIngreso(Ingresos ingresos)
        {
            try
            {
                ingresos.fechaIngreso = DateTime.Now;
                MessageBox.Show($"fechaIngreso set to {ingresos.fechaIngreso}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting fechaIngreso: {ex.Message}");
                return false;
            }
        }

        private bool TrySetTransaccionId(Ingresos ingresos, MemberBusiness negocio)
        {
            try
            {
                ingresos.transaccionId = negocio.GetLastTran();
                MessageBox.Show($"transaccionId set to {ingresos.transaccionId}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting transaccionId: {ex.Message}");
                return false;
            }
        }


        private bool TrySetSaldoEstado(Ingresos ingresos)
        {
            MemberBusiness negocio = new MemberBusiness();
            try
            {
                ingresos.saldoEstado = negocio.GetLatestBalance() + TotalWTaxes;
                MessageBox.Show($"Nuevo saldo set to {ingresos.saldoEstado}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting saldoEstado: {ex.Message}");
                return false;
            }
        }
        private bool TrySetCantidad(Ingresos ingresos)
        {
            try
            {
                ingresos.cantidad = 1; // Set a default value for saldoAnterior
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting saldoAnterior: {ex.Message}");
                return false;
            }
        }
        private void TrySetAmount(Ingresos ingresos)
        {
            try
            {
                string text = txtTotal.Text;

                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("El campo de texto está vacío. Por favor ingrese un valor.");
                    ingresos.monto = 0m; // Valor por defecto
                    return;
                }

                // Eliminar signos de dólar y otros caracteres no numéricos
                text = text.Replace("$", "").Trim();

                if (decimal.TryParse(text, out decimal result))
                {
                    ingresos.monto = result;
                    MessageBox.Show($"Conversión exitosa: {result}");
                }
                else
                {
                    MessageBox.Show("El valor ingresado no es un número válido. Se ha establecido a 0.");
                    ingresos.monto = 0m; // Valor por defecto
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting monto: {ex.Message}");
                ingresos.monto = 0m; // Establecer un valor por defecto en caso de error
            }
        }

        private void TrySetMembershipId(Ingresos ingresos)
        {
            try
            {
                if (cboxMembresia.SelectedValue != null)
                {
                    ingresos.tipoMembresiaId = (int)cboxMembresia.SelectedValue;
                }
                else
                {
                    ingresos.tipoMembresiaId = SelectedMember.tipoMembresiaId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting tipoMembresiaId: {ex.Message}");
            }
        }
        #endregion


        #region Insert & Create Transaction
        private void InsertTran()
        {
            try
            {
                Transaccion tran = CreateNewTransaction();
                MemberBusiness negocio = new MemberBusiness();

                if (tran != null)
                {
                    negocio.AddNewTransfer(tran);
                    tranId = tran.transaccionId;
                    MessageBox.Show("Payment registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                HandleTransactionInsertionError(ex);
            }
        }
        private void SetTransactionDate(Transaccion tran)
        {
            tran.fechaTransaccion = DateTime.Now;
        }
        private Transaccion CreateNewTransaction()
        {
            Transaccion tran = new Transaccion();

            try
            {
                SetTransactionDate(tran);
                SetTransactionPaymentMethod(tran);
                return tran;
            }
            catch (Exception ex)
            {
                HandleTransactionCreationError(ex);
                return null;
            }
        }
        private void SetTransactionPaymentMethod(Transaccion tran)
        {
            if (cboxPaymentMethod.SelectedItem == null)
                throw new ArgumentNullException("Please select a payment method.");

            tran.metodoPago = cboxPaymentMethod.SelectedItem.ToString();
        }
        private void HandleTransactionInsertionError(Exception ex)
        {
            MessageBox.Show($"An error occurred while registering the payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HandleTransactionCreationError(Exception ex)
        {
            MessageBox.Show($"An error occurred while creating the transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void UpdateWPayment()
        {
            MemberBusiness negocio = new MemberBusiness();
            Memberships membershipdata = new Memberships();
            try
            {
                if (cboxType.SelectedIndex == 1)
                {
                    membershipdata.pagoMembresia = true;
                    membershipdata.clienteId = SelectedMember.clienteId;
                    membershipdata.inicioMembresia = DateTime.Now;

                    if (cboxMembresia.SelectedIndex == 0)
                    {
                        membershipdata.finMembresia = membershipdata.inicioMembresia.AddMonths(1);
                    }
                    else if (cboxMembresia.SelectedIndex == 1)
                    {
                        membershipdata.finMembresia = membershipdata.inicioMembresia.AddMonths(12);

                    }
                    else if (cboxMembresia.SelectedIndex == 2)
                    {
                        membershipdata.finMembresia = membershipdata.inicioMembresia.AddMonths(12);
                    }
                    membershipdata.vencidoMembresia = false;
                    if (Int32.TryParse(cboxMembresia.SelectedValue.ToString(), out int tipoMembresiaId))
                    {
                        membershipdata.tipoMembresiaId = tipoMembresiaId;
                    }
                    negocio.UpdateMembershipToPaid(membershipdata);
                }
                else if (cboxType.SelectedIndex == 0)
                {

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Select the correct membership for the member
        private MembershipType GetSelectedMembership()
        {
            MemberBusiness negocio = new MemberBusiness();
            membershipTypes = negocio.LoadMemberships();
            return membershipTypes.FirstOrDefault(x => x.tipoMembresiaId == tipomembresiaId);
        }


        private void LoadSubTotal()
        {
            var selectedMembership = GetSelectedMembership();



            if (selectedMembership != null)
            {
                //    MessageBox.Show($"Precio: {selectedMembership.membresiaCosto}, ID: {tipomembresiaId}");




                txtSubTotal.Text = "$" + selectedMembership.membresiaCosto.ToString();



            }
            else
            {
                //      MessageBox.Show($"No se encontró una membresía con el ID: {tipomembresiaId}");
            }
        }

        private void CalculateTotalWithTaxes()
        {
            var selectedMembership = GetSelectedMembership();
            if (selectedMembership != null)
            {
                TotalWTaxes = selectedMembership.membresiaCosto * 6 / 100 + selectedMembership.membresiaCosto;
                txtTotal.Text = "$" + TotalWTaxes.ToString();
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int subTotal = Int32.Parse(txtSubTotal.Text);
                double totalWTaxes = subTotal * 1.06; // Sumar el 6% al subtotal
                txtTotal.Text = totalWTaxes.ToString("F2"); // Mostrar el total con impuestos, con 2 decimales
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un número válido.");
            }
        }

    }
}
