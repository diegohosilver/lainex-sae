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
    public partial class frmConsultaProductos : Form
    {
        //Variables Zone//-----------------------------------------------------------------------------------

        private string _TablaBuscador = "Productos";
        private Int32 _Valor;

        private string _Descripcion;
            
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private Int32 _Cantidad;
        public Int32 Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
        

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
        public frmConsultaProductos()
        {
            InitializeComponent();
        }

        private void frmConsultaProductos_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Init_Combos();
            CargarGrilla();
        }

        private void Init_Combos()
        {
            //Categorias
            string sPais = "Select Codigo, Descripcion from Categorias Order By Codigo";
            SqlDataAdapter daPais = new SqlDataAdapter(sPais, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsPais = new DataSet();
            daPais.Fill(dsPais, "Categorias");
            cmbCategorias.DisplayMember = "Descripcion";
            cmbCategorias.ValueMember = "Codigo";
            cmbCategorias.DataSource = dsPais.Tables["Categorias"];
            General.SqlCon.Close();
        }

        private void CargarGrilla()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from " + TablaBuscador + " where CodCategoria = " + Convert.ToInt32(cmbCategorias.SelectedValue), General.SqlCon);
            DataSet ds = new DataSet();
            General.SqlCon.Open();
            da.Fill(ds, TablaBuscador);
            grilla.DataSource = ds.Tables[TablaBuscador];
            General.SqlCon.Close();
        }

        private void cmbCategorias_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void grilla_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Valor = Convert.ToInt32(grilla.Rows[e.RowIndex].Cells[0].Value);
            txtDesc.Text = grilla.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
