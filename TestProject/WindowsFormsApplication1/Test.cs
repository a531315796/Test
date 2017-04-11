using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            dataGridView1.Dock = DockStyle.Fill;
            BuildingDAL dal = new BuildingDAL();
            DataTable dt = dal.GetAll(); 
            dataGridView1.DataSource = dt;
        }
    }
}
