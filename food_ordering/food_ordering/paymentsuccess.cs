using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace food_ordering
{
    public partial class paymentsuccess : Form
    {
        public paymentsuccess()
        {
            InitializeComponent();
        }

        public paymentsuccess(int orderid, Bitmap qrImage)
        { 
            InitializeComponent();
            lblCode.Text = "Your OrderID: " + orderid;
            pic.Image = new Bitmap(qrImage);
            lblCode.Visible = false;
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkQr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lblCode.Visible = true;
        }
    }
}
