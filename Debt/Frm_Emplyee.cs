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
    public partial class Frm_Emplyee : Form
    {
        public Frm_Emplyee()
        {
            InitializeComponent();
        }

        DB db = new DB();
        DataTable tbl = new DataTable();
        int introw = 0;
        //-------------------------------------------------------------------------------------------------------------
        public void AutoNum()
        {
            tbl.Clear();
            tbl = db.RunReader("Select Max(Emp_ID) from Emplyees ", "");
            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
                txtEmpID.Text = "1";
            else
                txtEmpID.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            NudEmpSalary.Value = 1;
            txtEmpJob.Clear();
            txtEmpAddres.Clear();
            txtPhoneNum.Clear();
            txtEmpName.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
          


        }
        //-------------------------------------------------------------------------------------------------------------
        private void ShowData()
        {
            tbl.Clear();
            tbl = db.RunReader("select * from Emplyees", "");
            if ((tbl.Rows.Count <= 0))
            {
                MessageBox.Show("لا يوجد بيانات فى هذه الشاشة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    txtEmpID.Text = tbl.Rows[introw][0].ToString();
                    txtEmpName.Text = tbl.Rows[introw][1].ToString();
                    txtEmpJob.Text = tbl.Rows[introw][2].ToString();

                    this.Text = tbl.Rows[introw][3].ToString();
                    DateTime dt = DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
                    DtbDate.Value = dt;

                    NudEmpSalary.Value = Convert.ToDecimal(tbl.Rows[introw][4]);
                    txtPhoneNum.Text = tbl.Rows[introw][5].ToString();
                    txtEmpAddres.Text = tbl.Rows[introw][6].ToString();
                  

                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                }
                catch (Exception)
                { }

            }
        }

        private void FillEmployee() {
            cbxSearch.DataSource = db.RunReader("select * from Emplyees", "");
            cbxSearch.ValueMember = "Emp_ID";
            cbxSearch.DisplayMember = "Emp_Name";
        }

        private void Frm_Emplyee_Load(object sender, EventArgs e)
        {

            FillEmployee();
            DtbDate.Text = DateTime.Now.ToShortDateString();
            AutoNum();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEmpName.Text == "" )
            {
                MessageBox.Show("من فضلك اكمل البيانات", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                string d = DtbDate.Value.ToString("dd/MM/yyyy");
                db.RunNunQuary("insert into Emplyees values(" + txtEmpID.Text + " , N'" + txtEmpName.Text + "' , N'" + txtEmpJob.Text + "' ,  N'" + d + "'," + NudEmpSalary.Value + " , N'" + txtPhoneNum.Text + "'  , N'" + txtEmpAddres.Text + "')", "تم اضافة بيانات الموظف بنجاح");
                AutoNum();

            }
            catch (Exception)
            { }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AutoNum();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtEmpName.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                string d = DtbDate.Value.ToString("dd/MM/yyyy");

                db.RunNunQuary(" update Emplyees set Emp_Name='" + txtEmpName.Text + "' , Emp_Job= '" + txtEmpJob.Text + "' , Emp_StartDate='" + d + "', Emp_Salary=" + NudEmpSalary.Value + " , Emp_Phone = '" + txtPhoneNum.Text + "'  , Emp_Address='" + txtEmpAddres.Text + "' where Emp_ID= " + txtEmpID.Text + " ", "تم تعديل بيانات الموظف بنجاح");
                AutoNum();
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.RunNunQuary("delete from Emplyees where Emp_ID= " + txtEmpID.Text + " ", "تم حذف بيانات الموظف بنجاح");
                Frm_Emplyee_Load(null, null);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.RunNunQuary("delete  from Emplyees ", "تم حذف جميع البيانات بنجاح");

                NudEmpSalary.Value = 1;
                txtEmpJob.Clear();
                txtEmpAddres.Clear();
                txtPhoneNum.Clear();
                txtEmpName.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.RunReader("select * from Emplyees", "");
            introw = tbl.Rows.Count - 1;
            ShowData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (introw >= tbl.Rows.Count - 1)
                MessageBox.Show("هذا اخر موظف", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                introw += 1;
                ShowData();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (introw == 0)
                MessageBox.Show("هذا اول موظف", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                introw -= 1;
                ShowData();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            introw = 0;
            ShowData();
        }

        private void cbxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxSearch.Items.Count >= 1)
            {
                DataTable tblSearch = new DataTable();
                tblSearch.Clear();
                if (cbxSearch.SelectedIndex == 0)
                    tblSearch = db.RunReader("select * from Emplyees where Emp_ID=1 ", "");
                else
                    tblSearch = db.RunReader("select * from Emplyees where Emp_ID=" + cbxSearch.SelectedValue + " ", "");

                if ((tblSearch.Rows.Count <= 0))
                {
                }
                else
                {
                    try
                    {
                        txtEmpID.Text = tblSearch.Rows[0][0].ToString();
                        txtEmpName.Text = tblSearch.Rows[0][1].ToString();
                        txtEmpJob.Text = tblSearch.Rows[0][2].ToString();

                        this.Text = tblSearch.Rows[0][3].ToString();
                    DateTime dt = DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
                    DtbDate.Value = dt;

                    NudEmpSalary.Value = Convert.ToDecimal(tblSearch.Rows[0][4]);
                    txtPhoneNum.Text = tblSearch.Rows[0][5].ToString();
                    txtEmpAddres.Text = tblSearch.Rows[0][6].ToString();   
                    }

                    catch (Exception) { }
                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                }
            }
        }
    }
}
