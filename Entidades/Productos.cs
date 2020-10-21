using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroPedidos.Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public float Inventario { get; set; }

        [ForeignKey("ProductoId")]
        public virtual List<OrdenesDetalle> Detalle { get; set; }

        public Productos()
        {
            Detalle = new List<OrdenesDetalle>();
        }

        public Productos(int id, string descripcion, decimal costo, float inventario)
        {
            ProductoId = id;
            Descripcion = descripcion;
            Costo = costo;
            Inventario = inventario;
            Detalle = new List<OrdenesDetalle>();
        }
    }
}
