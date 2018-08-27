using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Clases
{
    class clsDetalleVta
    {
        private Int32 _idPedido;
        private Int32 _idProducto;
        private string _Descripcion;
        private Int32 _CodCategoria;
        private Int32 _CodMarca;
        private Int32 _Cantidad;
        private decimal _Monto;

        public Int32 idPedido
        {
            get { return _idPedido; }
            set { _idPedido = value; }
        }

        public Int32 idProducto
        {
            get { return _idProducto; }
            set { _idProducto = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public Int32 CodCategoria
        {
            get { return _CodCategoria; }
            set { _CodCategoria = value; }
        }

        public Int32 Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
    }
}
