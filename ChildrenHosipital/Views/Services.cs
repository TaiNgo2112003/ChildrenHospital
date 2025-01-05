using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildrenHosipital.Views
{
    public partial class Services : Form
    {
        public Services()
        {
            InitializeComponent();
        }

        private void Services_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'serviceDataSet.Services' table. You can move, or remove it, as needed.
            this.servicesTableAdapter.Fill(this.serviceDataSet.Services);
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ Title Bar

        }
    }
}
