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
    public partial class frmABMProvincias : Form
    {

        //Variables Zone//-----------------------------------------------------------------------------------

        private bool _EnableMOD = false;
        private Int32 _Codigo;
        private Int32 _xPais;
        private string _Consulta;

        public bool EnableMOD
        {
            get { return _EnableMOD; }
            set { _EnableMOD = value; }
        }

        public string Consulta
        {
            get { return _Consulta; }
            set { _Consulta = value; }
        }

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

        //Variables Zone//-----------------------------------------------------------------------------------

        clsProvincias Provincias = new clsProvincias();
        public frmABMProvincias()
        {
            InitializeComponent();
        }

        private void frmABMProvincias_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Init_Combos();
            asignarID();
        }

        public void GetItems()
        {
            Provincias.GetItemsById(Codigo, xPais);

            txtCodigo.Text = Provincias.Codigo.ToString();
            cmbPais.SelectedValue = Provincias.CodigoPais.ToString();
            txtDescripcion.Text = Provincias.Descripcion.ToString();
        }

        private void Guardar_Provincia()
        {
            Provincias.Codigo = Convert.ToInt32(txtCodigo.Text);
            Provincias.CodigoPais = Convert.ToInt32(cmbPais.SelectedValue);
            Provincias.Descripcion = txtDescripcion.Text.ToString();

            if (EnableMOD)
            {
                if (Provincias.Modify())
                {
                    MessageBox.Show("Provincia modificada con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo modificar la provincia!");
                }
            }
            else
            {
                if (Provincias.Add())
                {
                    MessageBox.Show("Provincia cargada con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la provincia!");
                }
            }
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
            
            //Asignar COD Pais
            xPais = Convert.ToInt32(cmbPais.SelectedValue);
        }

        void asignarPais()
        {
                xPais = Convert.ToInt32(cmbPais.SelectedValue);

            asignarID();
        }

        void asignarID()
        {
            string str = ("Select * from " + Provincias.Tabla + " where CodigoPais = " + xPais);
            SqlCommand cmd = new SqlCommand(str, General.SqlCon);
            General.SqlCon.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            General.SqlCon.Close();

            txtCodigo.Text = (dt.Rows.Count + 1).ToString();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Guardar_Provincia();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            General.lfrmBuscador = new frmBuscador();
            General.lfrmBuscador.MdiParent = General.frmParent;
            General.lfrmBuscador.TablaBuscador = Provincias.Tabla;
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