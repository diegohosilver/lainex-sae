using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP.Clases;

namespace TP.Formularios
{
    public partial class frmABMPedCp : Form
    {
        public frmABMPedCp()
        {
            InitializeComponent();
        }

        private void frmABMPedCp_Load(object sender, EventArgs e)
        {
            Init_Combos();
            asignarID();
            CargarGrilla();
            CrearDataTable();
        }
    }
}
