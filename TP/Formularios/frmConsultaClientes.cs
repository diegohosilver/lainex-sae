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
    public partial class frmConsultaClientes : Form
    {
        //Variables Zone//-----------------------------------------------------------------------------------
        private Int32 _Valor;
        public Int32 Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        //Variables Zone//-----------------------------------------------------------------------------------
        public frmConsultaClientes()
        {
            InitializeComponent();
        }

        private void frmConsultaClientes_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            CargarGrilla();
        }
        void CargarGrilla()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Clientes", General.SqlCon);
            DataSet ds = new DataSet();
            General.SqlCon.Open();
            da.Fill(ds, "Clientes");
            grilla.DataSource = ds.Tables["Clientes"];
            General.SqlCon.Close();
        }

        private void grilla_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Valor = Convert.ToInt32(grilla.Rows[e.RowIndex].Cells[0].Value);
            txtID.Text = grilla.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtRS.Text = grilla.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            General.lfrmABMPedVta.idCliente = Valor;
            General.lfrmABMPedVta.GetCliente();
            this.Close();
        }

    }
}
