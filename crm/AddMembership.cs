using Crm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPFinalNivel2_Sansberro
{
    public partial class AddMembership : Form
    {
        public AddMembership()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           AddNewMembership addNewMembership = new AddNewMembership();
            addNewMembership.Show();
        }
    }
}
