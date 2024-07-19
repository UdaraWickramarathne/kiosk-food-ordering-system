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
    public partial class digitalclock : UserControl
    {
        public digitalclock()
        {
            InitializeComponent();
        }
        private void digitalclock_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void timer_Click(object sender, EventArgs e)
        {

        }

    
    }
}
