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
    class clsProvincias
    {
        private string _Tabla = "Provincias";
        private Int32 _Codigo;
        private Int32 _CodigoPais;
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

        public Int32 CodigoPais
        {
            get { return _CodigoPais; }
            set { _CodigoPais = value; }
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
                cmd.Parameters.AddWithValue("@CodigoPais", CodigoPais);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.ExecuteNonQuery();
            }
            catch(SqlException SQLeX)
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
                cmd.Parameters.AddWithValue("@CodigoPais", CodigoPais);
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

        public void GetItemsById(Int32 Valor, Int32 xPais)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            string sql;
            try
            {
                General.SqlCon.Open();
                cmd.Connection = General.SqlCon;
                sql = "Select * from " + Tabla + " where Codigo = " + Valor + " and CodigoPais = " + xPais;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (dr.Read())
                {
                    Codigo = Convert.ToInt32(dr["Codigo"]);
                    CodigoPais = Convert.ToInt32(dr["CodigoPais"]);
                    Descripcion = dr["Descripcion"].ToString();
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

