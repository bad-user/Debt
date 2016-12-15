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
    public partial class Frm_Items : Form
    {
        public Frm_Items()
        {
            InitializeComponent();
        }

        DB db = new DB();
        DataTable tbl = new DataTable();
        int introw = 0;
       
        public void AutoNum()
        {
            tbl.Clear();
            tbl = db.RunReader("Select Max(Item_ID) from Items ", "");
            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
                txtItemID.Text = "1";
            else
                txtItemID.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
                txtItemName.Clear();
                txtItemDetails.Clear();
                txtItemNotes.Clear();
          

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;



        }
        //-------------------------------------------------------------------------------------------------------------
        private void ShowData()
        {
            tbl.Clear();
            tbl = db.RunReader("select * from Items", "");
            if ((tbl.Rows.Count <= 0))
            {
                MessageBox.Show("لا يوجد بيانات فى هذه الشاشة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    txtItemID.Text = tbl.Rows[introw][0].ToString();
                    txtItemName.Text = tbl.Rows[introw][1].ToString();
                    txtItemDetails.Text = tbl.Rows[introw][2].ToString();
                    txtItemNotes.Text = tbl.Rows[introw][3].ToString();

                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                }
                catch (Exception)
                { }

            }
        }

        private void FillItems()
        {
            cbxSearchItem.DataSource = db.RunReader("select * from Items", "");
            cbxSearchItem.ValueMember = "Item_ID";
            cbxSearchItem.DisplayMember = "Item_Name";
        }

        private void Frm_Items_Load(object sender, EventArgs e)
        {
            FillItems();
            AutoNum();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.RunReader("select * from Items", "");
            introw = tbl.Rows.Count - 1;
            ShowData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (introw >= tbl.Rows.Count - 1)
                MessageBox.Show("هذه اخر مادة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                introw += 1;
                ShowData();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (introw == 0)
                MessageBox.Show("هذه اول مادة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtItemName.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {

                db.RunNunQuary("insert into Items values(" + txtItemID.Text + " , N'" + txtItemName.Text + "' , N'" + txtItemDetails.Text + "' , N'" + txtItemNotes.Text + "')", "تم اضافة بيانات المادة بنجاح");
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
            if (txtItemName.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {

                db.RunNunQuary(" update Items set Item_Name='" + txtItemName.Text + "' , Item_Detalis= '" + txtItemDetails.Text + "', Item_Notes='" + txtItemNotes.Text + "' where Item_ID= " + txtItemID.Text + " ", "تم تعديل بيانات المادة بنجاح");
                AutoNum();
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.RunNunQuary("delete from Items where Item_ID= " + txtItemID.Text + " ", "تم حذف بيانات المادة بنجاح");
                AutoNum();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.RunNunQuary("delete  from Items ", "تم حذف جميع البيانات بنجاح");


                AutoNum();
            }
            
        }

        private void cbxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxSearchItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSearchItem.Items.Count >= 1)
            {
                DataTable tblSearchItem = new DataTable();
                tblSearchItem.Clear();
                if (cbxSearchItem.SelectedIndex == 0)
                    tblSearchItem = db.RunReader("select * from Items where Item_ID=1 ", "");
                else
                    tblSearchItem = db.RunReader("select * from Items where Item_ID=" + cbxSearchItem.SelectedValue + " ", "");

                if ((tblSearchItem.Rows.Count <= 0))
                {
                }
                else
                {
                    try
                    {
                        txtItemID.Text = tblSearchItem.Rows[0][0].ToString();
                        txtItemName.Text = tblSearchItem.Rows[0][1].ToString();
                        txtItemDetails.Text = tblSearchItem.Rows[0][2].ToString();
                        txtItemNotes.Text = tblSearchItem.Rows[0][3].ToString();

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
