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
    public partial class Frm_Department : Form
    {
        public Frm_Department()
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
            tbl = db.RunReader("Select Max(Dep_ID) from Department", "");
            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
                txtDepID.Text = "1";
            else
                txtDepID.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            txtDepName.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
        }
        private void ShowData()
        {
            tbl.Clear();
            tbl = db.RunReader("select * from Department", "");
            if ((tbl.Rows.Count <= 0))
            {
                MessageBox.Show("لا يوجد بيانات فى هذه الشاشة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtDepID.Text = tbl.Rows[introw][0].ToString();
                txtDepName.Text = tbl.Rows[introw][1].ToString();
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnDeleteAll.Enabled = true;
            }
        }
        private void Frm_Department_Load(object sender, EventArgs e)
        {
            AutoNum();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AutoNum();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDepName.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                db.RunNunQuary("insert into Department values(" + txtDepID.Text + ",N'" + txtDepName.Text + "')", "تم اضافه بيانات القسم بنجاح");
                AutoNum();
            }
            catch (Exception)
            { }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDepName.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                db.RunNunQuary("update Department set Dep_Name=N'" + txtDepName.Text + "' where Dep_ID=" + txtDepID.Text + "", "تم حفظ بيانات القسم بنجاح");
                AutoNum();
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.RunNunQuary("delete  from Department where Dep_ID=" + txtDepID.Text + "", "تم حذف بيانات القسم المحدد بنجاح");
                db.RunNunQuary("delete  from Section where Dep_ID=" + txtDepID.Text + "", "");
                
                AutoNum();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.RunNunQuary("delete  from Department ", "تم حذف جميع بيانات الاقسام  بنجاح");
                db.RunNunQuary("delete  from Section ", "");

                AutoNum();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {

            introw = 0;
            ShowData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (introw == 0)
                MessageBox.Show("هذا اول قسم", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                introw -= 1;
                ShowData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (introw >= tbl.Rows.Count - 1)
                MessageBox.Show("هذا اخر قسم", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                introw += 1;
                ShowData();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.RunReader("select * from Department", "");
            introw = tbl.Rows.Count - 1;
            ShowData();
        }
    }
}
