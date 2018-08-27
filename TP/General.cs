using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TP.Formularios;
using TP;

public class General
{
    //Conexion
    public static SqlConnection SqlCon = new SqlConnection("Server=localhost\\sqlexpress;Database=TP;Integrated security=SSPI;");
    public static string SqlConString = "Server=localhost\\sqlexpress;Database=TP;Integrated security=SSPI;";
    //public static SqlConnection SqlCon = new SqlConnection("Server=DIEGO-PC;Database=TP;Integrated security=SSPI;");
    //Formularios
    public static frmBuscador lfrmBuscador;
    public static frmABMClientes lfrmABMClientes;
    public static frmABMPaises lfrmABMPaises;
    public static frmABMProvincias lfrmABMProvincias;
    public static frmABMProveedores lfrmABMProveedores;
    public static frmABMProductos lfrmABMProductos;
    public static frmABMCategorias lfrmABMCategorias;
    public static frmABMMarcas lfrmABMMarcas;
    public static frmABMMoneda lfrmABMMoneda;
    public static frmABMPedVta lfrmABMPedVta;
    public static frmConsultaProductos lfrmConsultaProductos;
    public static frmConsultaClientes lfrmConsultaClientes;
    public static MDIParent frmParent;
    public static frmABMPedCp lfrmABMPedCp;

    //Funciones
    public static void cargarPadre(MDIParent MDI)
    {
        frmParent = MDI;
    }
}