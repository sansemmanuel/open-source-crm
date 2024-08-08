using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using FontAwesome.Sharp;
using System.Drawing.Drawing2D;
using Crm;

namespace TPFinalNivel2_Sansberro
{
    public partial class DashBoard : Form
    {
        //iu btn
        private Form currentChildForm;
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Button currentButton;
        //private DashBoard model;


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
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 55);
            panelLeft.Controls.Add(leftBorderBtn);
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
            OpenChildForm(new Reports());
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




    }


    #endregion

}

