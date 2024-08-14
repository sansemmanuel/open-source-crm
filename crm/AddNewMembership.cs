using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crm
{
    public partial class AddNewMembership : Form
    {
        private List<MembershipType> membershipTypes;
        public AddNewMembership()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private bool IsValidInput()
        {
            if (string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtType.Text) ||
                string.IsNullOrWhiteSpace(cboxDuration.Text))
            {
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Please verify that the values entered are valid.");
                return false;
            }

            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(IsValidInput()) { 
               MembershipType membershipType = new MembershipType();
                try
                {
                    membershipType.membresiaDias = Int32.Parse(cboxDuration.Text);
                    membershipType.membresiaTipo = txtType.Text;
                    membershipType.membresiaCosto = decimal.Parse(txtPrice.Text);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
