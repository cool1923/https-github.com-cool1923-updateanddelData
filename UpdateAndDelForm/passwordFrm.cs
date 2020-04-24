using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateAndDelForm
{
    public partial class passwordFrm : Form
    {
        public passwordFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pwd = "778899";

            if (textBox1.Text == pwd)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
