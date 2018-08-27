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
    public partial class frmABMPedVta : Form
    {
        //Get Clientes Zone//-----------------------------------------------------------------------------------
        private Int32 _idCliente;
        clsClientes Clientes = new clsClientes();

        public Int32 idCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public void GetCliente()
        {
            Clientes.GetItemsById(idCliente);

            txtnombre.Text = Clientes.RazonSocial.ToString();
            txtid.Text = Clientes.Id.ToString();

        }

        //Get Clientes Zone//-----------------------------------------------------------------------------------

        //Nuevo registro Zone//-----------------------------------------------------------------------------------
        private Int32 _CodigoProducto;
        private string _Descripcion;
        private Int32 _Cantidad;
        private Int32 _CodCategoria;
        private Int32 _CodMarca;
        private decimal _Monto;
        private Int32 _Row;

        public Int32 CodigoProducto
        {
            get { return _CodigoProducto; }
            set { _CodigoProducto = value; }
        }

        public decimal Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public Int32 Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public Int32 CodCategoria
        {
            get { return _CodCategoria; }
            set { _CodCategoria = value; }
        }

        public Int32 CodMarca
        {
            get { return _CodMarca; }
            set { _CodMarca = value; }
        }

        public Int32 Row
        {
            get { return _Row; }
            set { _Row = value; }
        }

        //Nuevo registro Zone//-----------------------------------------------------------------------------------

        //Variables Zone//-----------------------------------------------------------------------------------

        private Int32 _IdPedido;
        private Int32 _Codigo;
        private bool _EnableMOD;
        
        DataTable dt = new DataTable();

        public Int32 IdPedido
        {
            get { return _IdPedido; }
            set { _IdPedido = value; }
        }
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

        clsFacturasVtas Facturas = new clsFacturasVtas();
        public frmABMPedVta()
        {
            InitializeComponent();
        }

        private void frmABMPedVta_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Init_Combos();
            asignarID();
            CargarGrilla();
            CrearDataTable();
        }

        void asignarID()
        {
            string str = ("Select * from " + Facturas.Tabla);
            SqlCommand cmd = new SqlCommand(str, General.SqlCon);
            General.SqlCon.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            General.SqlCon.Close();

            txtPedido.Text = (dt.Rows.Count + 1).ToString();
        }

        public void GetItems()
        {
            Facturas.GetItemsById(Codigo);

            txtPedido.Text = Facturas.id.ToString();
            txtFecha.Value = Facturas.Fecha;
            txtnombre.Text = Facturas.RSCliente.ToString();
            txtid.Text = Facturas.idCliente.ToString();
            txtVencimiento.Value = Facturas.Vencimiento;
            cmbEstado.SelectedValue = Facturas.Estado;
            cmbMoneda.SelectedValue = Facturas.Moneda;
            txtMontoTotal.Text = Facturas.MontoTotal.ToString();
        }

        private void Guardar_Factura()
        {
            Facturas.id = Convert.ToInt32(txtPedido.Text);
            Facturas.idCliente = Convert.ToInt32(txtid.Text);
            Facturas.Fecha = txtFecha.Value;
            Facturas.RSCliente = txtnombre.Text;
            Facturas.Vencimiento = txtVencimiento.Value;
            Facturas.Estado = Convert.ToInt32(cmbEstado.SelectedValue);
            Facturas.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
            Facturas.MontoTotal = Convert.ToDecimal(txtMontoTotal.Text);

            if (EnableMOD)
            {
                if (Facturas.Modify())
                {
                    MessageBox.Show("Factura modificada con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo modificar la factura!");
                }
            }
            else
            {
                if (Facturas.Add())
                {
                    MessageBox.Show("Factura cargada con exito!");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la factura!");
                }
            }

        }

        void CrearDataTable()
        {

            dt.Columns.Add(new DataColumn("idPedido", typeof(Int32)));
            dt.Columns.Add(new DataColumn("idProducto", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Descripcion", typeof(string)));
            dt.Columns.Add(new DataColumn("CodCategoria", typeof(Int32)));
            dt.Columns.Add(new DataColumn("CodMarca", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Monto", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Cantidad", typeof(Int32)));
        
        }

        void NuevoRegistro()
        {
            DataRow dr = dt.NewRow();

            dr["idPedido"] = IdPedido;
            dr["idProducto"] = CodigoProducto;
            dr["Descripcion"] = Descripcion;
            dr["CodCategoria"] = CodCategoria;
            dr["CodMarca"] = CodMarca;
            dr["Monto"] = Monto;
            dr["Cantidad"] = Cantidad;

            dt.Rows.Add(dr);
        }

        void EscribirRegistro()
        {
            this.grillaDetalle.Visible = true;

            grillaDetalle.DataSource = dt;
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
            //Monedas
            string sMon = "Select Codigo, Descripcion from Moneda Order By Codigo";
            SqlDataAdapter daMon = new SqlDataAdapter(sMon, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsMon = new DataSet();
            daMon.Fill(dsMon, "Moneda");
            cmbMoneda.DisplayMember = "Descripcion";
            cmbMoneda.ValueMember = "Codigo";
            cmbMoneda.DataSource = dsMon.Tables["Moneda"];
            General.SqlCon.Close();
            //Estados
            string sEst = "Select Codigo, Descripcion from Estados Order By Codigo";
            SqlDataAdapter daEst = new SqlDataAdapter(sEst, General.SqlCon);
            General.SqlCon.Open();
            DataSet dsEst = new DataSet();
            daEst.Fill(dsEst, "Estados");
            cmbEstado.DisplayMember = "Descripcion";
            cmbEstado.ValueMember = "Codigo";
            cmbEstado.DataSource = dsEst.Tables["Estados"];
            General.SqlCon.Close();
        }

        private void CargarGrilla()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Productos where CodCategoria = " + Convert.ToInt32(cmbCategorias.SelectedValue), General.SqlCon);
            DataSet ds = new DataSet();
            General.SqlCon.Open();
            da.Fill(ds, "Productos");
            grilla.DataSource = ds.Tables["Productos"];
            General.SqlCon.Close();
        }

        private void cmbCategorias_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void grilla_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get Descripcion
            txtDesc.Text = grilla.Rows[e.RowIndex].Cells[1].Value.ToString();

            //Get value of ID Factura
            IdPedido = Convert.ToInt32(txtPedido.Text);

            //Get Values of the datagrid
            CodigoProducto = Convert.ToInt32(grilla.Rows[e.RowIndex].Cells[0].Value);
            Descripcion = grilla.Rows[e.RowIndex].Cells[1].Value.ToString();
            CodCategoria = Convert.ToInt32(grilla.Rows[e.RowIndex].Cells[4].Value);
            CodMarca = Convert.ToInt32(grilla.Rows[e.RowIndex].Cells[5].Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Llenamos los txtBox
            Cantidad = Convert.ToInt32(txtCant.Text);
            Monto = Convert.ToInt32(txtCant.Text) * Convert.ToDecimal(txtPrecioUnitario.Text);

            //Agregamos registro al datagrid
            EscribirRegistro();
            NuevoRegistro();

            //Calculamos el monto total
            calcularMontoTotal();
           

        }

        void calcularMontoTotal()
        {
            decimal MontoTotal = new decimal();

            for (int i = 0; i < grillaDetalle.Rows.Count; ++i)
            {
                MontoTotal += Convert.ToDecimal(grillaDetalle.Rows[i].Cells[5].Value);
            }
            txtMontoTotal.Text = MontoTotal.ToString();
        }

        private void grillaDetalle_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           Row = grillaDetalle.CurrentCell.RowIndex;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            grillaDetalle.Rows.RemoveAt(Row);
            calcularMontoTotal();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            General.lfrmConsultaClientes = new frmConsultaClientes();
            General.lfrmConsultaClientes.MdiParent = General.frmParent;
            General.lfrmConsultaClientes.Show();
        }

        void GuardarDetalle()
        {
            using (var bulkCopy = new SqlBulkCopy(General.SqlConString, SqlBulkCopyOptions.KeepIdentity))
            {
                //Pasamos el datatable del detalle a la base de datos
                foreach (DataColumn col in dt.Columns)
                {
                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                }
                bulkCopy.BulkCopyTimeout = 600;
                bulkCopy.DestinationTableName = "DetalleFacturaVta";
                bulkCopy.WriteToServer(dt);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Guardar_Factura();
            GuardarDetalle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            General.lfrmBuscador = new frmBuscador();
            General.lfrmBuscador.TablaBuscador = Facturas.Tabla;
            General.lfrmBuscador.MdiParent = General.frmParent;
            General.lfrmBuscador.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
