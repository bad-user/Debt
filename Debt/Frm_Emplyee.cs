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

        private void Frm_Emplyee_Load(object sender, EventArgs e)
        {
            DtbDate.Text = DateTime.Now.ToShortDateString();
            AutoNum();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
