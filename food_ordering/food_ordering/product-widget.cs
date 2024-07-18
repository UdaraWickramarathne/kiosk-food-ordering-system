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
    public partial class product_widget : UserControl
    {
        public product_widget()
        {
            InitializeComponent();
        }

        public event EventHandler onSelect = null;
        public event EventHandler minus = null;
        public event EventHandler add = null;
        public string Price { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public int Count { get; set; }
        public Image PImage
        {
            get { return itemImage.Image; }
            set { itemImage.Image = value; }
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            Count = Convert.ToInt32(lblCount.Text);
            if (Count > 1)
            {
                minus?.Invoke(this, e);
                Count--;
                lblCount.Text = Count.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            Count = Convert.ToInt32(lblCount.Text);
            onSelect?.Invoke(this, e);
          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            Count = Convert.ToInt32(lblCount.Text);
            if (Count >= 1)
            {
                add?.Invoke(this, e);
                Count++;
                lblCount.Text = Count.ToString();
            }

        }

        private void product_widget_LocationChanged(object sender, EventArgs e)
        {

        }

        private void product_widget_Load(object sender, EventArgs e)
        {
            lblName.Text = Name;
            lblPrice.Text = Price;


        }
    }
}
