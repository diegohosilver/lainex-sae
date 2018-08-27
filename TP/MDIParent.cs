using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using TP.Formularios;

namespace TP
{
    public partial class MDIParent : Form
    {
        public MDIParent()
        {
            InitializeComponent();
        }

        private void MDIParent_Load(object sender, EventArgs e)
        {
            General.cargarPadre(this);
            MdiClient ctlMDI;

            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException exc)
                {
                    // Catch and ignore the error if casting failed.
                }
            }
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.lfrmABMClientes = new frmABMClientes();
            General.lfrmABMClientes.MdiParent = this;
            General.lfrmABMClientes.Show();
        }

        private void paisesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.lfrmABMPaises = new frmABMPaises();
            General.lfrmABMPaises.MdiParent = this;
            General.lfrmABMPaises.Show();
        }

        private void provinciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.lfrmABMProvincias = new frmABMProvincias();
            General.lfrmABMProvincias.MdiParent = this;
            General.lfrmABMProvincias.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.lfrmABMProveedores = new frmABMProveedores();
            General.lfrmABMProveedores.MdiParent = this;
            General.lfrmABMProveedores.Show();
        }

        private void notasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process Notepad = new Process();
            Notepad.StartInfo.FileName = "notepad.exe";
            Notepad.StartInfo.Arguments = "";
            Notepad.Start();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.lfrmABMProductos = new frmABMProductos();
            General.lfrmABMProductos.MdiParent = this;
            General.lfrmABMProductos.Show();
        }

        private void categoríasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            General.lfrmABMCategorias = new frmABMCategorias();
            General.lfrmABMCategorias.MdiParent = this;
            General.lfrmABMCategorias.Show();
        }

        private void marcasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            General.lfrmABMMarcas = new frmABMMarcas();
            General.lfrmABMMarcas.MdiParent = this;
            General.lfrmABMMarcas.Show();
        }

        private void monedaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.lfrmABMMoneda = new frmABMMoneda();
            General.lfrmABMMoneda.MdiParent = this;
            General.lfrmABMMoneda.Show();
        }
        private void pedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            General.lfrmABMPedVta = new frmABMPedVta();
            General.lfrmABMPedVta.MdiParent = this;
            General.lfrmABMPedVta.Show();
        }
    }
}
