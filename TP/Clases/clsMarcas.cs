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
    class clsMarcas
    {
        private string _Tabla = "Marcas";
        private Int32 _Codigo;
        private Int32 _CodCategoria;
        private string _Descripcion;

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
        public Int32 CodCategoria
        {
            get { return _CodCategoria; }
            set { _CodCategoria = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public bool Add()
        {
            bool Done = true;
            SqlCommand cmd = new SqlCommand(Tabla + "_A", General.SqlCon);
            General.SqlCon.Open();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = General.SqlCon;
                cmd.Parameters.AddWithValue("@Codigo", Codigo);
                cmd.Parameters.AddWithValue("@CodCategoria", CodCategoria);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException SQLeX)
            {
                MessageBox.Show(SQLeX.Message + " - Method: " + SQLeX.TargetSite.Name + " - Info: " + SQLeX.Source + " - Source: " + ToString());
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
            bool Done = true;
            SqlCommand cmd = new SqlCommand(Tabla + "_M", General.SqlCon);
            General.SqlCon.Open();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = General.SqlCon;
                cmd.Parameters.AddWithValue("@Codigo", Codigo);
                cmd.Parameters.AddWithValue("@CodCategoria", CodCategoria);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException SQLeX)
            {
                MessageBox.Show(SQLeX.Message + " - Method: " + SQLeX.TargetSite.Name + " - Info: " + SQLeX.Source + " - Source: " + ToString());
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
                    CodCategoria = Convert.ToInt32(dr["CodCategoria"]);
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
