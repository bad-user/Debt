using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dbet;

namespace Debt
{
    public partial class Frm_Section : Form
    {
        public Frm_Section()
        {
            InitializeComponent();
        }

        DataTable tbl = new DataTable();
        DataTable tblSearch = new DataTable();
        DB db = new DB();
        int introw = 0;

        public void AutoNum()
        {
            tbl.Clear();
            tbl = db.RunReader("Select Max(Sec_ID) from Sections", "");
            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
                txtDesID.Text = "1";
            else
                txtDesID.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            if (cbxType.Items.Count >= 1)
                cbxType.SelectedIndex = 0;
            txtSecName.Clear();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
        }
        private void ShowData()
        {
            tbl.Clear();
            tbl = db.RunReader("select * from Sections", "");
            if ((tbl.Rows.Count <= 0))
            {
                MessageBox.Show("لا يوجد بيانات فى هذه الشاشة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtDesID.Text = tbl.Rows[introw][0].ToString();
                cbxType.SelectedValue = tbl.Rows[introw][2].ToString();
                txtSecName.Text = tbl.Rows[introw][1].ToString();
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnDeleteAll.Enabled = true;
            }
        }


        private void FillSections()
        {
            cbxType.DataSource = db.RunReader("select * from Department", "");
            cbxType.DisplayMember = "Dep_Name";
            cbxType.ValueMember = "Dep_ID";
        }
        private void Frm_Section_Load(object sender, EventArgs e)
        {
            FillSections();
            AutoNum();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AutoNum();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbxType.Text == "")
            { MessageBox.Show("من فضلك ادخل الاقسام اولا من شاشة الاقسام ", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            try
            {
                db.RunNunQuary("insert into Sections values(" + txtDesID.Text + ",'" + txtSecName.Text + "'," + cbxType.SelectedValue + ")", "تم اضافه بيانات الشعبه بنجاح");
                AutoNum();
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cbxType.Text == "")
            { MessageBox.Show("من فضلك ادخل الاقسام اولا من شاشة الاقسام ", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            try
            {
                db.RunNunQuary("update  Sections set Sec_Name='" + txtSecName.Text + "',Dep_ID=" + cbxType.SelectedValue + "  where Sec_ID=" + txtDesID.Text + " ", "تم تعديل بيانات الشعبه بنجاح");
                AutoNum();
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.RunNunQuary("delete  from Sections where Sec_ID=" + txtDesID.Text + "", "تم حذف بيانات الشعبه المحدد بنجاح");
                AutoNum();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.RunNunQuary("delete  from Sections ", "تم حذف جميع بيانات الشعب  بنجاح");
                AutoNum();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            introw = 0;
            ShowData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (introw == 0)
            {
                MessageBox.Show("هذه اول شعبه", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                introw -= 1;
                ShowData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (introw >= tbl.Rows.Count - 1)
            {
                MessageBox.Show("هذه اخر شعبه", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                introw += 1;
                ShowData();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.RunReader("select * from Sections", "");
            introw = tbl.Rows.Count - 1;
            ShowData();
        }
    }
}
