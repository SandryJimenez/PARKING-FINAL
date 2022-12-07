using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Reportes
{
    public partial class Frm_rep_vehiculos : Form
    {
        public Frm_rep_vehiculos()
        {
            InitializeComponent();
        }

        private void Frm_rep_vehiculos_Load(object sender, EventArgs e)
        {
            this.uSP_Listado_vehiTableAdapter.Fill(this.dS_Reportes.USP_Listado_vehi, cTexto: txt_p2.Text);
            this.reportViewer1.RefreshReport();
        }
    }
}
