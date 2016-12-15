using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Dbet
{
    class DB
    {
        SqlConnection conn = new SqlConnection(@"server=.\SQLExpress;Initial Catalog=Debt;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public void SetCommand(string stmt)
        {
            cmd.Connection = conn;
            cmd.CommandText = stmt;
        }
        public bool RunNunQuary(string stmt, string Message)
        {
            try
            {
                SetCommand(stmt);
                conn.Open();
                cmd.ExecuteNonQuery();
                if (Message != "")
                    MessageBox.Show(Message, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        //هذه الداله مسؤله عن اى عملية بحث او استعلام داخل البرنامج
        public DataTable RunReader(string stmt, string Message)
        {
            //متغير من نوع داتا تبل ليحمل الجدول او الاستعلام الراجع من الداتا بيز
            DataTable tbl = new DataTable();
            try
            {
                //استدعاء داله تهيئه الاتصال
                SetCommand(stmt);
                //فتح قاعدة البيانات
                conn.Open();
                //وضع ناتج الاستعلام فى متغير ال tbl
                tbl.Load(cmd.ExecuteReader());

            }
            catch (Exception)
            {
            }
            finally
            { conn.Close(); }
            if (Message != "")
                MessageBox.Show(Message, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return tbl;
        }




    }
}

