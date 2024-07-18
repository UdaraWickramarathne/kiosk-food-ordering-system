using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace food_ordering
{
    public partial class Form1 : Form
    {   

        public Form1()
        {
            InitializeComponent();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (tglShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';

            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            String uname = txtUname.Text;
            String pass = txtPassword.Text;
              
            if(uname.Length == 0 || pass.Length == 0)
            {
                lblError.Text = "Please fill all the fields";
                lblError.Visible = true;
                return;
            }
            else if(uname == "nsbm" && pass == "admin")
            {
                this.Close();
                Thread th = new Thread(openMenu);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
            {
                lblError.Text = "Invalid Branch Id or Password";
                lblError.Visible = true;
            }

        }

        private void openMenu()
        {
            Application.Run(new Menu());
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }
    }
}
