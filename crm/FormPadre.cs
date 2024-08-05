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
    public partial class FormPadre : Form
    {
        public FormPadre()
        {
            InitializeComponent();
            InitializeCustomComponents();   
        }
        private void InitializeCustomComponents()
        {
            // Establecer el modo de autoescalado
            this.AutoScaleMode = AutoScaleMode.Font;

            // Establecer el tamaño mínimo del formulario
            this.MinimumSize = new Size(800, 600);

            // Manejar el evento de cambio de tamaño
            this.Resize += new EventHandler(BaseForm_Resize);
        }

        private void BaseForm_Resize(object sender, EventArgs e)
        {
            // Ajustes adicionales de los controles
            AdjustControls();
        }

        protected virtual void AdjustControls()
        {
            // Lógica para ajustar los controles
            foreach (Control control in this.Controls)
            {
                control.Width = this.ClientSize.Width / 2;
            }
        }
        private void FormPadre_Load(object sender, EventArgs e)
        {

        }
    }
}
