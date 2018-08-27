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
using TP.Clases;

namespace TP.Formularios
{
    public partial class frmABMProductos : Form
    {
        //Variables Zone//-----------------------------------------------------------------------------------

        private Int32 _Codigo;
        private bool _EnableMOD;

        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        public bool EnableMOD
        {
            get { return _EnableMOD; }
            set { _EnableMOD = value; }
        }

        //Variables Zone//-----------------------------------------------------------------------------------

        clsProductos Clientes = new clsProductos();
        public frmABMProductos()
        {
            InitializeComponent();
        }

        private void frmABMProductos_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Init_Combos();
            asignarID();
        }

        public void GetItems()
        {
            Clientes.GetItemsById(Codigo);

            txtCodigo.Text = Clientes.Codigo.ToString();
            txtDescripcion.Text = Clientes.Descripcion;
            txtDescripcion_Adicional.Text = Clientes.DescripcionAdicional;
            txtPrecioUnitario.Text = Clientes.PrecioUnitario.ToString();
            cmbCategoria.SelectedValue = Clientes.CodCategoria.ToString();
            cmbMarca.SelectedValue = Clientes.Marca.ToString();
            cmbProveedor.SelectedValue = Clientes.Proveedor.ToString();
            txtAncho.Text = Clientes.Ancho.ToString();
            txtAlto.Text = Clientes.Alto.ToString();
            txtLargo.Text = Clientes.Largo.ToString();
            txtPeso.Text = Clientes.Peso.ToString();
        }

        private void Guardar_Cliente()
        {
            Clientes.Codigo = Convert.ToInt32(txtCodigo.Text);
            Clientes.Descripcion = txtDescripcion.Text.ToString();
            Clientes.DescripcionAdicional = txtDescripcion_Adicional.Text.ToString();
            Clientes.PrecioUnitario = Convert.ToDecimal(txtPrecioUnitario.Text);
            Clientes.CodCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
            Clientes.Marca = Convert.ToInt32(cmbMarca.SelectedValue);
            Clientes.Proveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            Clientes.Ancho = Convert.ToDecimal(txtAncho.Text);
            Clientes.Alto = Convert.ToDecimal(txtAlto.Text);
            Clientes.Largo = Convert.ToDecimal(txtLargo.Text);
            Clientes.Peso = Convert.ToDecimal(txtPeso.Text);

            if (EnableMOD)
            {
                if (Clientes.Modify())
                {
                    MessageBox.Show("Producto modificado con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el producto!");
                }
            }
            else
            {
                if (Clientes.Add())
                {
                    MessageBox.Show("Producto cargado con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar el producto!");
                }
            }

        }
        
        private void Init_Combos()
        {
            //Proveedores
            string sProvincia = "Select id, RazonSocial from Proveedores Order By RazonSocial";
            SqlDataAdapter daProv = new SqlDataAdapter(sProvincia, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsProv = new DataSet();
            daProv.Fill(dsProv, "Proveedores");
            cmbProveedor.DisplayMember = "RazonSocial";
            cmbProveedor.ValueMember = "id";
            cmbProveedor.DataSource = dsProv.Tables["Proveedores"];
            General.SqlCon.Close();
            //Categorias
            string sCat = "Select Codigo, Descripcion from Categorias Order By Descripcion";
            SqlDataAdapter daCat = new SqlDataAdapter(sCat, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsCat = new DataSet();
            daCat.Fill(dsCat, "Categorias");
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Codigo";
            cmbCategoria.DataSource = dsCat.Tables["Categorias"];
            General.SqlCon.Close();
            //Marcas
            string sPais = "Select Codigo, Descripcion from Marcas where CodCategoria = " + Convert.ToInt32(cmbCategoria.SelectedValue) + " Order By Descripcion";
            SqlDataAdapter daPais = new SqlDataAdapter(sPais, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsPais = new DataSet();
            daPais.Fill(dsPais, "Marcas");
            cmbMarca.DisplayMember = "Descripcion";
            cmbMarca.ValueMember = "Codigo";
            cmbMarca.DataSource = dsPais.Tables["Marcas"];
            General.SqlCon.Close();
            

        }

        void asignarID()
        {
            string str = ("Select * from " + Clientes.Tabla);
            SqlCommand cmd = new SqlCommand(str, General.SqlCon);
            General.SqlCon.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            General.SqlCon.Close();

            txtCodigo.Text = (dt.Rows.Count + 1).ToString();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Guardar_Cliente();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            General.lfrmBuscador = new frmBuscador();
            General.lfrmBuscador.MdiParent = General.frmParent;
            General.lfrmBuscador.TablaBuscador = Clientes.Tabla;
            General.lfrmBuscador.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategoria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Marcas
            string sPais = "Select Codigo, Descripcion from Marcas where CodCategoria = " + Convert.ToInt32(cmbCategoria.SelectedValue) + " Order By Descripcion";
            SqlDataAdapter daPais = new SqlDataAdapter(sPais, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsPais = new DataSet();
            daPais.Fill(dsPais, "Marcas");
            cmbMarca.DisplayMember = "Descripcion";
            cmbMarca.ValueMember = "Codigo";
            cmbMarca.DataSource = dsPais.Tables["Marcas"];
            General.SqlCon.Close();
        }

    }
}
