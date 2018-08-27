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
    public partial class frmABMMarcas : Form
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
        public bool EnableMOD
        {
            get { return _EnableMOD; }
            set { _EnableMOD = value; }
        }
        public Int32 xPais
        {
            get { return _xPais; }
            set { _xPais = value; }
        }

        //Variables Zone//-----------------------------------------------------------------------------------

        clsMarcas Paises = new clsMarcas();
        public frmABMMarcas()
        {
            InitializeComponent();
        }

        private void frmABMMarcas_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Init_Combos();
            asignarID();
        }

        void asignarPais()
        {
            xPais = Convert.ToInt32(cmbPais.SelectedValue);

            asignarID();
        }

        private void Init_Combos()
        {
            //Paises
            string sPais = "Select Codigo, Descripcion from Categorias Order By Descripcion";
            SqlDataAdapter daPais = new SqlDataAdapter(sPais, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsPais = new DataSet();
            daPais.Fill(dsPais, "Categorias");
            cmbPais.DisplayMember = "Descripcion";
            cmbPais.ValueMember = "Codigo";
            cmbPais.DataSource = dsPais.Tables["Categorias"];
            General.SqlCon.Close();

            //Asignar COD Pais
            xPais = Convert.ToInt32(cmbPais.SelectedValue);
        }

        public void GetItems()
        {
            Paises.GetItemsById(Codigo);

            txtCodigo.Text = Paises.Codigo.ToString();
            txtDescripcion.Text = Paises.Descripcion.ToString();
            cmbPais.SelectedValue = Paises.CodCategoria.ToString();
        }

        private void Guardar_Pais()
        {
            Paises.Codigo = Convert.ToInt32(txtCodigo.Text);
            Paises.CodCategoria = Convert.ToInt32(cmbPais.SelectedValue);
            Paises.Descripcion = txtDescripcion.Text.ToString();

            if (EnableMOD)
            {
                if (Paises.Modify())
                {
                    MessageBox.Show("Marca modificada con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo modificar la marca!");
                }
            }
            else
            {
                if (Paises.Add())
                {
                    MessageBox.Show("Marca cargada con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la marca!");
                }
            }
        }

        void asignarID()
        {
            string str = ("Select * from " + Paises.Tabla + " where CodCategoria = " + xPais);
            SqlCommand cmd = new SqlCommand(str, General.SqlCon);
            General.SqlCon.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            General.SqlCon.Close();

            txtCodigo.Text = (dt.Rows.Count + 1).ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Guardar_Pais();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            General.lfrmBuscador = new frmBuscador();
            General.lfrmBuscador.MdiParent = General.frmParent;
            General.lfrmBuscador.TablaBuscador = Paises.Tabla;
            General.lfrmBuscador.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPais_SelectionChangeCommitted(object sender, EventArgs e)
        {
            asignarPais();
        }
    }
}
