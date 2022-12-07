using Datos;
using Entidades;
using Logica;
using LogicaS;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;

namespace Presentacion
{
    public partial class FrmParqueadero : Form
    {
        servicioTarifa serviciotarifa;
        servicioCliente servicioCliente;
        servicioVehiculo servicioVehiculo;
        servicioParqueadero servicioAlquiler;
        public FrmParqueadero()
        {
            InitializeComponent();
            servicioCliente = new servicioCliente();
            servicioVehiculo = new servicioVehiculo();
            servicioAlquiler = new servicioParqueadero();
            serviciotarifa = new servicioTarifa() ;
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            var respuesta = servicioVehiculo.BuscarPorid(txtcedula.Text);
            ver2(respuesta.Vehiculo);
        }


        RepositorioParqueadero rv = new RepositorioParqueadero();
        public void cargar_tipo_vehiculo()
        {
            cmbtipovehiculo.DataSource = rv.Tipovehiculo();
            cmbtipovehiculo.DisplayMember = "tipo";
            cmbtipovehiculo.ValueMember = "id";
        }

        void ver_vehiculo(Tarifas tar)
        {
            if (tar == null)
            {
                return;
            }
            txtvalorhora.Text = tar.precioxhora;
        }




        void ver2(Vehiculo veh)
        {
            if (veh == null)
            {
                return;
            }
            txtplaca.Text = veh.PlacaVehiculo;
            txtcedula.Text = veh.cedula;
            txtmarca.Text = veh.Marca;
            txtmodelo.Text = veh.modelo;
            txtColor.Text = veh.color;
            fechallegada.Text =Convert.ToString(veh.fechallegada);
        }
        private Parqueadero CrearPago()
        {
            //DateTime fechauno = Convert.ToDateTime(fechallegada.Value);
            DateTime fechauno = Convert.ToDateTime( fechallegada.Value);
            DateTime fechados = Convert.ToDateTime(fechasalida.Value);
            //DateTime fechados = Convert.ToDateTime(fechasalida.Value);
            TimeSpan diffechas = fechados - fechauno;
            Double horas = diffechas.TotalHours;
            Random rnd = new Random();
            string random = txtcodfactura.Text = Convert.ToString(rnd.Next(0, 25000));
            Double Total1 =Convert.ToInt32(txtvalorhora.Text) * horas;
            double resultado = Math.Round(Total1);
            DateTime formatohora = DateTime.Now;
        

            Parqueadero parqueo = new Parqueadero
            {
                IdParqueadero = Convert.ToInt32(random),
                cedula = txtcedula.Text,
                PlacaVehiculo = txtplaca.Text,
                Marca = txtmarca.Text,
                modelo = txtmodelo.Text,
                color = txtColor.Text,
                fechallegada =fechallegada.Value,
                FechaSalida = fechasalida.Value,
                TipoVeh= cmbtipovehiculo.Text,
                valorPorHora =int.Parse(txtvalorhora.Text),
                Total = resultado,
            };
            txttotal.Text = parqueo.Total.ToString();
        
            return parqueo;
        }
       
        private void BtnGuardar_Click_1(object sender, EventArgs e)
        {
            string message = servicioAlquiler.Guardar(CrearPago());

            MessageBox.Show(message);
            //generar ticket factura
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += imprimir;
            printDocument1.Print();
        }

        private void FrmAlquiler_Load(object sender, EventArgs e)
        {
            cargar_tipo_vehiculo();
        }

        private void btnbuscarveh_Click(object sender, EventArgs e)
        {
            var respuesta = serviciotarifa.Buscarporid(cmbtipovehiculo.Text);
            ver_vehiculo(respuesta.Tarifa);
        }


       


        private void imprimir(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 14);
            int ancho = 300;
            int y = 20;

            e.Graphics.DrawString("------ Ticket de Factura -------", font, Brushes.Black, new RectangleF(0,y+=20,ancho,20));
            e.Graphics.DrawString("Factura #"+txtcodfactura.Text, font, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Cliente: " + txtcedula.Text, font, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("------ Vehiculo --------: " , font, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Placa:  " + txtplaca.Text, font, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Marca:  " + txtmarca.Text, font, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("------ Total --------: ", font, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Precio X Hora:  " +"$"+txtvalorhora.Text, font, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Total:  " +"$"+ txttotal.Text, font, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Gracias Por su Visita", font, Brushes.Black, new RectangleF(0, y += 40, ancho, 20));

        }
    }
}
