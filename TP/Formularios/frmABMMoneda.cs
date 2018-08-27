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
    public partial class frmABMMoneda : Form
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

        clsMoneda Paises = new clsMoneda();
        public frmABMMoneda()
        {
            InitializeComponent();
        }

        private void frmABMMoneda_Load_1(object sender, EventArgs e)
        {
            this.CenterToScreen();
            asignarID();
        }

        public void GetItems()
        {
            Paises.GetItemsById(Codigo);

            txtCodigo.Text = Paises.Codigo.ToString();
            txtDescripcion.Text = Paises.Descripcion.ToString();
        }

        private void Guardar_Pais()
        {
            Paises.Codigo = Convert.ToInt32(txtCodigo.Text);
            Paises.Descripcion = txtDescripcion.Text.ToString();

            if (EnableMOD)
            {
                if (Paises.Modify())
                {
                    MessageBox.Show("Moneda modificada con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo modificar la moneda!");
                }
            }
            else
            {
                if (Paises.Add())
                {
                    MessageBox.Show("Moneda cargada con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la moneda!");
                }
            }
        }

        void asignarID()
        {
            string str = ("Select * from " + Paises.Tabla);
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
            Guardar_Pais();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            General.lfrmBuscador = new frmBuscador();
            General.lfrmBuscador.MdiParent = General.frmParent;
            General.lfrmBuscador.TablaBuscador = Paises.Tabla;
            General.lfrmBuscador.Show();
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

    }
}
