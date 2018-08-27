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
    public partial class frmABMClientes : Form
    {
        //Variables Zone//-----------------------------------------------------------------------------------

        private Int32 _Codigo;
        private Int32 _xPais;
        private bool _EnableMOD;

        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        public Int32 xPais
        {
            get { return _xPais; }
            set { _xPais = value; }
        }
        public bool EnableMOD
        {
            get { return _EnableMOD; }
            set { _EnableMOD = value; }
        }

        //Variables Zone//-----------------------------------------------------------------------------------

        clsClientes Clientes = new clsClientes();
        public frmABMClientes()
        {
            InitializeComponent();
        }

        private void frmABMClientes_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Init_Combos();
            asignarID();
        }

        public void GetItems()
        {
            Clientes.GetItemsById(Codigo);

            txtID.Text = Clientes.Id.ToString();
            txtApellido.Text = Clientes.Apellido;
            txtNombre.Text = Clientes.Nombre;
            txtRazonSocial.Text = Clientes.RazonSocial;
            txtCUIT.Text = Clientes.Cuit;
            txtDomicilio.Text = Clientes.Domicilio;
            txtNumero.Text = Clientes.Nro.ToString();
            txtPiso.Text = Clientes.Piso;
            txtDepto.Text = Clientes.Depto;
            txtLocalidad.Text = Clientes.Localidad;
            cmbProvincia.SelectedValue = Clientes.IdProvincia.ToString();
            cmbPais.SelectedValue = Clientes.IdPais.ToString();
            txtWebSite.Text = Clientes.WebSite;
            txtEmail.Text = Clientes.Email;
            txtTelefonos.Text = Clientes.Telefono;
            txtInterno.Text = Clientes.Interno;
            txtFax.Text = Clientes.Fax;
            txtCP.Text = Clientes.CP.ToString();
        }

        private void Guardar_Cliente()
        {
            Clientes.Id = Convert.ToInt32(txtID.Text);
            Clientes.Nombre = txtNombre.Text.ToString();
            Clientes.Apellido = txtApellido.Text.ToString();
            Clientes.RazonSocial = txtRazonSocial.Text.ToString();
            Clientes.Cuit = this.txtCUIT.Text.ToString();
            Clientes.Domicilio = txtDomicilio.Text.ToString();
            Clientes.Nro = Convert.ToInt32(txtNumero.Text);
            Clientes.Piso = txtPiso.Text.ToString();
            Clientes.Depto = txtDepto.Text.ToString();
            Clientes.Localidad = txtLocalidad.Text.ToString();
            Clientes.IdProvincia = Convert.ToInt32(cmbProvincia.SelectedValue);
            Clientes.IdPais = Convert.ToInt32(cmbPais.SelectedValue);
            Clientes.WebSite = txtWebSite.Text.ToString();
            Clientes.Email = txtEmail.Text.ToString();
            Clientes.Telefono = txtTelefonos.Text.ToString();
            Clientes.Interno = txtInterno.Text.ToString();
            Clientes.Fax = txtFax.Text.ToString();
            Clientes.CP = Convert.ToInt32(txtCP.Text);

            if (EnableMOD)
            {
                if (Clientes.Modify())
                {
                    MessageBox.Show("Cliente modificado con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el cliente!");
                }
            }
            else
            {
                if (Clientes.Add())
                {
                    MessageBox.Show("Cliente cargado con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar el cliente!");
                }
            }

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Guardar_Cliente();
        }

        
        private void Init_Combos()
        {
            //Paises
            string sPais = "Select Codigo, Descripcion from Paises Order By Descripcion";
            SqlDataAdapter daPais = new SqlDataAdapter(sPais, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsPais = new DataSet();
            daPais.Fill(dsPais, "Paises");
            cmbPais.DisplayMember = "Descripcion";
            cmbPais.ValueMember = "Codigo";
            cmbPais.DataSource = dsPais.Tables["Paises"];
            General.SqlCon.Close();

            xPais = Convert.ToInt32(cmbPais.SelectedValue);

            //Provincias
            string sProvincia = "Select Codigo, Descripcion from Provincias where CodigoPais = " + xPais + " Order By Descripcion";
            SqlDataAdapter daProv = new SqlDataAdapter(sProvincia, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsProv = new DataSet();
            daProv.Fill(dsProv, "Provincias");
            cmbProvincia.DisplayMember = "Descripcion";
            cmbProvincia.ValueMember = "Codigo";
            cmbProvincia.DataSource = dsProv.Tables["Provincias"];
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

            txtID.Text = (dt.Rows.Count + 1).ToString();
        }

        void asignarPais()
        {

                xPais = Convert.ToInt32(cmbPais.SelectedValue);


            //Provincias
            string sProvincia = "Select Codigo, Descripcion from Provincias  where CodigoPais = " + xPais + " Order By Descripcion";
            SqlDataAdapter daProv = new SqlDataAdapter(sProvincia, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsProv = new DataSet();
            daProv.Fill(dsProv, "Provincias");
            cmbProvincia.DisplayMember = "Descripcion";
            cmbProvincia.ValueMember = "Codigo";
            cmbProvincia.DataSource = dsProv.Tables["Provincias"];
            General.SqlCon.Close();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            General.lfrmBuscador = new frmBuscador();
            General.lfrmBuscador.MdiParent = General.frmParent;
            General.lfrmBuscador.TablaBuscador = Clientes.Tabla;
            General.lfrmBuscador.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPais_SelectionChangeComitted(object sender, EventArgs e)
        {
            asignarPais();
        }
    }
}
