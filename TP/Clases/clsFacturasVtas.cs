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
    class clsFacturasVtas
    {
        private string _Tabla = "FacturasVtas";
        private Int32 _id;
        private Int32 _idCliente;
        private string _RSCliente;
        private DateTime _Fecha;
        private DateTime _Vencimiento;
        private decimal _MontoTotal;
        private Int32 _Estado;
        private Int32 _Moneda;

        public string Tabla
        {
            get { return _Tabla; }
            set { _Tabla = value; }
        }

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public string RSCliente
        {
            get { return _RSCliente; }
            set { _RSCliente = value; }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        } 

        public DateTime Vencimiento
        {
            get { return _Vencimiento; }
            set { _Vencimiento = value; }
        }

        public decimal MontoTotal
        {
            get { return _MontoTotal; }
            set { _MontoTotal = value; }
        }

        public Int32 Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public Int32 Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
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
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@RSCliente", RSCliente);
                cmd.Parameters.AddWithValue("@Fecha", Fecha);
                cmd.Parameters.AddWithValue("@Vencimiento", Vencimiento);
                cmd.Parameters.AddWithValue("@MontoTotal", MontoTotal);
                cmd.Parameters.AddWithValue("@Estado", Estado);
                cmd.Parameters.AddWithValue("@Moneda", Moneda);
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
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@RSCliente", RSCliente);
                cmd.Parameters.AddWithValue("@Fecha", Fecha);
                cmd.Parameters.AddWithValue("@Vencimiento", Vencimiento);
                cmd.Parameters.AddWithValue("@MontoTotal", MontoTotal);
                cmd.Parameters.AddWithValue("@Estado", Estado);
                cmd.Parameters.AddWithValue("@Moneda", Moneda);
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
                sql = "Select * from " + Tabla + " where id = " + Valor;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["id"]);
                    idCliente = Convert.ToInt32(dr["idCliente"]);
                    RSCliente = dr["RSCliente"].ToString();
                    Fecha = Convert.ToDateTime(dr["Fecha"]);
                    Vencimiento = Convert.ToDateTime(dr["Vencimiento"]);
                    MontoTotal = Convert.ToDecimal(dr["MontoTotal"]);
                    Estado = Convert.ToInt32(dr["Estado"]);
                    Moneda = Convert.ToInt32(dr["Moneda"]);
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
