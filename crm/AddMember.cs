using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TPFinalNivel2_Sansberro
{
    public partial class AddMember : Form
    {
        private List<MembershipType> membershipTypes;
        private Member member = null;
        private int clienteIdVar;
        public AddMember()
        {
            InitializeComponent();
        }

        public AddMember(Member member, int clienteIdVar)
        {
            InitializeComponent();
            this.member = member;
            this.clienteIdVar = clienteIdVar;
        }

        private void AddMember_Load(object sender, EventArgs e)
        {
            loadCbox();

            try
            {
                if (member != null)
                {
                    CargarTextoNoNulo(txtName, member.nombreCliente, "nombreCliente");
                    CargarTextoNoNulo(txtLastName, member.apellidoCliente, "apellidoCliente");
                    CargarTextoNoNulo(txtDni, member.dniCliente, "dniCliente");
                    CargarNumeroNoNulo(txtWeight, member.pesoCliente, "pesoCliente");
                    CargarNumeroNoNulo(txtHeight, member.alturaCliente, "alturaCliente");
                    if (member.generoCliente == "Male")
                    {
                        cboxGender.SelectedIndex = 0;
                    }
                    else if (member.generoCliente == "Female")
                    {
                        cboxGender.SelectedIndex = 1;
                    }
                    else if (member.generoCliente == "Non-specified")
                    {
                        cboxGender.SelectedIndex = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CargarTextoNoNulo(TextBox textBox, string valor, string campo)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                textBox.Text = valor;
            }
            else
            {
                MessageBox.Show($"El campo {campo} no se ha logrado cargar");
            }
        }

        private void CargarNumeroNoNulo(TextBox textBox, decimal? valor, string campo)
        {
            if (valor.HasValue)
            {
                textBox.Text = valor.Value.ToString();
            }
            else
            {
                MessageBox.Show($"El campo {campo} no se ha logrado cargar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (CamposValidos())
            {
                Member prod = new Member();
                Memberships memberships = new Memberships();
                MemberBusiness negocio = new MemberBusiness();
                try
                {
                    prod.nombreCliente = txtName.Text;
                    prod.apellidoCliente = txtLastName.Text;
                    prod.dniCliente = txtDni.Text;
                    prod.nacimientoCliente = dtpBorn.Value;
                    prod.emailCliente = txtEmail.Text;

                    if (cboxGender.SelectedIndex == 0)
                    {
                        prod.generoCliente = "Male";
                    }
                    else if (cboxGender.SelectedIndex == 1)
                    {
                        prod.generoCliente = "Female";
                    }
                    else if (cboxGender.SelectedIndex == 2)
                    {
                        prod.generoCliente = "Other";
                    }

                    prod.pesoCliente = Int32.Parse(txtWeight.Text);
                    prod.telefonoCliente = txtPhone.Text;
                    prod.alturaCliente = decimal.Parse(txtHeight.Text);
                    prod.clienteId = clienteIdVar;
                    if (member != null)
                    {
                        negocio.UpdateMember(prod);
                        MessageBox.Show($"clienteId: {prod.clienteId}, txtName: {txtName.Text}");
                    }
                    else
                    {
                        negocio.AddNewMember(prod);
                        MessageBox.Show($"clienteId: {prod.clienteId}, txtName: {txtName.Text}");
                        Close();
                        // Llamada para obtener el último clienteId
                        int ultimoClienteId = negocio.GetLatestMember();

                        // Asignar el clienteId al objeto prod

                        memberships.inicioMembresia = DateTime.Now;


                        if (cboxMemberType.SelectedIndex == 0)
                        {
                            memberships.finMembresia = memberships.inicioMembresia.AddMonths(1);
                        }
                        else if (cboxMemberType.SelectedIndex == 1)
                        {
                            memberships.finMembresia = memberships.inicioMembresia.AddMonths(12);
                        }
                        else if (cboxMemberType.SelectedIndex == 2)
                        {
                            memberships.finMembresia = memberships.inicioMembresia.AddMonths(12);
                        }
                        memberships.vencidoMembresia = false;
                        memberships.pagoMembresia = true;
                        memberships.pagoPendienteMembresia = false;
                        memberships.pagoProgramadoMembresia = false;
                        memberships.pagoRechazadoMembresia = false;
                        memberships.clienteId = ultimoClienteId; // Utilizando el último clienteId obtenido
                  
                        
                        if (Int32.TryParse(cboxMemberType.SelectedValue.ToString(), out int tipoMembresiaId))
                        {
                            memberships.tipoMembresiaId = tipoMembresiaId;
                        }
                     
                        
                        negocio.AddNewMembership(memberships);
                        Close();

                       
                        
                        MessageBox.Show("Member successfully added.", ultimoClienteId.ToString() + "tipomembresia" + memberships.tipoMembresiaId.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please verify that the values entered are valid.");
            }
        }

        private bool CamposValidos()
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtDni.Text) ||
                string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                return false;
            }

            if (!decimal.TryParse(txtWeight.Text, out _))
            {
                MessageBox.Show("Please verify that the values entered are valid.");
                return false;
            }

            return true;
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxProd.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxProd.Load("https://w7.pngwing.com/pngs/507/59/png-transparent-dolphin-error-404-blue-marine-mammal-mammal.png");
            }
        }

        private void loadCbox()
        {
            MemberBusiness negocio = new MemberBusiness();
            membershipTypes = negocio.LoadMemberships();
            cboxMemberType.DataSource = membershipTypes;
            cboxMemberType.DisplayMember = "membresiaTipo";
            cboxMemberType.ValueMember = "tipoMembresiaId";

            cboxGender.Items.Add("Male");
            cboxGender.Items.Add("Female");
            cboxGender.Items.Add("Other");
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
