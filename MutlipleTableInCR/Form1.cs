using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutlipleTableInCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=HP-NETBOOK\SQLEXPRESS01;Initial Catalog=AdventureWorks2016;
                                                    User ID=sa;Password=appforms@123;integrated Security=True;");
            sqlCon.Open();
            SqlDataAdapter sqlDa1 = new SqlDataAdapter("ProductsAtLowStock",sqlCon);
            SqlDataAdapter sqlDa2 = new SqlDataAdapter("TopCostlyProducts", sqlCon);
            sqlDa1.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa2.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsProductLowStock = new DataSet();
            sqlDa1.Fill(dsProductLowStock);
            DataSet dsCostlyProducts = new DataSet();
            sqlDa1.Fill(dsCostlyProducts);
            sqlCon.Close();

            CrystalReport.crptProductReport crptProduct = new CrystalReport.crptProductReport();
            crptProduct.Database.Tables["ProductsAtLowStock"].SetDataSource(dsProductLowStock.Tables[0]);
            crptProduct.Database.Tables["TopCostlyProducts"].SetDataSource(dsProductLowStock.Tables[0]);
            DataTable dtbl = new DataTable();
            //dtbl.Columns.Add("WaterMarkPath", typeof(string));
            //dtbl.Rows.Add(@"C:\Users\admin\Desktop\w2.jpg");
            //crptProduct.Database.Tables["ReportDetails"].SetDataSource(dtbl);
            crvViewer.ReportSource = null;
            crvViewer.ReportSource = crptProduct;
        }

  
    }
}
