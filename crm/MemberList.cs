using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dominio;
using Negocio;
namespace TPFinalNivel2_Sansberro
{
    public partial class MemberList : Form
    {
        private List<Member> listaProducto;
        public MemberList()
        {
            InitializeComponent();
        }

        private void stock_Load(object sender, EventArgs e)
        {

            cargar();
            
            cbCampo.Items.Add("Active");
            cbCampo.Items.Add("Gender");
            
        }



        private void cargar()
        {
            MemberBusiness negocio = new MemberBusiness();
            try
            {
                negocio.UpdateSubscriptionStatus();
                
                listaProducto = negocio.Listar();
                dgvStock.DataSource = null; 
                dgvStock.DataSource = listaProducto;
                LoadDgvColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void dgvStock_SelectionChanged(object sender, EventArgs e)
        {
           if(dgvStock.CurrentRow != null)
            {
                Member seleccionado = (Member)dgvStock.CurrentRow.DataBoundItem;
                LoadImage();
            }


        } 
        public void LoadImage()
        {
            if (!string.IsNullOrEmpty(null))
            {
                pbxDetail.Load("https://fcb-abj-pre.s3.amazonaws.com/img/jugadors/MESSI.jpg");
            }
            else
            {
                pbxDetail.Load("https://fcb-abj-pre.s3.amazonaws.com/img/jugadors/MESSI.jpg");
            }
        }

        private void LoadDgvColumns()
        {
            dgvStock.Columns["clienteId"].Visible = false;
            dgvStock.Columns["pesoCliente"].Visible = false;
            dgvStock.Columns["alturaCliente"].Visible = false;
            dgvStock.Columns["nombreCliente"].HeaderText = "Name";
            dgvStock.Columns["apellidoCliente"].HeaderText = "Last Name";
            dgvStock.Columns["dniCliente"].Visible = false;
            dgvStock.Columns["nacimientoCliente"].HeaderText = "BirthDate";
            dgvStock.Columns["generoCliente"].HeaderText = "Gender";
            dgvStock.Columns["telefonoCliente"].HeaderText = "Phone Number";
            dgvStock.Columns["emailCliente"].HeaderText = "Email";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AddMember alta = new AddMember();
            alta.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            Member seleccionado;
            seleccionado = (Member)dgvStock.CurrentRow.DataBoundItem;
            int clienteIdVar = seleccionado.clienteId;

            AddMember modificar = new AddMember(seleccionado, clienteIdVar);
            modificar.ShowDialog();
            cargar();
                
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MemberBusiness negocio = new MemberBusiness();
            Member seleccionado;
            try
            {
               DialogResult respuesta = MessageBox.Show("Realmente desea eliminar este articulo?", "Eliminando", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Member)dgvStock.CurrentRow.DataBoundItem;
                    negocio.LogicDelete(seleccionado.clienteId);
                    cargar();
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MemberBusiness negocio = new MemberBusiness();
            if (cbCampo.SelectedItem != null && cbCriterio.SelectedItem != null)
            {
                string campo = cbCampo.SelectedItem.ToString();
                string criterio = cbCriterio.SelectedItem.ToString();               
                try
                {
                    dgvStock.DataSource = negocio.filtrar(campo, criterio);
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                
            }
            else
            {
                MessageBox.Show("Seleccione campo y criterio para realizar su busqueda!");
            }




        }
        private bool EsNumeroValido(string texto)
        {
            return decimal.TryParse(texto, out _);
        }

        private void tbBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            List<Member> listaFiltrada;
            string filtro = tbBuscar.Text;
            if (filtro.Length >= 3)
            {
                listaFiltrada = listaProducto.FindAll(A => A.apellidoCliente.ToUpper().Contains(filtro.ToUpper()) || A.dniCliente.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaProducto;
            }
            dgvStock.DataSource = null;
            dgvStock.DataSource = listaFiltrada;
            LoadDgvColumns();
        }
       private void cbCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemberBusiness negocio = new MemberBusiness();
            try
            {

                string Active = cbCriterio.SelectedItem.ToString();
                string Gender = cbCriterio.SelectedItem.ToString();
                dgvStock.DataSource = negocio.filtrar(Active,Gender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbCampo.SelectedItem.ToString();
            if(opcion == "Active" )
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Active");
                cbCriterio.Items.Add("Innactive");              
            }
            else if(opcion == "Gender")
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Male");
                cbCriterio.Items.Add("Female");
                cbCriterio.Items.Add("Non-specified");                
            }        
        }


        
 
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvStock.SelectedRows.Count > 0)
            {
                Member selectedMember = dgvStock.SelectedRows[0].DataBoundItem as Member;
                if (selectedMember != null)
                {
                    MemberDetails detailForm = new MemberDetails(selectedMember);
                    detailForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Select a member to see their details.", "No member has been selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            DashBoard dash = new DashBoard();
            dash.ShowDialog();
        }
    }
}
