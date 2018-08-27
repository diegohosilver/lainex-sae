using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace TP.Clases
{
    class clsProductos
    {
        private string _Tabla = "Productos";
        private string _Descripcion;
        private string _DescripcionAdicional;
        private Int32 _Codigo;
        private Int32 _CodCategoria;
        private Int32 _Marca;
        private Int32 _Proveedor;
        private decimal _Ancho;
        private decimal _Alto;
        private decimal _Largo;
        private decimal _Peso;
        private decimal _PrecioUnitario;

        public string Tabla
        {
            get { return _Tabla; }
            set { _Tabla = value; }
        }

        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string DescripcionAdicional
        {
            get { return _DescripcionAdicional; }
            set { _DescripcionAdicional = value; }
        }

        public Int32 CodCategoria
        {
            get { return _CodCategoria; }
            set { _CodCategoria = value; }
        }

        public Int32 Marca
        {
            get { return _Marca; }
            set { _Marca = value; }
        }

        public Int32 Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }

        public decimal Ancho
        {
            get { return _Ancho; }
            set { _Ancho = value; }
        }

        public decimal Alto
        {
            get { return _Alto; }
            set { _Alto = value; }
        }

        public decimal Largo
        {
            get { return _Largo; }
            set { _Largo = value; }
        }

        public decimal Peso
        {
            get { return _Peso; }
            set { _Peso = value; }
        }

        public decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }

        public bool Add()
        {
            bool Done = true; ;
            SqlCommand cmd = new SqlCommand(Tabla + "_A", General.SqlCon);
            General.SqlCon.Open();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = General.SqlCon;
                cmd.Parameters.AddWithValue("@Codigo", Codigo);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@DescripcionAdicional", DescripcionAdicional);
                cmd.Parameters.AddWithValue("@PrecioUnitario", PrecioUnitario);
                cmd.Parameters.AddWithValue("@CodCategoria", CodCategoria);
                cmd.Parameters.AddWithValue("@Marca", Marca);
                cmd.Parameters.AddWithValue("@Proveedor", Proveedor);
                cmd.Parameters.AddWithValue("@Ancho", Ancho);
                cmd.Parameters.AddWithValue("@Alto", Alto);
                cmd.Parameters.AddWithValue("@Largo", Largo);
                cmd.Parameters.AddWithValue("@Peso", Peso);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException SQLeX)
            {
                MessageBox.Show(SQLeX.Message + " - Method: " + SQLeX.TargetSite.Name + " - Info: " + SQLeX.Source + " - Source: " + ToString());
                General.SqlCon.Close();
                Done = false;
            }

            finally
            {
                General.SqlCon.Close();
            }

            return Done;
        }
        public bool Modify()
        {
            bool Done = true; ;
            SqlCommand cmd = new SqlCommand(Tabla + "_M", General.SqlCon);
            General.SqlCon.Open();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = General.SqlCon;
                cmd.Parameters.AddWithValue("@Codigo", Codigo);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@DescripcionAdicional", DescripcionAdicional);
                cmd.Parameters.AddWithValue("@PrecioUnitario", PrecioUnitario);
                cmd.Parameters.AddWithValue("@CodCategoria", CodCategoria);
                cmd.Parameters.AddWithValue("@Marca", Marca);
                cmd.Parameters.AddWithValue("@Proveedor", Proveedor);
                cmd.Parameters.AddWithValue("@Ancho", Ancho);
                cmd.Parameters.AddWithValue("@Alto", Alto);
                cmd.Parameters.AddWithValue("@Largo", Largo);
                cmd.Parameters.AddWithValue("@Peso", Peso);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException SQLeX)
            {
                MessageBox.Show(SQLeX.Message + " - Method: " + SQLeX.TargetSite.Name + " - Info: " + SQLeX.Source + " - Source: " + ToString());
                General.SqlCon.Close();
                Done = false;
            }

            finally
            {
                General.SqlCon.Close();
            }

            return Done;
        }
        public void GetItemsById(Int32 Valor)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            string sql;
            try
            {
                General.SqlCon.Open();
                cmd.Connection = General.SqlCon;
                sql = "Select * from " + Tabla + " where Codigo = " + Valor;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (dr.Read())
                {
                    Codigo = Convert.ToInt32(dr["Codigo"]);
                    Descripcion = dr["Descripcion"].ToString();
                    DescripcionAdicional = dr["DescripcionAdicional"].ToString();
                    PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]);
                    CodCategoria = Convert.ToInt32(dr["CodCategoria"]);
                    Marca = Convert.ToInt32(dr["Marca"]);
                    Proveedor = Convert.ToInt32(dr["Proveedor"]);
                    Ancho = Convert.ToDecimal(dr["Ancho"]);
                    Alto = Convert.ToDecimal(dr["Alto"]);
                    Largo = Convert.ToDecimal(dr["Largo"]);
                    Peso = Convert.ToDecimal(dr["Peso"]);
                }
                General.SqlCon.Close();
            }
            catch (SqlException SQLeX)
            {
                MessageBox.Show(SQLeX.Message + " - Method: " + SQLeX.TargetSite.Name + " - Info: " + SQLeX.Source + " - Source: " + ToString());
                General.SqlCon.Close();
            }
        }
    }
}
