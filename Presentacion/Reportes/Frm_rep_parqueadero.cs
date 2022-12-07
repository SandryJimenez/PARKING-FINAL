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
    public partial class Frm_rep_parqueadero : Form
    {
        public Frm_rep_parqueadero()
        {
            InitializeComponent();
        }

        private void Frm_rep_parqueadero_Load(object sender, EventArgs e)
        {
            this.uSP_Listado_ParqueaderoTableAdapter.Fill(this.dS_Reportes.USP_Listado_Parqueadero, cTexto: txt_p3.Text);

            this.reportViewer1.RefreshReport();
        }
    }
}
