using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debt
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Items frm = new Frm_Items();
            frm.ShowDialog();

        }

        private void اضافهقسمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Department frm = new Frm_Department();
            frm.ShowDialog();
        }

        private void اضافهشعبةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Section frm = new Frm_Section();
            frm.ShowDialog();
        }

        private void ادارةالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Emplyee frm = new Frm_Emplyee();
            frm.ShowDialog();
        }
    }
}
