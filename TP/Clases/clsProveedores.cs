using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace TP.Clases
{
    class clsProveedores
    {
            private string _Tabla = "Proveedores";
            private Int32 _Id;
            private string _RazonSocial;
            private string _Cuit;
            private string _Domicilio;
            private Int32 _Nro;
            private string _Piso;
            private string _Depto;
            private Int32 _CP;
            private string _Localidad;
            private Int32 _IdProvincia;
            private Int32 _IdPais;
            private string _Email;
            private string _Telefono;
            private string _Interno;
            private string _WebSite;
            private string _Fax;

            public string Tabla
            {
                get { return _Tabla; }
                set { _Tabla = value; }
            }

            public Int32 Id
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public string RazonSocial
            {
                get { return _RazonSocial; }
                set { _RazonSocial = value; }
            }

            public string Cuit
            {
                get { return _Cuit; }
                set { _Cuit = value; }
            }

            public string Domicilio
            {
                get { return _Domicilio; }
                set { _Domicilio = value; }
            }

            public Int32 Nro
            {
                get { return _Nro; }
                set { _Nro = value; }
            }

            public string Piso
            {
                get { return _Piso; }
                set { _Piso = value; }
            }

            public string Depto
            {
                get { return _Depto; }
                set { _Depto = value; }
            }

            public Int32 CP
            {
                get { return _CP; }
                set { _CP = value; }
            }

            public string Localidad
            {
                get { return _Localidad; }
                set { _Localidad = value; }
            }

            public Int32 IdProvincia
            {
                get { return _IdProvincia; }
                set { _IdProvincia = value; }
            }

            public Int32 IdPais
            {
                get { return _IdPais; }
                set { _IdPais = value; }
            }

            public string Email
            {
                get { return _Email; }
                set { _Email = value; }
            }

            public string Telefono
            {
                get { return _Telefono; }
                set { _Telefono = value; }
            }

            public string Interno
            {
                get { return _Interno; }
                set { _Interno = value; }
            }

            public string WebSite
            {
                get { return _WebSite; }
                set { _WebSite = value; }
            }

            public string Fax
            {
                get { return _Fax; }
                set { _Fax = value; }
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
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@RazonSocial", RazonSocial);
                    cmd.Parameters.AddWithValue("@Cuit", Cuit);
                    cmd.Parameters.AddWithValue("@Domicilio", Domicilio);
                    cmd.Parameters.AddWithValue("@Nro", Nro);
                    cmd.Parameters.AddWithValue("@Piso", Piso);
                    cmd.Parameters.AddWithValue("@Depto", Depto);
                    cmd.Parameters.AddWithValue("@CP", CP);
                    cmd.Parameters.AddWithValue("@Localidad", Localidad);
                    cmd.Parameters.AddWithValue("@IdProvincia", IdProvincia);
                    cmd.Parameters.AddWithValue("@IdPais", IdPais);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Interno", Interno);
                    cmd.Parameters.AddWithValue("@WebSite", WebSite);
                    cmd.Parameters.AddWithValue("@Fax", Fax);
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
                bool Done = true; ;
                SqlCommand cmd = new SqlCommand(Tabla + "_M", General.SqlCon);
                General.SqlCon.Open();

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = General.SqlCon;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@RazonSocial", RazonSocial);
                    cmd.Parameters.AddWithValue("@Cuit", Cuit);
                    cmd.Parameters.AddWithValue("@Domicilio", Domicilio);
                    cmd.Parameters.AddWithValue("@Nro", Nro);
                    cmd.Parameters.AddWithValue("@Piso", Piso);
                    cmd.Parameters.AddWithValue("@Depto", Depto);
                    cmd.Parameters.AddWithValue("@CP", CP);
                    cmd.Parameters.AddWithValue("@Localidad", Localidad);
                    cmd.Parameters.AddWithValue("@IdProvincia", IdProvincia);
                    cmd.Parameters.AddWithValue("@IdPais", IdPais);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Interno", Interno);
                    cmd.Parameters.AddWithValue("@WebSite", WebSite);
                    cmd.Parameters.AddWithValue("@Fax", Fax);
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
                        Id = Convert.ToInt32(dr["Id"]);
                        RazonSocial = dr["RazonSocial"].ToString();
                        Cuit = dr["Cuit"].ToString();
                        Domicilio = dr["Domicilio"].ToString();
                        Nro = Convert.ToInt32(dr["Nro"]);
                        Piso = dr["Piso"].ToString();
                        Depto = dr["Depto"].ToString();
                        CP = Convert.ToInt32(dr["CP"]);
                        Localidad = dr["Localidad"].ToString();
                        IdProvincia = Convert.ToInt32(dr["IdProvincia"]);
                        IdPais = Convert.ToInt32(dr["IdPais"]);
                        Email = dr["Email"].ToString();
                        Telefono = dr["Telefono"].ToString();
                        Interno = dr["Interno"].ToString();
                        WebSite = dr["WebSite"].ToString();
                        Fax = dr["Fax"].ToString();
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
