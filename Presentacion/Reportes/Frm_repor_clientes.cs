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
    public partial class Frm_repor_clientes : Form
    {
        public Frm_repor_clientes()
        {
            InitializeComponent();
        }

        private void Frm_repor_clientes_Load(object sender, EventArgs e)
        {
            this.uSP_Listado_cliTableAdapter.Fill(this.dS_Reportes.USP_Listado_cli, cTexto: txt_p1.Text);
            this.reportViewer1.RefreshReport();
        }
    }
}
