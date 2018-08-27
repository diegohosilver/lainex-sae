using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TP.Formularios
{
    public partial class frmBuscador : Form
    {
        //Variables Zone//-----------------------------------------------------------------------------------

        private string _TablaBuscador;
        private Int32 _Valor;
        

        public string TablaBuscador
        {
            get { return _TablaBuscador; }
            set { _TablaBuscador = value; }
        }

        public Int32 Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        //Variables Zone//-----------------------------------------------------------------------------------

        //Variables per Table//------------------------------------------------------------------------------

        //Provincias//
        private Int32 _CodPais;

        public Int32 CodPais
        {
            get { return _CodPais; }
            set { _CodPais = value; }
        }


        //Variables per Table//------------------------------------------------------------------------------

        public frmBuscador()
        {
            InitializeComponent();
        }

        
        private void frmBuscador_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            CargarGrilla();
        }

        void CargarGrilla()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from " + TablaBuscador, General.SqlCon);
            DataSet ds = new DataSet();
            General.SqlCon.Open();
            da.Fill(ds, TablaBuscador);
            grilla.DataSource = ds.Tables[TablaBuscador];
            General.SqlCon.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grilla_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Valor = Convert.ToInt32(grilla.Rows[e.RowIndex].Cells[0].Value);

            //Dependiendo de la tabla, devuelve el 2do valor necesario
            switch (TablaBuscador)
            {
                case "Provincias":
                    CodPais = Convert.ToInt32(grilla.Rows[e.RowIndex].Cells[2].Value);
                break;
            }

            MessageBox.Show("ID " + Valor.ToString() + " seleccionado!");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            switch (TablaBuscador)
            {
                case "Clientes":
                General.lfrmABMClientes.Codigo = Valor;
                General.lfrmABMClientes.GetItems();
                General.lfrmABMClientes.EnableMOD = true;
                break;

                case "Proveedores":
                General.lfrmABMProveedores.Codigo = Valor;
                General.lfrmABMProveedores.GetItems();
                General.lfrmABMProveedores.EnableMOD = true;
                break;

                case "Paises":
                General.lfrmABMPaises.Codigo = Valor;
                General.lfrmABMPaises.GetItems();
                General.lfrmABMPaises.EnableMOD = true;
                break;

                case "Provincias":
                General.lfrmABMProvincias.Codigo = Valor;
                General.lfrmABMProvincias.xPais = CodPais;
                General.lfrmABMProvincias.GetItems();
                General.lfrmABMProvincias.EnableMOD = true;
                break;

                case "Categorias":
                General.lfrmABMCategorias.Codigo = Valor;
                General.lfrmABMCategorias.GetItems();
                General.lfrmABMCategorias.EnableMOD = true;
                break;

                case "Marcas":
                General.lfrmABMMarcas.Codigo = Valor;
                General.lfrmABMMarcas.GetItems();
                General.lfrmABMMarcas.EnableMOD = true;
                break;

                case "Productos":
                General.lfrmABMProductos.Codigo = Valor;
                General.lfrmABMProductos.GetItems();
                General.lfrmABMProductos.EnableMOD = true;
                break;

                case "Moneda":
                General.lfrmABMMoneda.Codigo = Valor;
                General.lfrmABMMoneda.GetItems();
                General.lfrmABMMoneda.EnableMOD = true;
                break;

                case "FacturasVtas":
                General.lfrmABMPedVta.Codigo = Valor;
                General.lfrmABMPedVta.GetItems();
                General.lfrmABMPedVta.EnableMOD = true;
                break;
            }

            this.Close();

        }

        private void grilla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
